﻿using System.Collections.Generic;

namespace SoulsFormats
{
    /// <summary>
    /// A multi-file DDS container used in DS1, DSR, DS2, DS3, DeS, BB, and NB.
    /// </summary>
    public partial class TPF : SoulsFile<TPF>
    {
        /// <summary>
        /// The textures contained within this TPF.
        /// </summary>
        public List<Texture> Textures;

        /// <summary>
        /// The platform this TPF will be used on.
        /// </summary>
        public TPFPlatform Platform;

        /// <summary>
        /// Indicates encoding used for texture names.
        /// </summary>
        public byte Encoding;

        /// <summary>
        /// Unknown.
        /// </summary>
        public byte Flag2;

        /// <summary>
        /// Creates an empty TPF configured for DS3.
        /// </summary>
        public TPF()
        {
            Textures = new List<Texture>();
            Platform = TPFPlatform.PC;
            Encoding = 1;
            Flag2 = 3;
        }

        /// <summary>
        /// Returns true if the data appears to be a TPF.
        /// </summary>
        internal override bool Is(BinaryReaderEx br)
        {
            string magic = br.GetASCII(0, 4);
            return magic == "TPF\0";
        }

        /// <summary>
        /// Reads TPF data from a BinaryReaderEx.
        /// </summary>
        internal override void Read(BinaryReaderEx br)
        {
            br.BigEndian = false;
            br.AssertASCII("TPF\0");
            br.BigEndian = br.GetByte(0xC) == 2;

            int totalFileSize = br.ReadInt32();
            int fileCount = br.ReadInt32();

            Platform = br.ReadEnum8<TPFPlatform>();
            Flag2 = br.AssertByte(0, 1, 2, 3);
            Encoding = br.AssertByte(0, 1, 2);
            br.AssertByte(0);

            Textures = new List<Texture>(fileCount);
            for (int i = 0; i < fileCount; i++)
            {
                Textures.Add(new Texture(br, Platform, Flag2, Encoding));
            }
        }

        /// <summary>
        /// Writes TPF data to a BinaryWriterEx.
        /// </summary>
        internal override void Write(BinaryWriterEx bw)
        {
            bw.BigEndian = false;
            bw.WriteASCII("TPF\0");
            bw.ReserveInt32("DataSize");
            bw.WriteInt32(Textures.Count);
            bw.WriteByte((byte)Platform);
            bw.WriteByte(Flag2);
            bw.WriteByte(Encoding);
            bw.WriteByte(0);

            for (int i = 0; i < Textures.Count; i++)
            {
                Textures[i].Write(bw, i, Platform, Flag2);
            }
            bw.Pad(0x10);

            for (int i = 0; i < Textures.Count; i++)
            {
                Texture texture = Textures[i];
                bw.FillUInt32($"FileName{i}", (uint)bw.Position);
                if (Encoding == 1)
                    bw.WriteUTF16(texture.Name, true);
                else if (Encoding == 0 || Encoding == 2)
                    bw.WriteShiftJIS(texture.Name, true);
            }

            int dataStart = (int)bw.Position;
            for (int i = 0; i < Textures.Count; i++)
            {
                Texture texture = Textures[i];
                if (texture.Bytes.Length > 0)
                    bw.Pad(0x10);

                bw.FillUInt32($"FileData{i}", (uint)bw.Position);

                byte[] bytes = texture.Bytes;
                if (texture.Flags1 == 2 || texture.Flags2 == 3)
                    bytes = DCX.Compress(bytes, DCX.Type.ACEREDGE);
                bw.FillInt32($"FileSize{i}", bytes.Length);
                bw.WriteBytes(bytes);
            }
            bw.FillInt32("DataSize", (int)bw.Position - dataStart);
        }

        /// <summary>
        /// A DDS texture in a TPF container.
        /// </summary>
        public class Texture
        {
            /// <summary>
            /// The name of the texture; should not include a path or extension.
            /// </summary>
            public string Name;

            /// <summary>
            /// Indicates format of the texture.
            /// </summary>
            public byte Format;

            /// <summary>
            /// Whether this texture is a cubemap.
            /// </summary>
            public TexType Type;

            /// <summary>
            /// Number of mipmap levels in this texture.
            /// </summary>
            public byte Mipmaps;

            /// <summary>
            /// Unknown.
            /// </summary>
            public byte Flags1;

            /// <summary>
            /// Unknown.
            /// </summary>
            public int Flags2;

            /// <summary>
            /// The raw data of the texture.
            /// </summary>
            public byte[] Bytes;

            /// <summary>
            /// Extended metadata present in headerless console TPF textures.
            /// </summary>
            public TexHeader Header;

            /// <summary>
            /// Create a new PC Texture with the specified information; Cubemap and Mipmaps are determined based on bytes.
            /// </summary>
            public Texture(string name, byte format, byte flags1, int flags2, byte[] bytes)
            {
                Name = name;
                Format = format;
                Flags1 = flags1;
                Flags2 = flags2;
                Bytes = bytes;
                Header = null;

                DDS dds = new DDS(bytes);
                if (dds.dwCaps2.HasFlag(DDS.DDSCAPS2.CUBEMAP))
                    Type = TexType.Cubemap;
                else if (dds.dwCaps2.HasFlag(DDS.DDSCAPS2.VOLUME))
                    Type = TexType.Volume;
                else
                    Type = TexType.Texture;
                Mipmaps = (byte)dds.dwMipMapCount;
            }

            internal Texture(BinaryReaderEx br, TPFPlatform platform, byte flag2, byte encoding)
            {
                uint fileOffset = br.ReadUInt32();
                int fileSize = br.ReadInt32();

                Format = br.ReadByte();
                Type = br.ReadEnum8<TexType>();
                Mipmaps = br.ReadByte();
                Flags1 = br.AssertByte(0, 1, 2, 3);

                uint nameOffset = uint.MaxValue;
                if (platform == TPFPlatform.PC)
                {
                    Header = null;
                    nameOffset = br.ReadUInt32();
                    Flags2 = br.AssertInt32(0, 1);
                }
                else
                {
                    Header = new TexHeader();
                    Header.Width = br.ReadInt16();
                    Header.Height = br.ReadInt16();

                    if (platform == TPFPlatform.Xbox360)
                    {
                        br.AssertInt32(0);
                        nameOffset = br.ReadUInt32();
                        br.AssertInt32(0);
                    }
                    else if (platform == TPFPlatform.PS3)
                    {
                        Header.Unk1 = br.ReadInt32();
                        if (flag2 != 0)
                            Header.Unk2 = br.AssertInt32(0, 0xAAE4);
                        nameOffset = br.ReadUInt32();
                        Flags2 = br.AssertInt32(0, 1);
                    }
                    else if (platform == TPFPlatform.PS4 || platform == TPFPlatform.Xbone)
                    {
                        Header.TextureCount = br.AssertInt32(1, 6);
                        Header.Unk2 = br.AssertInt32(0xD);
                        nameOffset = br.ReadUInt32();
                        Flags2 = br.AssertInt32(0, 1);
                        Header.DXGIFormat = br.ReadInt32();
                    }
                }

                Bytes = br.GetBytes(fileOffset, fileSize);
                if (Flags1 == 2 || Flags1 == 3)
                    Bytes = DCX.Decompress(Bytes);

                if (encoding == 1)
                    Name = br.GetUTF16(nameOffset);
                else if (encoding == 0 || encoding == 2)
                    Name = br.GetShiftJIS(nameOffset);
            }

            internal void Write(BinaryWriterEx bw, int index, TPFPlatform platform, byte flag2)
            {
                if (platform == TPFPlatform.PC)
                {
                    DDS dds = new DDS(Bytes);
                    if (dds.dwCaps2.HasFlag(DDS.DDSCAPS2.CUBEMAP))
                        Type = TexType.Cubemap;
                    else if (dds.dwCaps2.HasFlag(DDS.DDSCAPS2.VOLUME))
                        Type = TexType.Volume;
                    else
                        Type = TexType.Texture;
                    Mipmaps = (byte)dds.dwMipMapCount;
                }

                bw.ReserveUInt32($"FileData{index}");
                bw.ReserveInt32($"FileSize{index}");

                bw.WriteByte(Format);
                bw.WriteByte((byte)Type);
                bw.WriteByte(Mipmaps);
                bw.WriteByte(Flags1);

                if (platform == TPFPlatform.PC)
                {
                    bw.ReserveUInt32($"FileName{index}");
                    bw.WriteInt32(Flags2);
                }
                else
                {
                    bw.WriteInt16(Header.Width);
                    bw.WriteInt16(Header.Height);

                    if (platform == TPFPlatform.Xbox360)
                    {
                        bw.WriteInt32(0);
                        bw.ReserveUInt32($"FileName{index}");
                        bw.WriteInt32(0);
                    }
                    else if (platform == TPFPlatform.PS3)
                    {
                        bw.WriteInt32(Header.Unk1);
                        if (flag2 != 0)
                            bw.WriteInt32(Header.Unk2);
                        bw.ReserveUInt32($"FileName{index}");
                        bw.WriteInt32(Flags2);
                    }
                    else if (platform == TPFPlatform.PS4 || platform == TPFPlatform.Xbone)
                    {
                        bw.WriteInt32(Header.TextureCount);
                        bw.WriteInt32(Header.Unk2);
                        bw.ReserveUInt32($"FileName{index}");
                        bw.WriteInt32(Flags2);
                        bw.WriteInt32(Header.DXGIFormat);
                    }
                }
            }

            /// <summary>
            /// Attempt to create a full DDS file from headerless console textures. Very very very poor support at the moment.
            /// </summary>
            public byte[] Headerize()
            {
                return Headerizer.Headerize(this);
            }

            /// <summary>
            /// Returns the name of this texture.
            /// </summary>
            public override string ToString()
            {
                return $"[{Format} {Type}] {Name}";
            }
        }

        /// <summary>
        /// The platform of the game a TPF is for.
        /// </summary>
        public enum TPFPlatform : byte
        {
            /// <summary>
            /// Headered DDS with minimal metadata.
            /// </summary>
            PC = 0,

            /// <summary>
            /// Headerless DDS with pre-DX10 metadata.
            /// </summary>
            Xbox360 = 1,

            /// <summary>
            /// Headerless DDS with pre-DX10 metadata.
            /// </summary>
            PS3 = 2,

            /// <summary>
            /// Headerless DDS with DX10 metadata.
            /// </summary>
            PS4 = 4,

            /// <summary>
            /// Headerless DDS with DX10 metadata.
            /// </summary>
            Xbone = 5,
        }

        /// <summary>
        /// Type of texture in a TPF.
        /// </summary>
        public enum TexType : byte
        {
            /// <summary>
            /// One 2D texture.
            /// </summary>
            Texture = 0,

            /// <summary>
            /// Six 2D textures.
            /// </summary>
            Cubemap = 1,

            /// <summary>
            /// One 3D texture.
            /// </summary>
            Volume = 2,
        }

        /// <summary>
        /// Extra metadata for headerless textures used in console versions.
        /// </summary>
        public class TexHeader
        {
            /// <summary>
            /// Width of the texture, in pixels.
            /// </summary>
            public short Width;

            /// <summary>
            /// Height of the texture, in pixels.
            /// </summary>
            public short Height;

            /// <summary>
            /// Number of textures in the array, either 1 for normal textures or 6 for cubemaps.
            /// </summary>
            public int TextureCount;

            /// <summary>
            /// Unknown; PS3 only.
            /// </summary>
            public int Unk1;

            /// <summary>
            /// Unknown; 0x0 or 0xAAE4 in DeS, 0xD in DS3.
            /// </summary>
            public int Unk2;

            /// <summary>
            /// Microsoft DXGI_FORMAT.
            /// </summary>
            public int DXGIFormat;
        }
    }
}
