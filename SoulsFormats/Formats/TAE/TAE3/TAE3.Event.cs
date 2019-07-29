using System;

namespace SoulsFormats
{
    public partial class TAE3
    {
        /// <summary>
        /// Determines the behavior of an event and what data it contains.
        /// </summary>
        public enum EventType : uint
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            JumpTable = 000,
            Tae001 = 001,
            Tae002 = 002,
            Tae005 = 005,
            Tae016 = 016,
            Tae017 = 017,
            Tae024 = 024,
            SwitchWeapon1 = 032,
            SwitchWeapon2 = 033,
            Tae034 = 034,
            Tae035 = 035,
            CastSelectedSpell = 064,
            ConsumeSelectedItem = 065,
            CreateBehaviorSpEffect1 = 066,
            CreateBehaviorSpEffect2 = 067,
            Tae095 = 095,
            PlayFFX = 096,
            Tae099 = 099,
            Tae110 = 110,
            HitEffect = 112,
            Tae113 = 113,
            Tae114 = 114,
            Tae115 = 115,
            Tae116 = 116,
            Tae117 = 117,
            Tae118 = 118,
            Tae119 = 119,
            Tae120 = 120,
            Tae121 = 121,
            PlayFFXIfSpEffect = 122,
            PlaySound1 = 128,
            PlaySound2 = 129,
            PlaySound3 = 130,
            PlaySound4 = 131,
            PlaySound5 = 132,
            Tae136 = 136,
            CreateDecal1 = 137,
            CreateDecal2 = 138,
            CreateCameraShakeSpecial = 144,
            CreateCameraShake = 145,
            SetLockCamParam1 = 150,
            SetLockCamParam2 = 151,
            Tae160 = 160,
            Tae161 = 161,
            SetDebugGhost = 192,
            FadeOut = 193,
            Tae194 = 194,
            EnableBlurFeedback = 195,
            C_ARSN_BumpBlendDecal = 196,
            Ghost = 197,
            Tae224 = 224,
            ChangeStaminaRegen = 225,
            Tae226 = 226,
            Tae227 = 227,
            RagdollReviveTime = 228,
            CreateAISound1 = 229,
            Tae230 = 230,
            SetEzStateRequestId = 231,
            Tae232 = 232,
            ChangeDrawMask = 233,
            Tae234 = 234,
            Tae235 = 235,
            RollDistanceReduction = 236,
            CreateAISound2 = 237,
            Tae300 = 300,
            Padding = 301,
            CreateSpEffect1 = 302,
            PlayAnimation = 303,
            BehaviorThing = 304,
            Tae305 = 305,
            Tae306 = 306,
            CreateBehaviorPC = 307,
            Tae308 = 308,
            Tae310 = 310,
            Tae311 = 311,
            Tae312 = 312,
            Tae317 = 317,
            ActionRequest = 320,
            WeaponArtMpConsume = 330,
            CreateSpEffect_SwordArt = 331,
            Tae332 = 332,
            CreateSpEffect2 = 401,
            CreateWind = 402,
            Tae500 = 500,
            Tae510 = 510,
            Tae520 = 520,
            Tae521 = 521,
            SetSpecialLockOnParameter = 522,
            EnableBehavior = 600,
            Tae601 = 601,
            Tae602 = 602,
            DebugAnimSpeed = 603,
            TestParam = 604,
            Tae605 = 605,
            Tae606 = 606,
            Tae607 = 607,
            Tae700 = 700,
            EnableTurningDirection = 703,
            Tae704 = 704,
            FacingAngleCorrection = 705,
            Tae706 = 706,
            Tae707 = 707,
            HideWeapon = 710,
            HideModelMask = 711,
            DamageLevelModule = 712,
            ModelMask = 713,
            DamageLevelFunction = 714,
            Tae715 = 715,
            CultStart = 720,
            Tae730 = 730,
            Tae740 = 740,
            IFrameState = 760,
            BonePos = 770,
            BoneFixOn1 = 771,
            BoneFixOn2 = 772,
            AddHeight = 780,
            TurnLowerBody = 781,
            Tae782 = 782,
            SpawnChrFinderBullet = 785,
            SetMaxUndurationAngle = 786,
            Tae790 = 790,
            Tae791 = 791,
            HitEffect2 = 792,
            CultSacrifice1 = 793,
            SacrificeEmpty = 794,
            Toughness = 795,
            CreateMenu = 796,
            CeremonyParamID = 797,
            CultSingle = 798,
            CultEmpty2 = 799,
            Tae800 = 800,
            Tae900 = 900,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// An action or effect triggered at a certain time during an animation.
        /// </summary>
        public abstract class Event
        {
            /// <summary>
            /// The type of this event.
            /// </summary>
            [System.ComponentModel.Browsable(false)]
            public abstract EventType Type { get; }

            /// <summary>
            /// When the event begins.
            /// </summary>
            public float StartTime { get; set; }

            /// <summary>
            /// When the event ends.
            /// </summary>
            public float EndTime { get; set; }

            internal Event(float startTime, float endTime)
            {
                StartTime = startTime;
                EndTime = endTime;
            }

            internal void WriteTime(BinaryWriterEx bw)
            {
                bw.WriteSingle(StartTime);
                bw.WriteSingle(EndTime);
            }

            internal void WriteHeader(BinaryWriterEx bw, int i, int j, long timeStart)
            {
                bw.WriteInt64(timeStart + j * 8);
                bw.WriteInt64(timeStart + j * 8 + 4);
                bw.ReserveInt64($"EventDataOffset{i}:{j}");
            }

            internal void WriteData(BinaryWriterEx bw, int i, int j)
            {
                bw.FillInt64($"EventDataOffset{i}:{j}", bw.Position);
                bw.WriteUInt32((uint)Type);
                bw.WriteUInt32(0);
                bw.WriteInt64(bw.Position + 8);
                WriteSpecific(bw);
                bw.Pad(0x10);
            }

            internal abstract void WriteSpecific(BinaryWriterEx bw);

            /// <summary>
            /// Returns the start time, end time, and type of the event.
            /// </summary>
            public override string ToString()
            {
                return $"{(int)Math.Round(StartTime * 30):D3} - {(int)Math.Round(EndTime * 30):D3} {Type}";
            }

            public Event Clone()
            {
                var evt = EventFromType(this.Type);
                var props = evt.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.SetMethod == null) continue;
                    var val = prop.GetValue(this);
                    prop.SetValue(evt, val);
                }
                return evt;
            }

            public static Event EventFromType(EventType type)
            {
                switch (type)
                {
                    case EventType.JumpTable: return new Tae000_JumpTable(0, 0.25f);
                    case EventType.Tae001: return new Tae001(0, 0.25f);
                    case EventType.Tae002: return new Tae002(0, 0.25f);
                    case EventType.Tae005: return new Tae005(0, 0.25f);
                    case EventType.Tae016: return new Tae016(0, 0.25f);
                    case EventType.Tae017: return new Tae017(0, 0.25f);
                    case EventType.Tae024: return new Tae024(0, 0.25f);
                    case EventType.SwitchWeapon1: return new Tae032_SwitchWeapon1(0, 0.25f);
                    case EventType.SwitchWeapon2: return new Tae033_SwitchWeapon2(0, 0.25f);
                    case EventType.Tae034: return new Tae034(0, 0.25f);
                    case EventType.Tae035: return new Tae035(0, 0.25f);
                    case EventType.CastSelectedSpell: return new Tae064_CastSelectedSpell(0, 0.25f);
                    case EventType.ConsumeSelectedItem: return new Tae065_ConsumeSelectedItem(0, 0.25f);
                    case EventType.CreateBehaviorSpEffect1: return new Tae066_CreateBehaviorSpEffect1(0, 0.25f);
                    case EventType.CreateBehaviorSpEffect2: return new Tae067_CreateBehaviorSpEffect2(0, 0.25f);
                    case EventType.Tae095: return new Tae095(0, 0.25f);
                    case EventType.PlayFFX: return new Tae096_PlayFFX(0, 0.25f);
                    case EventType.Tae099: return new Tae099(0, 0.25f);
                    case EventType.Tae110: return new Tae110(0, 0.25f);
                    case EventType.HitEffect: return new Tae112_HitEffect(0, 0.25f);
                    case EventType.Tae113: return new Tae113(0, 0.25f);
                    case EventType.Tae114: return new Tae114(0, 0.25f);
                    case EventType.Tae115: return new Tae115(0, 0.25f);
                    case EventType.Tae116: return new Tae116(0, 0.25f);
                    case EventType.Tae117: return new Tae117(0, 0.25f);
                    case EventType.Tae118: return new Tae118(0, 0.25f);
                    case EventType.Tae119: return new Tae119(0, 0.25f);
                    case EventType.Tae120: return new Tae120(0, 0.25f);
                    case EventType.Tae121: return new Tae121(0, 0.25f);
                    case EventType.PlayFFXIfSpEffect: return new Tae122_PlayFFXIfSpEffect(0, 0.25f);
                    case EventType.PlaySound1: return new Tae128_PlaySound1(0, 0.25f);
                    case EventType.PlaySound2: return new Tae129_PlaySound2(0, 0.25f);
                    case EventType.PlaySound3: return new Tae130_PlaySound3(0, 0.25f);
                    case EventType.PlaySound4: return new Tae131_PlaySound4(0, 0.25f);
                    case EventType.PlaySound5: return new Tae132_PlaySound5(0, 0.25f);
                    case EventType.CreateDecal1: return new Tae137_CreateDecal1(0, 0.25f);
                    case EventType.CreateDecal2: return new Tae138_CreateDecal2(0, 0.25f);
                    case EventType.CreateCameraShakeSpecial: return new Tae144_CreateCameraShakeSpecial(0, 0.25f);
                    case EventType.CreateCameraShake: return new Tae145_CreateCameraShake(0, 0.25f);
                    case EventType.SetLockCamParam1: return new Tae150_SetLockCamParam1(0, 0.25f);
                    case EventType.SetLockCamParam2: return new Tae151_SetLockCamParam2(0, 0.25f);
                    case EventType.Tae160: return new Tae160(0, 0.25f);
                    case EventType.Tae161: return new Tae161(0, 0.25f);
                    case EventType.SetDebugGhost: return new Tae192_SetDebugGhost(0, 0.25f);
                    case EventType.FadeOut: return new Tae193_FadeOut(0, 0.25f);
                    case EventType.Tae194: return new Tae194(0, 0.25f);
                    case EventType.EnableBlurFeedback: return new Tae195_EnableBlurFeedback(0, 0.25f);
                    case EventType.C_ARSN_BumpBlendDecal: return new Tae196_C_ARSN_BumpBlendDecal(0, 0.25f);
                    case EventType.Ghost: return new Tae197_Ghost(0, 0.25f);
                    case EventType.Tae224: return new Tae224(0, 0.25f);
                    case EventType.ChangeStaminaRegen: return new Tae225_ChangeStaminaRegen(0, 0.25f);
                    case EventType.Tae226: return new Tae226(0, 0.25f);
                    case EventType.Tae227: return new Tae227(0, 0.25f);
                    case EventType.RagdollReviveTime: return new Tae228_RagdollReviveTime(0, 0.25f);
                    case EventType.CreateAISound1: return new Tae229_CreateAISound1(0, 0.25f);
                    case EventType.Tae230: return new Tae230(0, 0.25f);
                    case EventType.SetEzStateRequestId: return new Tae231_SetEzStateRequestId(0, 0.25f);
                    case EventType.Tae232: return new Tae232(0, 0.25f);
                    case EventType.ChangeDrawMask: return new Tae233_ChangeDrawMask(0, 0.25f);
                    case EventType.Tae234: return new Tae234(0, 0.25f);
                    case EventType.Tae235: return new Tae235(0, 0.25f);
                    case EventType.RollDistanceReduction: return new Tae236_RollDistanceReduction(0, 0.25f);
                    case EventType.CreateAISound2: return new Tae237_CreateAISound2(0, 0.25f);
                    case EventType.Tae300: return new Tae300(0, 0.25f);
                    case EventType.Padding: return new Tae301_Padding(0, 0.25f);
                    case EventType.CreateSpEffect1: return new Tae302_CreateSpEffect1(0, 0.25f);
                    case EventType.PlayAnimation: return new Tae303_PlayAnimation(0, 0.25f);
                    case EventType.BehaviorThing: return new Tae304_BehaviorThing(0, 0.25f);
                    case EventType.Tae305: return new Tae305(0, 0.25f);
                    case EventType.Tae306: return new Tae306(0, 0.25f);
                    case EventType.CreateBehaviorPC: return new Tae307_CreateBehaviorPC(0, 0.25f);
                    case EventType.Tae308: return new Tae308(0, 0.25f);
                    case EventType.Tae310: return new Tae310(0, 0.25f);
                    case EventType.Tae311: return new Tae311(0, 0.25f);
                    case EventType.Tae312: return new Tae312(0, 0.25f);
                    case EventType.ActionRequest: return new Tae320_ActionRequest(0, 0.25f);
                    case EventType.WeaponArtMpConsume: return new Tae330_WeaponArtMpConsume(0, 0.25f);
                    case EventType.CreateSpEffect_SwordArt: return new Tae331_CreateSpEffect_SwordArt(0, 0.25f);
                    case EventType.Tae332: return new Tae332(0, 0.25f);
                    case EventType.CreateSpEffect2: return new Tae401_CreateSpEffect2(0, 0.25f);
                    case EventType.CreateWind: return new Tae402_CreateWind(0, 0.25f);
                    case EventType.Tae500: return new Tae500(0, 0.25f);
                    case EventType.Tae510: return new Tae510(0, 0.25f);
                    case EventType.Tae520: return new Tae520(0, 0.25f);
                    case EventType.Tae521: return new Tae521(0, 0.25f);
                    case EventType.SetSpecialLockOnParameter: return new Tae522_SetSpecialLockOnParameter(0, 0.25f);
                    case EventType.EnableBehavior: return new Tae600_EnableBehavior(0, 0.25f);
                    case EventType.Tae601: return new Tae601(0, 0.25f);
                    case EventType.Tae602: return new Tae602(0, 0.25f);
                    case EventType.DebugAnimSpeed: return new Tae603_DebugAnimSpeed(0, 0.25f);
                    case EventType.TestParam: return new Tae604_TestParam(0, 0.25f);
                    case EventType.Tae605: return new Tae605(0, 0.25f);
                    case EventType.Tae606: return new Tae606(0, 0.25f);
                    case EventType.Tae700: return new Tae700(0, 0.25f);
                    case EventType.EnableTurningDirection: return new Tae703_EnableTurningDirection(0, 0.25f);
                    case EventType.Tae704: return new Tae704(0, 0.25f);
                    case EventType.FacingAngleCorrection: return new Tae705_FacingAngleCorrection(0, 0.25f);
                    case EventType.Tae706: return new Tae706(0, 0.25f);
                    case EventType.Tae707: return new Tae707(0, 0.25f);
                    case EventType.HideWeapon: return new Tae710_HideWeapon(0, 0.25f);
                    case EventType.HideModelMask: return new Tae711_HideModelMask(0, 0.25f);
                    case EventType.DamageLevelModule: return new Tae712_DamageLevelModule(0, 0.25f);
                    case EventType.ModelMask: return new Tae713_ModelMask(0, 0.25f);
                    case EventType.DamageLevelFunction: return new Tae714_DamageLevelFunction(0, 0.25f);
                    case EventType.Tae715: return new Tae715(0, 0.25f);
                    case EventType.CultStart: return new Tae720_CultStart(0, 0.25f);
                    case EventType.Tae730: return new Tae730(0, 0.25f);
                    case EventType.Tae740: return new Tae740(0, 0.25f);
                    case EventType.IFrameState: return new Tae760_IFrameState(0, 0.25f);
                    case EventType.BonePos: return new Tae770_BonePos(0, 0.25f);
                    case EventType.BoneFixOn1: return new Tae771_BoneFixOn1(0, 0.25f);
                    case EventType.BoneFixOn2: return new Tae772_BoneFixOn2(0, 0.25f);
                    case EventType.AddHeight: return new Tae780_AddHeight(0, 0.25f);
                    case EventType.TurnLowerBody: return new Tae781_TurnLowerBody(0, 0.25f);
                    case EventType.Tae782: return new Tae782(0, 0.25f);
                    case EventType.SpawnChrFinderBullet: return new Tae785_SpawnChrFinderBullet(0, 0.25f);
                    case EventType.SetMaxUndurationAngle: return new Tae786_SetMaxUndurationAngle(0, 0.25f);
                    case EventType.Tae790: return new Tae790(0, 0.25f);
                    case EventType.Tae791: return new Tae791(0, 0.25f);
                    case EventType.HitEffect2: return new Tae792_HitEffect2(0, 0.25f);
                    case EventType.CultSacrifice1: return new Tae793_CultSacrifice1(0, 0.25f);
                    case EventType.SacrificeEmpty: return new Tae794_SacrificeEmpty(0, 0.25f);
                    case EventType.Toughness: return new Tae795_Toughness(0, 0.25f);
                    case EventType.CreateMenu: return new Tae796_CreateMenu(0, 0.25f);
                    case EventType.CeremonyParamID: return new Tae797_CeremonyParamID(0, 0.25f);
                    case EventType.CultSingle: return new Tae798_CultSingle(0, 0.25f);
                    case EventType.CultEmpty2: return new Tae799_CultEmpty2(0, 0.25f);
                    case EventType.Tae800: return new Tae800(0, 0.25f);
                    default: throw new NotImplementedException();
                }
            }

            internal static Event Read(BinaryReaderEx br)
            {
                long startTimeOffset = br.ReadInt64();
                long endTimeOffset = br.ReadInt64();
                long eventDataOffset = br.ReadInt64();
                float startTime = br.GetSingle(startTimeOffset);
                float endTime = br.GetSingle(endTimeOffset);

                Event result;
                br.StepIn(eventDataOffset);
                {
                    EventType type = br.ReadEnum32<EventType>();

                    int UnkNew1 = br.ReadInt32(); //Bad Solution ?
                    int UnkNew2 = br.ReadInt32();
                    int UnkNew3 = br.ReadInt32();
                    switch (type)
                    {
                        case EventType.JumpTable: result = new Tae000_JumpTable(startTime, endTime, br); break;
                        case EventType.Tae001: result = new Tae001(startTime, endTime, br); break;
                        case EventType.Tae002: result = new Tae002(startTime, endTime, br); break;
                        case EventType.Tae005: result = new Tae005(startTime, endTime, br); break;
                        case EventType.Tae016: result = new Tae016(startTime, endTime, br); break;
                        case EventType.Tae017: result = new Tae017(startTime, endTime, br); break;
                        case EventType.Tae024: result = new Tae024(startTime, endTime, br); break;
                        case EventType.SwitchWeapon1: result = new Tae032_SwitchWeapon1(startTime, endTime, br); break;
                        case EventType.SwitchWeapon2: result = new Tae033_SwitchWeapon2(startTime, endTime, br); break;
                        case EventType.Tae034: result = new Tae034(startTime, endTime, br); break;
                        case EventType.Tae035: result = new Tae035(startTime, endTime, br); break;
                        case EventType.CastSelectedSpell: result = new Tae064_CastSelectedSpell(startTime, endTime, br); break;
                        case EventType.ConsumeSelectedItem: result = new Tae065_ConsumeSelectedItem(startTime, endTime, br); break;
                        case EventType.CreateBehaviorSpEffect1: result = new Tae066_CreateBehaviorSpEffect1(startTime, endTime, br); break;
                        case EventType.CreateBehaviorSpEffect2: result = new Tae067_CreateBehaviorSpEffect2(startTime, endTime, br); break;
                        case EventType.Tae095: result = new Tae095(startTime, endTime, br); break;
                        case EventType.PlayFFX: result = new Tae096_PlayFFX(startTime, endTime, br); break;
                        case EventType.Tae099: result = new Tae099(startTime, endTime, br); break;
                        case EventType.Tae110: result = new Tae110(startTime, endTime, br); break;
                        case EventType.HitEffect: result = new Tae112_HitEffect(startTime, endTime, br); break;
                        case EventType.Tae113: result = new Tae113(startTime, endTime, br); break;
                        case EventType.Tae114: result = new Tae114(startTime, endTime, br); break;
                        case EventType.Tae115: result = new Tae115(startTime, endTime, br); break;
                        case EventType.Tae116: result = new Tae116(startTime, endTime, br); break;
                        case EventType.Tae117: result = new Tae117(startTime, endTime, br); break;
                        case EventType.Tae118: result = new Tae118(startTime, endTime, br); break;
                        case EventType.Tae119: result = new Tae119(startTime, endTime, br); break;
                        case EventType.Tae120: result = new Tae120(startTime, endTime, br); break;
                        case EventType.Tae121: result = new Tae121(startTime, endTime, br); break;
                        case EventType.PlayFFXIfSpEffect: result = new Tae122_PlayFFXIfSpEffect(startTime, endTime, br); break;
                        case EventType.PlaySound1: result = new Tae128_PlaySound1(startTime, endTime, br); break;
                        case EventType.PlaySound2: result = new Tae129_PlaySound2(startTime, endTime, br); break;
                        case EventType.PlaySound3: result = new Tae130_PlaySound3(startTime, endTime, br); break;
                        case EventType.PlaySound4: result = new Tae131_PlaySound4(startTime, endTime, br); break;
                        case EventType.PlaySound5: result = new Tae132_PlaySound5(startTime, endTime, br); break;
                        case EventType.CreateDecal1: result = new Tae137_CreateDecal1(startTime, endTime, br); break;
                        case EventType.CreateDecal2: result = new Tae138_CreateDecal2(startTime, endTime, br); break;
                        case EventType.CreateCameraShakeSpecial: result = new Tae144_CreateCameraShakeSpecial(startTime, endTime, br); break;
                        case EventType.CreateCameraShake: result = new Tae145_CreateCameraShake(startTime, endTime, br); break;
                        case EventType.SetLockCamParam1: result = new Tae150_SetLockCamParam1(startTime, endTime, br); break;
                        case EventType.SetLockCamParam2: result = new Tae151_SetLockCamParam2(startTime, endTime, br); break;
                        case EventType.Tae160: result = new Tae160(startTime, endTime, br); break;
                        case EventType.Tae161: result = new Tae161(startTime, endTime, br); break;
                        case EventType.SetDebugGhost: result = new Tae192_SetDebugGhost(startTime, endTime, br); break;
                        case EventType.FadeOut: result = new Tae193_FadeOut(startTime, endTime, br); break;
                        case EventType.Tae194: result = new Tae194(startTime, endTime, br); break;
                        case EventType.EnableBlurFeedback: result = new Tae195_EnableBlurFeedback(startTime, endTime, br); break;
                        case EventType.C_ARSN_BumpBlendDecal: result = new Tae196_C_ARSN_BumpBlendDecal(startTime, endTime, br); break;
                        case EventType.Ghost: result = new Tae197_Ghost(startTime, endTime, br); break;
                        case EventType.Tae224: result = new Tae224(startTime, endTime, br); break;
                        case EventType.ChangeStaminaRegen: result = new Tae225_ChangeStaminaRegen(startTime, endTime, br); break;
                        case EventType.Tae226: result = new Tae226(startTime, endTime, br); break;
                        case EventType.Tae227: result = new Tae227(startTime, endTime, br); break;
                        case EventType.RagdollReviveTime: result = new Tae228_RagdollReviveTime(startTime, endTime, br); break;
                        case EventType.CreateAISound1: result = new Tae229_CreateAISound1(startTime, endTime, br); break;
                        case EventType.Tae230: result = new Tae230(startTime, endTime, br); break;
                        case EventType.SetEzStateRequestId: result = new Tae231_SetEzStateRequestId(startTime, endTime, br); break;
                        case EventType.Tae232: result = new Tae232(startTime, endTime, br); break;
                        case EventType.ChangeDrawMask: result = new Tae233_ChangeDrawMask(startTime, endTime, br); break;
                        case EventType.Tae234: result = new Tae234(startTime, endTime, br); break;
                        case EventType.Tae235: result = new Tae235(startTime, endTime, br); break;
                        case EventType.RollDistanceReduction: result = new Tae236_RollDistanceReduction(startTime, endTime, br); break;
                        case EventType.CreateAISound2: result = new Tae237_CreateAISound2(startTime, endTime, br); break;
                        case EventType.Tae300: result = new Tae300(startTime, endTime, br); break;
                        case EventType.Padding: result = new Tae301_Padding(startTime, endTime, br); break;
                        case EventType.CreateSpEffect1: result = new Tae302_CreateSpEffect1(startTime, endTime, br); break;
                        case EventType.PlayAnimation: result = new Tae303_PlayAnimation(startTime, endTime, br); break;
                        case EventType.BehaviorThing: result = new Tae304_BehaviorThing(startTime, endTime, br); break;
                        case EventType.Tae305: result = new Tae305(startTime, endTime, br); break;
                        case EventType.Tae306: result = new Tae306(startTime, endTime, br); break;
                        case EventType.CreateBehaviorPC: result = new Tae307_CreateBehaviorPC(startTime, endTime, br); break;
                        case EventType.Tae308: result = new Tae308(startTime, endTime, br); break;
                        case EventType.Tae310: result = new Tae310(startTime, endTime, br); break;
                        case EventType.Tae311: result = new Tae311(startTime, endTime, br); break;
                        case EventType.Tae312: result = new Tae312(startTime, endTime, br); break;
                        case EventType.ActionRequest: result = new Tae320_ActionRequest(startTime, endTime, br); break;
                        case EventType.WeaponArtMpConsume: result = new Tae330_WeaponArtMpConsume(startTime, endTime, br); break;
                        case EventType.CreateSpEffect_SwordArt: result = new Tae331_CreateSpEffect_SwordArt(startTime, endTime, br); break;
                        case EventType.Tae332: result = new Tae332(startTime, endTime, br); break;
                        case EventType.CreateSpEffect2: result = new Tae401_CreateSpEffect2(startTime, endTime, br); break;
                        case EventType.CreateWind: result = new Tae402_CreateWind(startTime, endTime, br); break;
                        case EventType.Tae500: result = new Tae500(startTime, endTime, br); break;
                        case EventType.Tae510: result = new Tae510(startTime, endTime, br); break;
                        case EventType.Tae520: result = new Tae520(startTime, endTime, br); break;
                        case EventType.Tae521: result = new Tae521(startTime, endTime, br); break;
                        case EventType.SetSpecialLockOnParameter: result = new Tae522_SetSpecialLockOnParameter(startTime, endTime, br); break;
                        case EventType.EnableBehavior: result = new Tae600_EnableBehavior(startTime, endTime, br); break;
                        case EventType.Tae601: result = new Tae601(startTime, endTime, br); break;
                        case EventType.Tae602: result = new Tae602(startTime, endTime, br); break;
                        case EventType.DebugAnimSpeed: result = new Tae603_DebugAnimSpeed(startTime, endTime, br); break;
                        case EventType.TestParam: result = new Tae604_TestParam(startTime, endTime, br); break;
                        case EventType.Tae605: result = new Tae605(startTime, endTime, br); break;
                        case EventType.Tae606: result = new Tae606(startTime, endTime, br); break;
                        case EventType.Tae700: result = new Tae700(startTime, endTime, br); break;
                        case EventType.EnableTurningDirection: result = new Tae703_EnableTurningDirection(startTime, endTime, br); break;
                        case EventType.Tae704: result = new Tae704(startTime, endTime, br); break;
                        case EventType.FacingAngleCorrection: result = new Tae705_FacingAngleCorrection(startTime, endTime, br); break;
                        case EventType.Tae706: result = new Tae706(startTime, endTime, br); break;
                        case EventType.Tae707: result = new Tae707(startTime, endTime, br); break;
                        case EventType.HideWeapon: result = new Tae710_HideWeapon(startTime, endTime, br); break;
                        case EventType.HideModelMask: result = new Tae711_HideModelMask(startTime, endTime, br); break;
                        case EventType.DamageLevelModule: result = new Tae712_DamageLevelModule(startTime, endTime, br); break;
                        case EventType.ModelMask: result = new Tae713_ModelMask(startTime, endTime, br); break;
                        case EventType.DamageLevelFunction: result = new Tae714_DamageLevelFunction(startTime, endTime, br); break;
                        case EventType.Tae715: result = new Tae715(startTime, endTime, br); break;
                        case EventType.CultStart: result = new Tae720_CultStart(startTime, endTime, br); break;
                        case EventType.Tae730: result = new Tae730(startTime, endTime, br); break;
                        case EventType.Tae740: result = new Tae740(startTime, endTime, br); break;
                        case EventType.IFrameState: result = new Tae760_IFrameState(startTime, endTime, br); break;
                        case EventType.BonePos: result = new Tae770_BonePos(startTime, endTime, br); break;
                        case EventType.BoneFixOn1: result = new Tae771_BoneFixOn1(startTime, endTime, br); break;
                        case EventType.BoneFixOn2: result = new Tae772_BoneFixOn2(startTime, endTime, br); break;
                        case EventType.AddHeight: result = new Tae780_AddHeight(startTime, endTime, br); break;
                        case EventType.TurnLowerBody: result = new Tae781_TurnLowerBody(startTime, endTime, br); break;
                        case EventType.Tae782: result = new Tae782(startTime, endTime, br); break;
                        case EventType.SpawnChrFinderBullet: result = new Tae785_SpawnChrFinderBullet(startTime, endTime, br); break;
                        case EventType.SetMaxUndurationAngle: result = new Tae786_SetMaxUndurationAngle(startTime, endTime, br); break;
                        case EventType.Tae790: result = new Tae790(startTime, endTime, br); break;
                        case EventType.Tae791: result = new Tae791(startTime, endTime, br); break;
                        case EventType.HitEffect2: result = new Tae792_HitEffect2(startTime, endTime, br); break;
                        case EventType.CultSacrifice1: result = new Tae793_CultSacrifice1(startTime, endTime, br); break;
                        case EventType.SacrificeEmpty: result = new Tae794_SacrificeEmpty(startTime, endTime, br); break;
                        case EventType.Toughness: result = new Tae795_Toughness(startTime, endTime, br); break;
                        case EventType.CreateMenu: result = new Tae796_CreateMenu(startTime, endTime, br); break;
                        case EventType.CeremonyParamID: result = new Tae797_CeremonyParamID(startTime, endTime, br); break;
                        case EventType.CultSingle: result = new Tae798_CultSingle(startTime, endTime, br); break;
                        case EventType.CultEmpty2: result = new Tae799_CultEmpty2(startTime, endTime, br); break;
                        case EventType.Tae800: result = new Tae800(startTime, endTime, br); break;

                        default:
                            throw new NotImplementedException();
                    }

                    if (result.Type != type)
                    {
                        throw new InvalidProgramException("There is a typo in TAE3.Event.cs. Please bully me.");
                    }
                }
                br.StepOut();

                return result;
            }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            /// <summary>
            /// General-purpose event that calls different functions based on the first field.
            /// </summary>
            public class Tae000_JumpTable : Event // 000
            {
                public override EventType Type => EventType.JumpTable;

                public int JumpTableID { get; set; }
                public int Unk04 { get; set; }
                public int Unk08 { get; set; } //Extra Argument
                public short Unk0C { get; set; } //Extra Argument
                public short Unk0E { get; set; }

                /* Jumptable Enum
                 1 = Action Request,
                 3 = State_ShieldBlock,
                 4 = Action Request,
                 5 - State_Unk1,
                 6 = State_Unk2,
                 7 = State_Unk3,
                 8 = State_EnableRollIFrames,
                 9 = Action Request,
                 10 = Action Request,
                 11 = Action Request,
                 14 = Set Boolean to true,
                 15 = Set Some Values,
                 16 = Action Request,
                 17 = State_Unk4,
                 18 = State_Unk5,
                 19 = State_Unk6,
                 20 = State_Unk7,
                 21 = Action Request,
                 22 = Action Request,
                 23 = Set Some Value,
                 24 = State_unk8,
                 25 = Action Request,
                 26 = Action Request,
                 27 = State_Unk9,
                 28 = Set Value,
                 ...
                 124 = State_UnkX
                 */

                public Tae000_JumpTable(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae000_JumpTable(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    JumpTableID = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    Unk0C = br.ReadInt16();
                    Unk0E = br.ReadInt16();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(JumpTableID);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteInt16(Unk0C);
                    bw.WriteInt16(Unk0E);
                }

                public override string ToString()
                {
                    return $"{base.ToString()} : {JumpTableID}";
                }
            }

            public class Tae001 : Event // 001
            {
                public override EventType Type => EventType.Tae001;

                public int Unk00 { get; set; }
                public int Unk04 { get; set; }
                public int Condition { get; set; }
                public byte Unk0C { get; set; }
                public byte Unk0D { get; set; }
                public short StateInfo { get; set; }

                public Tae001(float startTime, float endTime) : base(startTime, endTime)
                {
                    Unk00 = 0;
                    Unk04 = 0;
                    Condition = 0;
                    Unk0C = 0;
                    Unk0D = 0;
                    StateInfo = 0;
                }

                public Tae001(float startTime, float endTime, int unk00, int unk04, int condition, byte unk0C, byte unk0D, short stateInfo) : base(startTime, endTime)
                {
                    Unk00 = unk00;
                    Unk04 = unk04;
                    Condition = condition;
                    Unk0C = unk0C;
                    Unk0D = unk0D;
                    StateInfo = stateInfo;
                }

                internal Tae001(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Condition = br.ReadInt32();
                    Unk0C = br.ReadByte();
                    Unk0D = br.ReadByte();
                    StateInfo = br.ReadInt16();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Condition);
                    bw.WriteByte(Unk0C);
                    bw.WriteByte(Unk0D);
                    bw.WriteInt16(StateInfo);
                }
            }

            public class Tae002 : Event // 002
            {
                public override EventType Type => EventType.Tae002;

                public int Unk00 { get; set; }
                public int Unk04 { get; set; }
                public int ChrAsmStyle { get; set; }
                public byte Unk0C { get; set; }
                public byte Unk0D { get; set; }
                public ushort Unk0E { get; set; }
                public ushort Unk10 { get; set; }

                public Tae002 (float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae002(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    ChrAsmStyle = br.ReadInt32();
                    Unk0C = br.ReadByte();
                    Unk0D = br.ReadByte();
                    Unk0E = br.ReadUInt16();
                    Unk10 = br.ReadUInt16();
                    br.AssertInt16(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(ChrAsmStyle);
                    bw.WriteByte(Unk0C);
                    bw.WriteByte(Unk0D);
                    bw.WriteUInt16(Unk0E);
                    bw.WriteUInt16(Unk10);
                    bw.WriteInt16(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae005 : Event // 005
            {
                public override EventType Type => EventType.Tae005;

                public int Unk00 { get; set; }
                public int Unk04 { get; set; }

                public Tae005(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae005(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(Unk04);
                }
            }

            public class Tae016 : Event // 016
            {
                public override EventType Type => EventType.Tae016;


                public Tae016(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae016(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime) { }

                internal override void WriteSpecific(BinaryWriterEx bw) { }
            }

            public class Tae017 : Event // Doesnt Exist
            {
                public override EventType Type => EventType.Tae017;


                public Tae017(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae017(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae024 : Event // Useless
            {
                public override EventType Type => EventType.Tae024;

                public int Unk00 { get; set; }
                public int Unk04 { get; set; }
                public int Unk08 { get; set; }
                public int Unk0C { get; set; }

                public Tae024(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae024(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    Unk0C = br.ReadInt32();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteInt32(Unk0C);
                }
            }

            public class Tae032_SwitchWeapon1 : Event // 032
            {
                public override EventType Type => EventType.SwitchWeapon1;

                public int SwitchState { get; set; }

                public Tae032_SwitchWeapon1(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae032_SwitchWeapon1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SwitchState = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SwitchState);
                    bw.WriteInt32(0);
                }
            }

            public class Tae033_SwitchWeapon2 : Event // 033
            {
                public override EventType Type => EventType.SwitchWeapon2;

                public int SwitchState { get; set; }

                public Tae033_SwitchWeapon2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae033_SwitchWeapon2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SwitchState = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SwitchState);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae034 : Event // 034
            {
                public override EventType Type => EventType.Tae034;

                public int State { get; set; }

                public Tae034(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae034(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    State = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(State);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae035 : Event // 035
            {
                public override EventType Type => EventType.Tae035;

                public int State { get; set; }

                public Tae035(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae035(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    State = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(State);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae064_CastSelectedSpell : Event // 064
            {
                public override EventType Type => EventType.CastSelectedSpell;

                public int Unk00 { get; set; }
                public ushort Unk04 { get; set; }
                public ushort Unk06 { get; set; }
                public byte Unk08 { get; set; }
                public byte Unk09 { get; set; }
                public byte Unk0A { get; set; }
                public byte Unk0B { get; set; }

                public Tae064_CastSelectedSpell(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae064_CastSelectedSpell(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadUInt16();
                    Unk06 = br.ReadUInt16();
                    Unk08 = br.ReadByte();
                    Unk09 = br.ReadByte();
                    Unk0A = br.ReadByte();
                    Unk0B = br.ReadByte();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteUInt16(Unk04);
                    bw.WriteUInt16(Unk06);
                    bw.WriteByte(Unk08);
                    bw.WriteByte(Unk09);
                    bw.WriteByte(Unk0A);
                    bw.WriteByte(Unk0B);
                    bw.WriteInt32(0);
                }
            }

            public class Tae065_ConsumeSelectedItem : Event // 065
            {
                public override EventType Type => EventType.ConsumeSelectedItem;

                public int Unk00 { get; set; }
                public byte HandIndexId { get; set; }
                public byte Unk05 { get; set; }
                public ushort Unk06 { get; set; }
                public int Unk08 { get; set; }

                public Tae065_ConsumeSelectedItem(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae065_ConsumeSelectedItem(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    HandIndexId = br.ReadByte();
                    Unk05 = br.ReadByte();
                    Unk06 = br.ReadUInt16();
                    Unk08 = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteByte(HandIndexId);
                    bw.WriteByte(Unk05);
                    bw.WriteUInt16(Unk06);
                    bw.WriteInt32(Unk08);
                    bw.WriteInt32(0);
                }
            }

            // During attack
            public class Tae066_CreateBehaviorSpEffect1 : Event // 066
            {
                public override EventType Type => EventType.CreateBehaviorSpEffect1;

                public int SpEffectID { get; set; }

                public Tae066_CreateBehaviorSpEffect1(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                public Tae066_CreateBehaviorSpEffect1(float startTime, float endTime, int speffectID) : base(startTime, endTime)
                {
                    SpEffectID = speffectID;
                }

                internal Tae066_CreateBehaviorSpEffect1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SpEffectID = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SpEffectID);
                    bw.WriteInt32(0);
                }
            }

            // During roll
            public class Tae067_CreateBehaviorSpEffect2 : Event // 067
            {
                public override EventType Type => EventType.CreateBehaviorSpEffect2;

                public int SpEffectID { get; set; }

                public Tae067_CreateBehaviorSpEffect2(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae067_CreateBehaviorSpEffect2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SpEffectID = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SpEffectID);
                    bw.WriteInt32(0);
                }
            }

            public class Tae095 : Event // play ffx?
            {
                public override EventType Type => EventType.Tae095;

                public int FFXID { get; set; }
                public int Unk04 { get; set; }
                public int Unk08 { get; set; }
                public sbyte State0 { get; set; }
                public sbyte State1 { get; set; }
                public sbyte GhostFFXCondition { get; set; }
                public byte Unk0F { get; set; }

                public Tae095(float startTime, float endTime) : base(startTime, endTime)
                {
                    
                }

                internal Tae095(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    FFXID = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    State0 = br.ReadSByte();
                    State1 = br.ReadSByte();
                    GhostFFXCondition = br.ReadSByte();
                    Unk0F = br.ReadByte();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(FFXID);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteSByte(State0);
                    bw.WriteSByte(State1);
                    bw.WriteSByte(GhostFFXCondition);
                    bw.WriteByte(Unk0F);
                }
            }

            public class Tae096_PlayFFX : Event // 096
            {
                public override EventType Type => EventType.PlayFFX;

                public int FFXID { get; set; }
                public int Unk04 { get; set; }
                public int Unk08 { get; set; }
                public sbyte State0 { get; set; }
                public sbyte State1 { get; set; }
                public sbyte GhostFFXCondition { get; set; }
                public byte Unk0F { get; set; }

                public Tae096_PlayFFX(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae096_PlayFFX(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    FFXID = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    State0 = br.ReadSByte();
                    State1 = br.ReadSByte();
                    GhostFFXCondition = br.ReadSByte();
                    Unk0F = br.ReadByte();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(FFXID);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteSByte(State0);
                    bw.WriteSByte(State1);
                    bw.WriteSByte(GhostFFXCondition);
                    bw.WriteByte(Unk0F);
                }
            }

            public class Tae099 : Event // 99
            {
                public override EventType Type => EventType.Tae099;

                public int Unk1 { get; set; }
                public int Unk2 { get; set; }
                public int Unk3 { get; set; }

                public Tae099(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae099(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk1 = br.ReadInt32();
                    Unk2 = br.ReadInt32();
                    Unk3 = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk1);
                    bw.WriteInt32(Unk2);
                    bw.WriteInt32(Unk3);
                    bw.WriteInt32(0);
                }
            }

            public class Tae110 : Event // 110
            {
                public override EventType Type => EventType.Tae110;

                public int ID { get; set; }

                public Tae110(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae110(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    ID = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(ID);
                    bw.WriteInt32(0);
                }
            }

            public class Tae112_HitEffect : Event // 112
            {
                public override EventType Type => EventType.HitEffect;

                public int Size { get; set; }
                public int Unk04 { get; set; }
                public int Unk08 { get; set; }

                public Tae112_HitEffect(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae112_HitEffect(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Size = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Size);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteInt32(0);
                }
            }

            public class Tae113 : Event // 113
            {
                public override EventType Type => EventType.Tae113;

                public Tae113(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae113(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae114 : Event // 114
            {
                public override EventType Type => EventType.Tae114;

                public int Unk00 { get; set; }
                public ushort Unk04 { get; set; }
                public ushort Unk06 { get; set; }
                public int Unk08 { get; set; }
                public byte Unk0C { get; set; }
                public sbyte Unk0D { get; set; }
                public sbyte Unk0E { get; set; }
                public byte Unk0F { get; set; }
                public byte Unk10 { get; set; }
                public byte Unk11 { get; set; }
                public short Unk12 { get; set; }


                public Tae114(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae114(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadUInt16();
                    Unk06 = br.ReadUInt16();
                    Unk08 = br.ReadInt32();
                    Unk0C = br.ReadByte();
                    Unk0D = br.ReadSByte();
                    Unk0E = br.ReadSByte();
                    Unk0F = br.ReadByte();
                    Unk10 = br.ReadByte();
                    Unk11 = br.ReadByte();
                    Unk12 = br.ReadInt16();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteUInt16(Unk04);
                    bw.WriteUInt16(Unk06);
                    bw.WriteInt32(Unk08);
                    bw.WriteByte(Unk0C);
                    bw.WriteSByte(Unk0D);
                    bw.WriteSByte(Unk0E);
                    bw.WriteByte(Unk0F);
                    bw.WriteByte(Unk10);
                    bw.WriteByte(Unk11);
                    bw.WriteInt16(Unk12);
                    bw.WriteInt32(0);
                }
            }

            public class Tae115 : Event // 115
            {
                public override EventType Type => EventType.Tae115;

                public int Unk00 { get; set; }
                public ushort Unk04 { get; set; }
                public ushort Unk06 { get; set; }
                public int Unk08 { get; set; }
                public byte Unk0C { get; set; }
                public byte Unk0D { get; set; }
                public byte Unk0E { get; set; }
                public byte Unk0F { get; set; }
                public byte Unk10 { get; set; }
                public byte Unk11 { get; set; }
                public short Unk12 { get; set; }


                public Tae115(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae115(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadUInt16();
                    Unk06 = br.ReadUInt16();
                    Unk08 = br.ReadInt32();
                    Unk0C = br.ReadByte();
                    Unk0D = br.ReadByte();
                    Unk0E = br.ReadByte();
                    Unk0F = br.ReadByte();
                    Unk10 = br.ReadByte();
                    Unk11 = br.ReadByte();
                    Unk12 = br.ReadInt16();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteUInt16(Unk04);
                    bw.WriteUInt16(Unk06);
                    bw.WriteInt32(Unk08);
                    bw.WriteByte(Unk0C);
                    bw.WriteByte(Unk0D);
                    bw.WriteByte(Unk0E);
                    bw.WriteByte(Unk0F);
                    bw.WriteByte(Unk10);
                    bw.WriteByte(Unk11);
                    bw.WriteInt16(Unk12);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae116 : Event // 116
            {
                public override EventType Type => EventType.Tae116;

                public int Unk00 { get; set; }
                public int Unk04 { get; set; }
                public int Unk08 { get; set; }
                public int Unk0C { get; set; }


                public Tae116(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae116(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    Unk0C = br.ReadInt32();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteInt32(Unk0C);
                }
            }

            public class Tae117 : Event // 117
            {
                public override EventType Type => EventType.Tae117;

                public int Unk00 { get; set; }
                public int Unk04 { get; set; }
                public int Unk08 { get; set; }
                public byte Unk0C { get; set; }
                public byte Unk0D { get; set; }
                public byte Unk0E { get; set; }
                public byte Unk0F { get; set; }

                public Tae117(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae117(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32(); // -1
                    Unk0C = br.ReadByte();
                    Unk0D = br.ReadByte();
                    Unk0E = br.ReadByte();
                    Unk0F = br.ReadByte();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteByte(Unk0C);
                    bw.WriteByte(Unk0D);
                    bw.WriteByte(Unk0E);
                    bw.WriteByte(Unk0F);
                }
            }

            public class Tae118 : Event // 118
            {
                public override EventType Type => EventType.Tae118;

                public int Unk00 { get; set; }
                public ushort Unk04 { get; set; }
                public ushort Unk06 { get; set; }
                public ushort Unk08 { get; set; }
                public ushort Unk0A { get; set; }


                public Tae118(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae118(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadUInt16();
                    Unk06 = br.ReadUInt16();
                    Unk08 = br.ReadUInt16();
                    Unk0A = br.ReadUInt16();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteUInt16(Unk04);
                    bw.WriteUInt16(Unk06);
                    bw.WriteUInt16(Unk08);
                    bw.WriteUInt16(Unk0A);
                    bw.WriteInt32(0);
                }
            }

            public class Tae119 : Event // 119
            {
                public override EventType Type => EventType.Tae119;

                public int Unk00 { get; set; }
                public int Unk04 { get; set; }
                public int Unk08 { get; set; }
                public byte Unk0C { get; set; }


                public Tae119(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae119(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    Unk0C = br.ReadByte(); // 0
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteByte(Unk0C);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                }
            }

            public class Tae120 : Event // 120
            {
                public override EventType Type => EventType.Tae120;

                public int ChrType { get; set; }
                public int[] FFXIDs { get; private set; }
                public int Unk30 { get; set; }
                public int Unk34 { get; set; }
                public byte Unk38 { get; set; }


                public Tae120(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae120(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    ChrType = br.ReadInt32();
                    FFXIDs = br.ReadInt32s(11);
                    Unk30 = br.ReadInt32();
                    Unk34 = br.ReadInt32();
                    Unk38 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(ChrType);
                    bw.WriteInt32s(FFXIDs);
                    bw.WriteInt32(Unk30);
                    bw.WriteInt32(Unk34);
                    bw.WriteByte(Unk38);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae121 : Event // Play FFX?
            {
                public override EventType Type => EventType.Tae121;

                public int FFXId { get; set; }
                public short DummyPolyId { get; set; }
                public byte SpType { get; set; }
                public byte Unk07 { get; set; }


                public Tae121(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae121(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    FFXId = br.ReadInt32();
                    DummyPolyId = br.ReadInt16();
                    SpType = br.ReadByte();
                    Unk07 = br.ReadByte();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(FFXId);
                    bw.WriteInt16(DummyPolyId);
                    bw.WriteByte(SpType );
                    bw.WriteByte(Unk07);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae122_PlayFFXIfSpEffect : Event // 122
            {
                public override EventType Type => EventType.PlayFFXIfSpEffect;

                public int FFXId { get; set; }
                public short DummyPolyId { get; set; }
                public short Unk3 { get; set; }
                public byte Unk4 { get; set; }
                public bool IsEnable { get; set; }
                public short CompareSpEffectId { get; set; }
                public byte Unk7 { get; set; }


                public Tae122_PlayFFXIfSpEffect(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae122_PlayFFXIfSpEffect(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    FFXId = br.ReadInt32();
                    DummyPolyId = br.ReadInt16();
                    Unk3 = br.ReadInt16();
                    Unk4 = br.ReadByte();
                    IsEnable = br.ReadBoolean();
                    CompareSpEffectId = br.ReadInt16();
                    Unk7 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(FFXId);
                    bw.WriteInt16(DummyPolyId);
                    bw.WriteInt16(Unk3);
                    bw.WriteByte(Unk4);
                    bw.WriteBoolean(IsEnable);
                    bw.WriteInt16(CompareSpEffectId);
                    bw.WriteByte(Unk7);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                }
            }

            public class Tae128_PlaySound1 : Event // 128
            {
                public override EventType Type => EventType.PlaySound1;

                public int SoundType { get; set; }
                public int SoundID { get; set; }


                public Tae128_PlaySound1(float startTime, float endTime) : base(startTime, endTime)
                {

                }


                internal Tae128_PlaySound1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SoundType = br.ReadInt32();
                    SoundID = br.ReadInt32();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SoundType);
                    bw.WriteInt32(SoundID);
                }
            }

            public class Tae129_PlaySound2 : Event // 129
            {
                public override EventType Type => EventType.PlaySound2;

                public int SoundType { get; set; }
                public int SoundID { get; set; }
                public int Unk08 { get; set; }
                public int Unk0C { get; set; }
                public int Unk10 { get; set; }


                public Tae129_PlaySound2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae129_PlaySound2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SoundType = br.ReadInt32();
                    SoundID = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    Unk0C = br.ReadInt32();
                    Unk10 = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SoundType);
                    bw.WriteInt32(SoundID);
                    bw.WriteInt32(Unk08);
                    bw.WriteInt32(Unk0C);
                    bw.WriteInt32(Unk10);
                    bw.WriteInt32(0);
                }
            }

            public class Tae130_PlaySound3 : Event // 130
            {
                public override EventType Type => EventType.PlaySound3;

                public int SoundType { get; set; }
                public int SoundID { get; set; }
                public float Unk08 { get; set; }
                public float Unk0C { get; set; }


                public Tae130_PlaySound3(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae130_PlaySound3(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SoundType = br.ReadInt32();
                    SoundID = br.ReadInt32();
                    Unk08 = br.ReadSingle();
                    Unk0C = br.ReadSingle(); // int -1
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SoundType);
                    bw.WriteInt32(SoundID);
                    bw.WriteSingle(Unk08);
                    bw.WriteSingle(Unk0C);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae131_PlaySound4 : Event // 131
            {
                public override EventType Type => EventType.PlaySound4;

                public int SoundType { get; set; }
                public int SoundID { get; set; }
                public int Unk08 { get; set; }


                public Tae131_PlaySound4(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae131_PlaySound4(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SoundType = br.ReadInt32();
                    SoundID = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SoundType);
                    bw.WriteInt32(SoundID);
                    bw.WriteInt32(Unk08);
                    bw.WriteInt32(0);
                }
            }

            public class Tae132_PlaySound5 : Event // 132
            {
                public override EventType Type => EventType.PlaySound5;

                public int SoundType { get; set; }
                public int SoundID { get; set; }


                public Tae132_PlaySound5(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae132_PlaySound5(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SoundType = br.ReadInt32();
                    SoundID = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SoundType);
                    bw.WriteInt32(SoundID);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae137_CreateDecal1 : Event // 137
            {
                public override EventType Type => EventType.CreateDecal1;

                public int DecalParamID { get; set; }

                public Tae137_CreateDecal1(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae137_CreateDecal1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    DecalParamID = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(DecalParamID);
                    bw.WriteInt32(0);
                }
            }

            public class Tae138_CreateDecal2 : Event // 138
            {
                public override EventType Type => EventType.CreateDecal2;

                public int DecalParamID { get; set; }
                public int FlverParameterId { get; set; }


                public Tae138_CreateDecal2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae138_CreateDecal2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    DecalParamID = br.ReadInt32();
                    FlverParameterId = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(DecalParamID);
                    bw.WriteInt32(FlverParameterId);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae144_CreateCameraShakeSpecial : Event // 144
            {
                public override EventType Type => EventType.CreateCameraShakeSpecial;

                public ushort RumbleId { get; set; }
                public ushort Unk02 { get; set; }
                public float Unk04 { get; set; }
                public float Unk08 { get; set; }


                public Tae144_CreateCameraShakeSpecial(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae144_CreateCameraShakeSpecial(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    RumbleId = br.ReadUInt16();
                    Unk02 = br.ReadUInt16();
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteUInt16(RumbleId);
                    bw.WriteUInt16(Unk02);
                    bw.WriteSingle(Unk04);
                    bw.WriteSingle(Unk08);
                    bw.WriteInt32(0);
                }
            }

            public class Tae145_CreateCameraShake : Event // 145
            {
                public override EventType Type => EventType.CreateCameraShake;

                public short Condition { get; set; }
                public short RumbleId { get; set; }

                public Tae145_CreateCameraShake(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae145_CreateCameraShake(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Condition = br.ReadInt16();
                    RumbleId = br.ReadInt16();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt16(Condition);
                    bw.WriteInt16(RumbleId);
                    bw.WriteInt32(0);
                }
            }

            public class Tae150_SetLockCamParam1 : Event // 150
            {
                public override EventType Type => EventType.SetLockCamParam1;

                public int LockCamParamId { get; set; }

                public Tae150_SetLockCamParam1(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae150_SetLockCamParam1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    LockCamParamId = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(LockCamParamId);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae151_SetLockCamParam2 : Event // 151
            {
                public override EventType Type => EventType.SetLockCamParam2;

                public int LockCamParamId { get; set; }

                public Tae151_SetLockCamParam2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae151_SetLockCamParam2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    LockCamParamId = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(LockCamParamId);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae160 : Event
            {
                public override EventType Type => EventType.Tae160;

                public Tae160(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae160(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae161 : Event // 161
            {
                public override EventType Type => EventType.Tae161;

                public Tae161(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae161(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae192_SetDebugGhost : Event // 193
            {
                public override EventType Type => EventType.SetDebugGhost;

                public float Ghost { get; set; }

                public Tae192_SetDebugGhost(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae192_SetDebugGhost(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Ghost = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Ghost);
                    bw.WriteInt32(0);
                }
            }

            public class Tae193_FadeOut : Event // 193
            {
                public override EventType Type => EventType.FadeOut;

                public float GhostVal1 { get; set; }
                public float GhostVal2 { get; set; }

                public Tae193_FadeOut(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae193_FadeOut(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    GhostVal1 = br.ReadSingle();
                    GhostVal2 = br.ReadSingle();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(GhostVal1);
                    bw.WriteSingle(GhostVal2);
                }
            }

            public class Tae194 : Event // Torch
            {
                public override EventType Type => EventType.Tae194;

                public ushort Unk00 { get; set; }
                public ushort Unk02 { get; set; }
                public ushort Unk04 { get; set; }
                public ushort Unk06 { get; set; }
                public float Unk08 { get; set; }

                public Tae194(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae194(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadUInt16();
                    Unk02 = br.ReadUInt16();
                    Unk04 = br.ReadUInt16();
                    Unk06 = br.ReadUInt16();
                    Unk08 = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteUInt16(Unk00);
                    bw.WriteUInt16(Unk02);
                    bw.WriteUInt16(Unk04);
                    bw.WriteUInt16(Unk06);
                    bw.WriteSingle(Unk08);
                    bw.WriteInt32(0);
                }
            }

            public class Tae195_EnableBlurFeedback : Event 
            {
                public override EventType Type => EventType.EnableBlurFeedback;

                public Tae195_EnableBlurFeedback(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae195_EnableBlurFeedback(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae196_C_ARSN_BumpBlendDecal : Event
            {
                public override EventType Type => EventType.C_ARSN_BumpBlendDecal;

                public long Unk1 { get; set; }
                public int Unk2 { get; set; }
                public int Unk3 { get; set; }
                public int Unk4_pad { get; set; }
                public int Unk5 { get; set; }
                public byte Unk6 { get; set; }

                public Tae196_C_ARSN_BumpBlendDecal(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae196_C_ARSN_BumpBlendDecal(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk1 = br.ReadInt64();
                    Unk2 = br.ReadInt32();
                    Unk3 = br.ReadInt32();
                    Unk4_pad = br.ReadInt32();
                    Unk5 = br.ReadInt32();
                    Unk6 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt64(Unk1);
                    bw.WriteInt32(Unk2);
                    bw.WriteInt32(Unk3);
                    bw.WriteInt32(Unk4_pad);
                    bw.WriteInt32(Unk5);
                    bw.WriteByte(Unk6);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae197_Ghost : Event // 194
            {
                public override EventType Type => EventType.Ghost;

                public float GhostMain { get; set; }
                public float GhostSub { get; set; }

                public Tae197_Ghost(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae197_Ghost(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    GhostMain = br.ReadSingle();
                    GhostSub = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(GhostMain);
                    bw.WriteSingle(GhostSub);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae224 : Event // 224
            {
                public override EventType Type => EventType.Tae224;

                public float Unk00 { get; set; }
                public bool IsCheckForLockOnState { get; set; }

                public Tae224(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae224(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    IsCheckForLockOnState = br.ReadBoolean();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteBoolean(IsCheckForLockOnState);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                }
            }

            public class Tae225_ChangeStaminaRegen : Event // 225
            {
                public override EventType Type => EventType.ChangeStaminaRegen;

                // "0x64 - Enables Regen Back" -Pav
                public byte State { get; set; }

                public Tae225_ChangeStaminaRegen(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae225_ChangeStaminaRegen(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    State = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(State);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae226 : Event // 226
            {
                public override EventType Type => EventType.Tae226;

                // "x/100 Coefficient" -Pav
                public byte State { get; set; }

                public Tae226(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae226(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    State = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(State);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae227 : Event // 227
            {
                public override EventType Type => EventType.Tae227;

                public int Mask { get; set; }

                public Tae227(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae227(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Mask = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Mask);
                    bw.WriteInt32(0);
                }
            }

            public class Tae228_RagdollReviveTime : Event // 228
            {
                public override EventType Type => EventType.RagdollReviveTime;

                public float Unk00 { get; set; }
                public float ReviveTimer { get; set; }

                public Tae228_RagdollReviveTime(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae228_RagdollReviveTime(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    ReviveTimer = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteSingle(ReviveTimer);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae229_CreateAISound1 : Event // 229
            {
                public override EventType Type => EventType.CreateAISound1;

                public int AiSoundID { get; set; }

                public Tae229_CreateAISound1(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae229_CreateAISound1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    AiSoundID = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(AiSoundID);
                    bw.WriteInt32(0);
                }
            }

            public class Tae230 : Event 
            {
                public override EventType Type => EventType.Tae230;

                public byte Unk00 { get; set; }

                public Tae230(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae230(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(Unk00);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae231_SetEzStateRequestId : Event // 231
            {
                public override EventType Type => EventType.SetEzStateRequestId;

                public int EzStateRequestId { get; set; }

                public Tae231_SetEzStateRequestId(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae231_SetEzStateRequestId(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    EzStateRequestId = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(EzStateRequestId);
                    bw.WriteInt32(0);
                }
            }

            public class Tae232 : Event // 232
            {
                public override EventType Type => EventType.Tae232;

                public byte Unk00 { get; set; }
                public byte Unk01 { get; set; }
                public byte Unk02 { get; set; }
                public byte Unk03 { get; set; }

                public Tae232(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae232(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadByte();
                    Unk01 = br.ReadByte();
                    Unk02 = br.ReadByte();
                    Unk03 = br.ReadByte();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(Unk00);
                    bw.WriteByte(Unk01);
                    bw.WriteByte(Unk02);
                    bw.WriteByte(Unk03);
                    bw.WriteInt32(0);
                }
            }

            public class Tae233_ChangeDrawMask : Event // 233
            {
                public override EventType Type => EventType.ChangeDrawMask;

                public byte[] DrawMask { get; private set; }

                public Tae233_ChangeDrawMask(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae233_ChangeDrawMask(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    DrawMask = br.ReadBytes(32);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBytes(DrawMask);
                }
            }

            public class Tae234 : Event // Havok?
            {
                public override EventType Type => EventType.Tae234;

                public int Unk00 { get; private set; }

                public Tae234(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae234(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(0);
                }
            }

            public class Tae235 : Event 
            {
                public override EventType Type => EventType.Tae235;

                public byte[] Mask { get; private set; }

                public Tae235(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae235(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Mask = br.ReadBytes(32);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBytes(Mask);
                }
            }

            public class Tae236_RollDistanceReduction : Event // 236
            {
                public override EventType Type => EventType.RollDistanceReduction;

                public float Unk00 { get; set; }
                public float Unk04 { get; set; }
                public bool RollType { get; set; }

                public Tae236_RollDistanceReduction(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae236_RollDistanceReduction(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    Unk04 = br.ReadSingle();
                    RollType = br.ReadBoolean();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteSingle(Unk04);
                    bw.WriteBoolean(RollType);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae237_CreateAISound2 : Event // 237
            {
                public override EventType Type => EventType.CreateAISound2;

                public int AISoundID { get; set; }

                public Tae237_CreateAISound2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae237_CreateAISound2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    AISoundID = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(AISoundID);
                    bw.WriteInt32(0);
                }
            }

            public class Tae300 : Event // 300
            {
                public override EventType Type => EventType.Tae300;

                public short JumpTableID_ActionRequest { get; set; }
                public short JumpTableID_GetFloat { get; set; }
                public float Unk04 { get; set; }
                public float Unk08 { get; set; }
                public int Unk0C { get; set; }

                public Tae300(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae300(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    JumpTableID_ActionRequest = br.ReadInt16();
                    JumpTableID_GetFloat = br.ReadInt16();
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadSingle();
                    Unk0C = br.ReadInt32();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt16(JumpTableID_ActionRequest);
                    bw.WriteInt16(JumpTableID_GetFloat);
                    bw.WriteSingle(Unk04);
                    bw.WriteSingle(Unk08);
                    bw.WriteInt32(Unk0C);
                }
            }

            public class Tae301_Padding : Event // 301
            {
                public override EventType Type => EventType.Padding;

                public int pad;

                public Tae301_Padding(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae301_Padding(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    pad = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(pad);
                    bw.WriteInt32(0);
                }
            }

            public class Tae302_CreateSpEffect1 : Event // 302
            {
                //hardcoded argument = 0
                public override EventType Type => EventType.CreateSpEffect1;

                public int SpEffectID { get; set; }

                public Tae302_CreateSpEffect1(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae302_CreateSpEffect1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SpEffectID = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SpEffectID);
                    bw.WriteInt32(0);
                }
            }

            public class Tae303_PlayAnimation : Event // 303
            {
                public override EventType Type => EventType.PlayAnimation;

                public int AnimationID { get; set; }

                public Tae303_PlayAnimation(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae303_PlayAnimation(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    AnimationID = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(AnimationID);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae304_BehaviorThing : Event // 304
            {
                public override EventType Type => EventType.BehaviorThing;

                public ushort Unk00 { get; set; }
                public short Unk02 { get; set; }
                public int BehaviorParamSubId { get; set; }


                public Tae304_BehaviorThing(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae304_BehaviorThing(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadUInt16();
                    Unk02 = br.ReadInt16();
                    BehaviorParamSubId = br.ReadInt32();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteUInt16(Unk00);
                    bw.WriteInt16(Unk02);
                    bw.WriteInt32(BehaviorParamSubId);
                }
            }

            public class Tae305 : Event // 303
            {
                public override EventType Type => EventType.Tae305;

                public float CompareFloat { get; set; }


                public Tae305(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae305(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    CompareFloat = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(CompareFloat);
                    bw.WriteInt32(0);
                }
            }

            public class Tae306 : Event // 303
            {
                public override EventType Type => EventType.Tae306;

                public float Unk00 { get; set; }
                public float Unk04 { get; set; }
                public byte flags { get; set; }


                public Tae306(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae306(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    Unk04 = br.ReadSingle();
                    flags = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteSingle(Unk04);
                    bw.WriteByte(flags);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae307_CreateBehaviorPC : Event // 307
            {
                public override EventType Type => EventType.CreateBehaviorPC;

                public short Unk00 { get; set; }
                public short Unk02 { get; set; }
                public int Condition { get; set; }
                public int BehaviorParamSubId { get; set; }

                public Tae307_CreateBehaviorPC(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae307_CreateBehaviorPC(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt16();
                    Unk02 = br.ReadInt16();
                    Condition = br.ReadInt32();
                    BehaviorParamSubId = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt16(Unk00);
                    bw.WriteInt16(Unk02);
                    bw.WriteInt32(Condition);
                    bw.WriteInt32(BehaviorParamSubId);
                    bw.WriteInt32(0);
                }
            }

            public class Tae308 : Event // Ragdoll ??
            {
                public override EventType Type => EventType.Tae308;

                public float Unk00 { get; set; }

                public Tae308(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae308(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            // "Behavior?" -Pav
            public class Tae310 : Event // 310
            {
                public override EventType Type => EventType.Tae310;

                public byte Unk00 { get; set; }
                public byte Unk01 { get; set; }

                public Tae310(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae310(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadByte();
                    Unk01 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(Unk00);
                    bw.WriteByte(Unk01);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae311 : Event // 311
            {
                public override EventType Type => EventType.Tae311;

                public byte Unk00 { get; set; }
                public byte Unk01 { get; set; }
                public byte Unk02 { get; set; }

                public Tae311(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae311(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadByte();
                    Unk01 = br.ReadByte();
                    Unk02 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(Unk00);
                    bw.WriteByte(Unk01);
                    bw.WriteByte(Unk02);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae312 : Event // 312
            {
                public override EventType Type => EventType.Tae312;

                public byte[] BehaviorMask { get; private set; }

                public Tae312(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae312(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    BehaviorMask = br.ReadBytes(32);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBytes(BehaviorMask);
                }
            }

            public class Tae320_ActionRequest : Event // 320
            {
                public override EventType Type => EventType.ActionRequest;

                public bool ActRequest1 { get; set; }
                public bool ActRequest2 { get; set; }
                public bool ActRequest3 { get; set; }
                public bool ActRequest4 { get; set; }
                public bool ActRequest5 { get; set; }
                public bool ActRequest6 { get; set; }
                public bool ActRequest7 { get; set; }

                public Tae320_ActionRequest(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae320_ActionRequest(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    ActRequest1 = br.ReadBoolean();
                    ActRequest2 = br.ReadBoolean();
                    ActRequest3 = br.ReadBoolean();
                    ActRequest4 = br.ReadBoolean();
                    ActRequest5 = br.ReadBoolean();
                    ActRequest6 = br.ReadBoolean();
                    ActRequest7 = br.ReadBoolean();
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBoolean(ActRequest1);
                    bw.WriteBoolean(ActRequest2);
                    bw.WriteBoolean(ActRequest3);
                    bw.WriteBoolean(ActRequest4);
                    bw.WriteBoolean(ActRequest5);
                    bw.WriteBoolean(ActRequest6);
                    bw.WriteBoolean(ActRequest7);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae330_WeaponArtMpConsume : Event // 330
            {
                public override EventType Type => EventType.WeaponArtMpConsume;

                public Tae330_WeaponArtMpConsume(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae330_WeaponArtMpConsume(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae331_CreateSpEffect_SwordArt : Event // 331
            {
                public override EventType Type => EventType.CreateSpEffect_SwordArt;

                public int SpEffectId { get; set; }
                public int SpEffectId_ForWeaponArt { get; set; }

                public Tae331_CreateSpEffect_SwordArt(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae331_CreateSpEffect_SwordArt(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SpEffectId = br.ReadInt32();
                    SpEffectId_ForWeaponArt = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SpEffectId);
                    bw.WriteInt32(SpEffectId_ForWeaponArt);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae332 : Event // 332
            {
                public override EventType Type => EventType.Tae332;

                public Tae332(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae332(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae401_CreateSpEffect2 : Event // 401
            {
                //hardcoded argument = 1
                public override EventType Type => EventType.CreateSpEffect2;

                public int SpEffectID { get; set; }

                public Tae401_CreateSpEffect2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                public Tae401_CreateSpEffect2(float startTime, float endTime, int effectId) : base(startTime, endTime)
                {
                    SpEffectID = effectId;
                }

                internal Tae401_CreateSpEffect2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SpEffectID = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SpEffectID);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae402_CreateWind : Event
            {
                public override EventType Type => EventType.CreateWind;

                public int WindParamId { get; set; }
                public short Unk04 { get; set; }

                public Tae402_CreateWind(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae402_CreateWind(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    WindParamId = br.ReadInt32();
                    Unk04 = br.ReadInt16();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(WindParamId);
                    bw.WriteInt16(Unk04);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae500 : Event // 500
            {
                public override EventType Type => EventType.Tae500;

                public byte Unk00 { get; set; } //0 - 31
                public byte Unk01 { get; set; }

                public Tae500(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae500(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadByte();
                    Unk01 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(Unk00);
                    bw.WriteByte(Unk01);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae510 : Event // Chr Stagger struct
            {
                public override EventType Type => EventType.Tae510;

                public Tae510(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae510(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae520 : Event // 520
            {
                public override EventType Type => EventType.Tae520;

                public Tae520(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae520(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae521 : Event
            {
                public override EventType Type => EventType.Tae521;

                public int Unk00 { get; set; }

                public Tae521(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae521(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae522_SetSpecialLockOnParameter : Event // 522
            {
                public override EventType Type => EventType.SetSpecialLockOnParameter;

                public float TargetChrParameter { get; set; }

                public Tae522_SetSpecialLockOnParameter(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae522_SetSpecialLockOnParameter(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    TargetChrParameter = br.ReadSingle(); // 0
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(TargetChrParameter);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae600_EnableBehavior : Event // 600
            {
                public override EventType Type => EventType.EnableBehavior;

                public int Mask { get; set; }

                public Tae600_EnableBehavior(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae600_EnableBehavior(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Mask = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Mask);
                    bw.WriteInt32(0);
                }
            }

            public class Tae601 : Event // 601
            {
                public override EventType Type => EventType.Tae601;

                public int StayAnimType { get; set; } //0 - 10
                public float Unk04 { get; set; }
                public float Unk08 { get; set; }

                public Tae601(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae601(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    StayAnimType = br.ReadInt32();
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(StayAnimType);
                    bw.WriteSingle(Unk04);
                    bw.WriteSingle(Unk08);
                    bw.WriteInt32(0);
                }
            }

            public class Tae602 : Event // 602
            {
                public override EventType Type => EventType.Tae602;

                public int StayAnimType { get; set; } //0 - 5
                public float Unk04 { get; set; }
                public float Unk08 { get; set; }

                public Tae602(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae602(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    StayAnimType = br.ReadInt32();
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(StayAnimType);
                    bw.WriteSingle(Unk04);
                    bw.WriteSingle(Unk08);
                    bw.WriteInt32(0);
                }
            }

            // "TAE Debug Anim Speed" -Pav
            public class Tae603_DebugAnimSpeed : Event // 603
            {
                public override EventType Type => EventType.DebugAnimSpeed;

                public uint AnimSpeed { get; set; }

                public Tae603_DebugAnimSpeed(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae603_DebugAnimSpeed(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    AnimSpeed = br.ReadUInt32();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteUInt32(AnimSpeed);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }
            
            public class Tae604_TestParam : Event
            {
                public override EventType Type => EventType.TestParam;


                public Tae604_TestParam(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae604_TestParam(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                }
            }
            
            
            public class Tae605 : Event // 605
            {
                public override EventType Type => EventType.Tae605;

                public bool IsCompareGender { get; set; } //IsCheckGender (1 byte + 3 padding)
                public int Unk04 { get; set; }
                public float Unk08 { get; set; }
                public float Unk0C { get; set; }

                public Tae605(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae605(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    IsCompareGender = br.ReadBoolean();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadSingle();
                    Unk0C = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBoolean(IsCompareGender);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(Unk04);
                    bw.WriteSingle(Unk08);
                    bw.WriteSingle(Unk0C);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }
            

            public class Tae606 : Event // 606 Foot Effect
            {
                public override EventType Type => EventType.Tae606;

                public byte JigglerDampingRate { get; set; }
                public short Unk04 { get; set; }
                public short Unk06 { get; set; }

                public Tae606(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae606(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    JigglerDampingRate = br.ReadByte(); // 0
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    Unk04 = br.ReadInt16();
                    Unk06 = br.ReadInt16();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(JigglerDampingRate);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt16(Unk04);
                    bw.WriteInt16(Unk06);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae700 : Event // 700
            {
                public override EventType Type => EventType.Tae700;

                public float Unk00 { get; set; }
                public float Unk04 { get; set; }
                public float Unk08 { get; set; }
                public float Unk0C { get; set; }
                public int Unk10 { get; set; }
                // 6 - head turn
                public sbyte Unk14 { get; set; }
                public float Unk18 { get; set; }
                public float Unk1C { get; set; }
                public float Unk20 { get; set; }
                public float Unk24 { get; set; }

                public Tae700(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae700(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadSingle();
                    Unk0C = br.ReadSingle();
                    Unk10 = br.ReadInt32();
                    Unk14 = br.ReadSByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    Unk18 = br.ReadSingle();
                    Unk1C = br.ReadSingle();
                    Unk20 = br.ReadSingle();
                    Unk24 = br.ReadSingle();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteSingle(Unk04);
                    bw.WriteSingle(Unk08);
                    bw.WriteSingle(Unk0C);
                    bw.WriteInt32(Unk10);
                    bw.WriteSByte(Unk14);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteSingle(Unk18);
                    bw.WriteSingle(Unk1C);
                    bw.WriteSingle(Unk20);
                    bw.WriteSingle(Unk24);
                }
            }

            public class Tae703_EnableTurningDirection : Event // 703
            {
                public override EventType Type => EventType.EnableTurningDirection;

                public bool State { get; set; }

                public Tae703_EnableTurningDirection(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae703_EnableTurningDirection(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    State = br.ReadBoolean();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBoolean(State);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae704 : Event
            {
                public override EventType Type => EventType.EnableTurningDirection;

                public float Unk00 { get; set; }
                public float Unk04 { get; set; }
                public float Unk08 { get; set; }

                public Tae704(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae704(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteSingle(Unk04);
                    bw.WriteSingle(Unk08);
                    bw.WriteInt32(0);
                }
            }

            public class Tae705_FacingAngleCorrection : Event // 705
            {
                public override EventType Type => EventType.FacingAngleCorrection;

                public float CorrectionRate { get; set; }

                public Tae705_FacingAngleCorrection(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae705_FacingAngleCorrection(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    CorrectionRate = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(CorrectionRate);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae706 : Event // Chr Stagger struct
            {
                public override EventType Type => EventType.FacingAngleCorrection;

                public float Unk00 { get; set; }

                public Tae706(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae706(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae707 : Event // Chr Stagger Struct
            {
                public override EventType Type => EventType.Tae707;

                public Tae707(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae707(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            // Used for Follower's Javelin WA
            // "Ladder State" -Pav
            public class Tae710_HideWeapon : Event // 710
            {
                public override EventType Type => EventType.HideWeapon;

                public byte Unk00 { get; set; }
                public byte Unk01 { get; set; }
                public byte Unk02 { get; set; }
                public byte Unk03 { get; set; }

                public Tae710_HideWeapon(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae710_HideWeapon(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadByte();
                    Unk01 = br.ReadByte();
                    Unk02 = br.ReadByte();
                    Unk03 = br.ReadByte();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(Unk00);
                    bw.WriteByte(Unk01);
                    bw.WriteByte(Unk02);
                    bw.WriteByte(Unk03);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae711_HideModelMask : Event // 711
            {
                public override EventType Type => EventType.HideModelMask;

                public byte[] Mask { get; private set; }

                public Tae711_HideModelMask(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae711_HideModelMask(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Mask = br.ReadBytes(32);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBytes(Mask);
                }
            }

            public class Tae712_DamageLevelModule : Event // 712
            {
                public override EventType Type => EventType.DamageLevelModule;

                public byte[] Mask { get; private set; }
                public byte Unk10 { get; set; }
                public byte Unk11 { get; set; }
                public byte Unk12 { get; set; }

                public Tae712_DamageLevelModule(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae712_DamageLevelModule(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Mask = br.ReadBytes(16);
                    Unk10 = br.ReadByte();
                    Unk11 = br.ReadByte();
                    Unk12 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBytes(Mask);
                    bw.WriteByte(Unk10);
                    bw.WriteByte(Unk11);
                    bw.WriteByte(Unk12);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae713_ModelMask : Event // 713
            {
                public override EventType Type => EventType.ModelMask;

                public byte[] Mask { get; private set; }

                public Tae713_ModelMask(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae713_ModelMask(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Mask = br.ReadBytes(32);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBytes(Mask);
                }
            }

            public class Tae714_DamageLevelFunction : Event // 714
            {
                public override EventType Type => EventType.DamageLevelFunction;

                public bool Unk00 { get; set; }

                public Tae714_DamageLevelFunction(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae714_DamageLevelFunction(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadBoolean();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteBoolean(Unk00);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae715 : Event // 715
            {
                public override EventType Type => EventType.Tae715;

                public byte Unk00 { get; set; }
                public byte Unk01 { get; set; }
                public byte Unk02 { get; set; }
                public byte Unk03 { get; set; }
                public byte Unk04 { get; set; }
                public byte Pad1;
                public byte Pad2;
                public byte Pad3;

                public Tae715(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae715(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadByte();
                    Unk01 = br.ReadByte();
                    Unk02 = br.ReadByte();
                    Unk03 = br.ReadByte();
                    Unk04 = br.ReadByte();
                    Pad1 = br.ReadByte();
                    Pad2 = br.ReadByte();
                    Pad3 = br.ReadByte();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(Unk00);
                    bw.WriteByte(Unk01);
                    bw.WriteByte(Unk02);
                    bw.WriteByte(Unk03);
                    bw.WriteByte(Unk04);
                    bw.WriteByte(Pad1);
                    bw.WriteByte(Pad2);
                    bw.WriteByte(Pad3);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae720_CultStart : Event // 720
            {
                public override EventType Type => EventType.CultStart;

                public byte CultType { get; set; } //0,1

                public Tae720_CultStart(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae720_CultStart(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    CultType = br.ReadByte(); // 0
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(CultType);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae730 : Event // 730
            {
                public override EventType Type => EventType.Tae730;

                public float Unk00 { get; set; }
                public float Unk04 { get; set; }

                public Tae730(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae730(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    Unk04 = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteSingle(Unk04);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae740 : Event // 740
            {
                public override EventType Type => EventType.Tae740;

                public Tae740(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae740(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae760_IFrameState : Event // 760
            {
                public override EventType Type => EventType.IFrameState;

                public byte Unk00 { get; set; }
                public float Unk04 { get; set; }
                public float Unk08 { get; set; }
                public float Unk0C { get; set; }
                public float Unk10 { get; set; }
                public float Unk14 { get; set; }
                public byte Pad1;
                public byte Pad2;
                public byte Pad3;

                public Tae760_IFrameState(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae760_IFrameState(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadByte();
                    Pad1 = br.ReadByte();
                    Pad2 = br.ReadByte();
                    Pad3 = br.ReadByte();
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadSingle();
                    Unk0C = br.ReadSingle();
                    Unk10 = br.ReadSingle();
                    Unk14 = br.ReadSingle();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(Unk00);
                    bw.WriteByte(Pad1);
                    bw.WriteByte(Pad2);
                    bw.WriteByte(Pad3);
                    bw.WriteSingle(Unk04);
                    bw.WriteSingle(Unk08);
                    bw.WriteSingle(Unk0C);
                    bw.WriteSingle(Unk10);
                    bw.WriteSingle(Unk14);
                }
            }

            public class Tae770_BonePos : Event // 770
            {
                public override EventType Type => EventType.BonePos;

                public int Unk00 { get; set; }
                public float Unk04 { get; set; }
                public byte Unk08 { get; set; }

                public Tae770_BonePos(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae770_BonePos(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32();
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteSingle(Unk04);
                    bw.WriteByte(Unk08);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae771_BoneFixOn1 : Event // 771
            {
                public override EventType Type => EventType.BoneFixOn1;

                public byte BoneID { get; set; }

                public Tae771_BoneFixOn1(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae771_BoneFixOn1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    BoneID = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(BoneID);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae772_BoneFixOn2 : Event // 772
            {
                public override EventType Type => EventType.BoneFixOn2;

                public int Unk00 { get; set; }
                public float Unk04 { get; set; }
                public byte Unk08 { get; set; }

                public Tae772_BoneFixOn2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae772_BoneFixOn2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadInt32(); 
                    Unk04 = br.ReadSingle();
                    Unk08 = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(Unk00);
                    bw.WriteSingle(Unk04);
                    bw.WriteByte(Unk08);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae780_AddHeight : Event 
            {
                public override EventType Type => EventType.AddHeight;

                public float Height { get; set; }

                public Tae780_AddHeight(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae780_AddHeight(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Height = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Height);
                    bw.WriteInt32(0);
                }
            }

            public class Tae781_TurnLowerBody : Event // 781
            {
                public override EventType Type => EventType.TurnLowerBody;

                public byte TurnState { get; set; }

                public Tae781_TurnLowerBody(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae781_TurnLowerBody(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    TurnState = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(TurnState);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae782 : Event // ChrStaggerStructure
            {
                public override EventType Type => EventType.Tae782;

                public Tae782(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae782(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae785_SpawnChrFinderBullet : Event // 785
            {
                public override EventType Type => EventType.SpawnChrFinderBullet;

                public float DetectionRange { get; set; }
                public int DummyPointID { get; set; }
                public int BulletID { get; set; }
                public bool IsCompareChrType { get; set; }
                public byte TargetNum { get; set; }

                public Tae785_SpawnChrFinderBullet(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae785_SpawnChrFinderBullet(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    DetectionRange = br.ReadSingle();
                    DummyPointID = br.ReadInt32();
                    BulletID = br.ReadInt32();
                    IsCompareChrType = br.ReadBoolean();
                    TargetNum = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(DetectionRange);
                    bw.WriteInt32(DummyPointID);
                    bw.WriteInt32(BulletID);
                    bw.WriteBoolean(IsCompareChrType);
                    bw.WriteByte(TargetNum);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                }
            }

            public class Tae786_SetMaxUndurationAngle : Event // NpcParam
            {
                public override EventType Type => EventType.SetMaxUndurationAngle;

                public float UndurationAngle { get; set; }

                public Tae786_SetMaxUndurationAngle(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae786_SetMaxUndurationAngle(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    UndurationAngle = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(UndurationAngle);
                    bw.WriteInt32(0);
                }
            }

            public class Tae790 : Event // 790 ChrStaggerStruct
            {
                public override EventType Type => EventType.Tae790;

                public Tae790(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae790(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae791 : Event // 791 ChrStaggerStructure
            {
                public override EventType Type => EventType.Tae791;

                public Tae791(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae791(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae792_HitEffect2 : Event // 792
            {
                public override EventType Type => EventType.HitEffect2;

                public short FootSfxIndex { get; set; } //0 - 199
                public int Unk04 { get; set; }
                public int Unk08 { get; set; }
                public bool Unk0C { get; set; }
                public bool Unk0D { get; set; }
                public bool Unk0E { get; set; }
                public byte Unk0F { get; set; }

                public Tae792_HitEffect2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae792_HitEffect2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime) //FootSfx
                {
                    FootSfxIndex = br.ReadInt16();
                    br.AssertInt16(0);
                    Unk04 = br.ReadInt32();
                    Unk08 = br.ReadInt32();
                    Unk0C = br.ReadBoolean();
                    Unk0D = br.ReadBoolean();
                    Unk0E = br.ReadBoolean();
                    Unk0F = br.ReadByte();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt16(FootSfxIndex);
                    bw.WriteInt16(0);
                    bw.WriteInt32(Unk04);
                    bw.WriteInt32(Unk08);
                    bw.WriteBoolean(Unk0C);
                    bw.WriteBoolean(Unk0D);
                    bw.WriteBoolean(Unk0E);
                    bw.WriteByte(Unk0F);
                }
            }

            public class Tae793_CultSacrifice1 : Event // SetEventSystemParameter For c0000
            {
                public override EventType Type => EventType.CultSacrifice1;

                public int SacrificeValue { get; set; }

                public Tae793_CultSacrifice1(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae793_CultSacrifice1(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    SacrificeValue = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(SacrificeValue);
                    bw.WriteInt32(0);
                }
            }

            public class Tae794_SacrificeEmpty : Event // ChrStaggerStructure
            {
                public override EventType Type => EventType.SacrificeEmpty;

                public Tae794_SacrificeEmpty(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae794_SacrificeEmpty(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae795_Toughness : Event // 795
            {
                public override EventType Type => EventType.Toughness;

                public byte ToughnessParamID { get; set; }
                public byte ToughnessType { get; set; } //0,1,2
                public float DamageRatio { get; set; }

                public Tae795_Toughness(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae795_Toughness(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    ToughnessParamID = br.ReadByte();
                    ToughnessType = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    DamageRatio = br.ReadSingle();
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(ToughnessParamID);
                    bw.WriteByte(ToughnessType);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteSingle(DamageRatio);
                }
            }

            public class Tae796_CreateMenu : Event // 796
            {
                public override EventType Type => EventType.CreateMenu;

                public byte MenuType { get; set; } //0 - lvlup menu; 1 - reloallocate attributes

                public Tae796_CreateMenu(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae796_CreateMenu(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    MenuType = br.ReadByte();
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertByte(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteByte(MenuType);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteByte(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae797_CeremonyParamID : Event //Cult Item Id?
            {
                public override EventType Type => EventType.CeremonyParamID;

                public int ParamID { get; set; }

                public Tae797_CeremonyParamID(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae797_CeremonyParamID(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    ParamID = br.ReadInt32();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(ParamID);
                    bw.WriteInt32(0);
                }
            }

            public class Tae798_CultSingle : Event // CultId?
            {
                public override EventType Type => EventType.CultSingle;

                public float Unk00 { get; set; }

                public Tae798_CultSingle(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae798_CultSingle(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    Unk00 = br.ReadSingle();
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(Unk00);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae799_CultEmpty2 : Event // Cult Sp State?
            {
                public override EventType Type => EventType.CultEmpty2;

                public Tae799_CultEmpty2(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae799_CultEmpty2(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                    bw.WriteInt32(0);
                }
            }

            public class Tae800 : Event // FDP DATA
            {
                public override EventType Type => EventType.Tae800;

                public float MetersPerTick { get; set; }
                public float MetersOnTurn { get; set; }
                public float Unk08 { get; set; }

                public Tae800(float startTime, float endTime) : base(startTime, endTime)
                {

                }

                internal Tae800(float startTime, float endTime, BinaryReaderEx br) : base(startTime, endTime)
                {
                    MetersPerTick = br.ReadSingle();
                    MetersOnTurn = br.ReadSingle();
                    Unk08 = br.ReadSingle();
                    br.AssertInt32(0);
                }

                internal override void WriteSpecific(BinaryWriterEx bw)
                {
                    bw.WriteSingle(MetersPerTick);
                    bw.WriteSingle(MetersOnTurn);
                    bw.WriteSingle(Unk08);
                    bw.WriteInt32(0);
                }
            }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
    }
}
