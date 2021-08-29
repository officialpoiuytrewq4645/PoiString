﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.Knowledge
{
    public class Knowledge
    {
        public enum PhysicalMaterial
        {
            Canvas = 61790,
            RedwoodGoteraCoreMaterial = 29972,
            Rope = 35204,
            Straw = 16882,
            DaisLeather = 33384,
            DaisRedLeather = 37850,
            UnknownLeather = 42316,
            WyrmFaceLeather = 63538,
            Aluminium = 20178,
            Brass = 11420,
            CarsiAlloy = 24094,
            Copper = 20186,
            EvinonSteelAlloy = 31502,
            Gold = 56394,
            Iron = 1,
            Mythril = 31174,
            OrchiAlloy = 16222,
            RedIronAlloy = 31468,
            Silver = 28618,
            Tin = 54964,
            TrainingMetalCold = 16064,
            TrainingMetalHot = 3288,
            WhiteGoldAlloy = 24028,
            Ash = 19966,
            Birch = 37260,
            Oak = 24722,
            Redwood = 10962,
            Walnut = 15500,
        }

        public enum SpeciesHashes
        {
            Standard = 56530,
            TreeSpeciesAsh = 31500,
            TreeSpeciesBirch = 4442,
            TreeSpeciesOak = 11232,
            TreeSpeciesRedwood = 7386,
            TreeSpeciesWalnut = 28044,
        }

        public enum Stat
        {
            CrippleHealthStat = 36208,
            DamageProtectionStat = 23030,
            DamageStat = 33378,
            ExperienceBoost = 15202,
            FullnessStat = 34710,
            HandStaminaLeftStat = 53068,
            HandStaminaRightStat = 18866,
            HealthStat = 32712,
            HungerStat = 42940,
            Luminosity = 46602,
            MaxHealthStat = 42070,
            Nightmare = 56516,
            PlayerWeightStat = 59222,
            PoisonStat = 41264,
            RecentDamageStat = 18210,
            SpeedStat = 32340,
            Visibility = 50920,
            WetStat = 40768,
        }
        public enum LandmarkIcons
        {
            BagIcon = 38332,
            CarpentryIcon = 38342,
            CauldronIcon = 38352,
            DummyIcon = 23104,
            ExpShrineIcon = 38362,
            HebiosIcon = 38374,
            LocalPlayerIcon = 30770,
            LockboxIcon = 38384,
            MapBoardIcon = 36566,
            MarketsIcon = 17652,
            MineIcon = 38396,
            OtherPlayerIcon = 45330,
            ProfessionShrineIcon = 38404,
            RepairBoxIcon = 38414,
            SmelterIcon = 38422,
            TargetIcon = 23098,
            UnknownIcon = 38256,
            WellIcon = 38430,
            WoodcuttingIcon = 23092,
        }
        public enum EffectDefinition
        {
            BuildupTeleportToOverworld = 42574,
            DamageIndirecteffect = 654,
            DamageProtectionIndirecteffect = 64858,
            Feed = 3280,
            Heal = 48456,
            Inate = 22728,
            Luminosiyindirecteffect = 65294,
            Nourish = 20046,
            PoisonoverTime = 29250,
            RevealEffect = 62720,
            SpeedIndirectEffect = 6764,
            TeleportToOverworld = 27214,
        }
        public enum LiquidDefinition
        {
            DamageBuffPotion = 13764,
            DamageResistPotion = 19126,
            FeedReductionDebugPotion = 63318,
            HealthPotion = 15340,
            NourishReductionDebugPotion = 51002,
            Poison = 46018,
            SelfDamagePotion = 1180,
            TeleportPotion = 27100,
            Water = 44872,
        }



        //these are the long boios
        public enum LiquidVisualData
        {
            BlueMixStewBurntLiquidVisualData = 33594,
            BlueMixStewCookedLiquidVisualData = 13166,
            BlueMixStewRawLiquidVisualData = 51552,
            BlueberryStewBurntLiquidVisualData = 22532,
            BlueberryStewCookedLiquidVisualData = 22516,
            BlueberryStewRawLiquidVisualData = 41466,
            DefaultStewBurntLiquidVisualData = 22476,
            DefaultStewCookedLiquidVisualData = 22452,
            DefaultStewRawLiquidVisualData = 21644,
            EggplantPotatoRedStewBurntLiquidVisualData = 13690,
            EggplantPotatoRedStewCookedLiquidVisualData = 36046,
            EggplantPotatoRedStewRawLiquidVisualData = 47072,
            GlowingMushroomStewBurntLiquidVisualData = 21060,
            GlowingMushroomStewCookedLiquidVisualData = 21046,
            GlowingMushroomStewRawLiquidVisualData = 20952,
            MushroomSpriggullStewBurntLiquidVisualData = 22598,
            MushroomSpriggullStewCookedLiquidVisualData = 22590,
            MushroomSpriggullStewRawLiquidVisualData = 22572,
            MushroomStewBurntLiquidVisualData = 21600,
            MushroomStewCookedLiquidVisualData = 21586,
            MushroomStewRawLiquidVisualData = 21578,
            NonRecipeStewBurntLiquidVisualData = 21202,
            NonRecipeStewCookedLiquidVisualData = 21182,
            NonRecipeStewRawLiquidVisualData = 21158,
            PotatoPureeStewCookedLiquidVisualData = 40124,
            PotatoPureeStewRawLiquidVisualData = 6764,
            PoultryOnionMushroomStewCookedLiquidVisualData = 15654,
            PoultryOnionMushroomStewRawLiquidVisualData = 45270,
            PoultryPotatoStewCookedLiquidVisualData = 28868,
            PoultryPotatoStewRawLiquidVisualData = 60754,
            PumpkinStewBurntLiquidVisualData = 62608,
            PumpkinStewCookedLiquidVisualData = 62618,
            PumpkinStewRawLiquidVisualData = 62628,
            RedMushroomStewBurntLiquidVisualData = 21248,
            RedMushroomStewCookedLiquidVisualData = 21230,
            RedMushroomStewRawLiquidVisualData = 28546,
            RedSauceStewCookedLiquidVisualData = 45938,
            RedSauceStewRawLiquidVisualData = 17112,
            SpriggullStewBurntLiquidVisualData = 41908,
            SpriggullStewCookedLiquidVisualData = 41578,
            SpriggullStewRawLiquidVisualData = 41624,
            TeleportLiquidVisualData = 49754,
            VegetableRaguStewCookedLiquidVisualData = 38666,
            VegetableRaguStewRawLiquidVisualData = 13246,
            VisionStewCookedLiquidVisualData = 55814,
            VisionStewRawLiquidVisualData = 24160,
            WaterLiquidVisualData = 7838,
        }
        public enum ItemsTheseAreNOTPrefabsThoseWillHaveTheirOwnClass
        {
            Arrow = 25064,
            ArrowDudTraining = 4268,
            ArrowTraining = 4018,
            Bag = 52218,
            BarrelBag = 24794,
            Bow = 15284,
            BowString = 52926,
            BrownMushroomShield = 59166,
            Bung = 20048,
            BuranResin = 19046,
            Camera = 35382,
            CandyCane = 27268,
            CarvedPumpkinSmelter = 41856,
            Cauldron = 25660,
            ChocolateEasterEgg = 64118,
            Clipboard = 39588,
            Coal = 42618,
            CoalAsh = 42736,
            CoalTraining = 45430,
            Coin = 39398,
            HardMetalSmallBits = 22564,
            HardPlateMetalMediumSquare = 52156,
            RopeClump = 61476,
            SoftFabricLargeRoll = 6310,
            SoftFabricMediumRoll = 46760,
            SoftFabricMediumStrips = 3296,
            ThinClothMediumSquare = 57540,
            Guard = 6662,
            GuardFancy = 20860,
            GuardHandle = 55222,
            GuardHemisphere = 45818,
            GuardPointyEnd = 50796,
            GuardRoundEnd = 44282,
            GuardStraightEnd = 27204,
            LargeGuardRectangle = 45306,
            LargeGuardTShape = 48740,
            LargeGuardWedge = 47906,
            PhantomGuard = 2166,
            PoleHuggerEdgy = 41614,
            PoleHuggerPointyEnds = 23256,
            PoleHuggerShort = 55590,
            PoleHuggerTall = 59996,
            Pommel = 38888,
            PommelCircle = 41888,
            PommelCone = 62918,
            PommelDiamond = 3942,
            PommelLargeSquare = 52748,
            PommelSquashed = 64894,
            AxeHeadCurve = 30918,
            AxeHeadFelling = 28524,
            AxeHeadGreatCurve = 4200,
            AxeHeadLShape = 34982,
            BowBlade = 11760,
            GreatSwordBlade = 3666,
            HebiosKatanaBlade = 43412,
            HebiosNaginataBlade = 18356,
            HebiosSaiBlade = 11660,
            HebiosWakizashiBlade = 3414,
            KatanaBladePartFull = 19994,
            LargeLongswordBlade = 31474,
            NaginataBladePartFull = 18190,
            RapierBlade = 52282,
            SaiBladePartFull = 6840,
            ScytheBlade = 32210,
            ShortSwordBlade = 22888,
            ShotelBlade = 55424,
            ThickGreatSwordBlade = 23312,
            WakizashiBladePartFull = 28548,
            Chisel = 3168,
            DaggerBladeCurved = 13684,
            DaggerBladeEdgy = 59506,
            DaggerBladeRound = 38432,
            DaggerBladeWide = 27432,
            EdgeCurvedBlade = 27754,
            EdgeLongCurveBlade = 31890,
            EdgeShortCurveBlade = 37942,
            EdgeStraightBlade = 34578,
            HammerHead = 32966,
            HammerHeadSmall = 14274,
            LogMallet = 11082,
            LogMalletv02 = 23508,
            MetalHammerHead = 35212,
            TurabadaArm = 7822,
            WarHammerHead = 42968,
            HebiosKatanaBladePart01 = 17402,
            HebiosKatanaBladePart02 = 17408,
            HebiosNaginataBladePart01 = 17338,
            HebiosNaginataBladePart02 = 17378,
            HebiosSaiBladePart01 = 62068,
            HebiosSaiBladePart02 = 1350,
            HebiosWakizashiBladePart01 = 54988,
            HebiosWakizashiBladePart02 = 59494,
            SpearHeadPyramid = 37842,
            SpearHeadSpiky = 40662,
            SpearHeadStandard = 29050,
            BoneSpike = 13970,
            CrystalShardBlue = 7824,
            HealingPod = 45470,
            SpikeExplosive = 31316,
            SpikeFancy = 26290,
            SpikeShort = 8446,
            SpikeTall = 53816,
            CraftPieceSide1WayItem = 29156,
            CraftPieceSide2WayItem = 29164,
            CraftPieceSide4WayItem = 29172,
            CraftPieceSideFlat1WayItem = 29148,
            CraftPieceSideFlat2WayItem = 29122,
            ArrowShaft = 39982,
            ArrowShaftTraining = 3016,
            HandleBow = 1058,
            HandleLargeBranch = 56114,
            HandleLargeCool = 40154,
            HandleLargeStandard = 49052,
            HandleLongStraight = 61630,
            HandleMediumBranch = 56088,
            HandleMediumCool = 25450,
            HandleMediumCurved = 22576,
            HandleMediumRidged = 39038,
            HandleMediumStandard = 8410,
            HandleMediumStraight = 19768,
            HandleRoundFist = 53676,
            HandleShort = 34662,
            HandleShortCCurve = 36240,
            HandleShortCool = 61562,
            HandleShortPointyEnd = 19208,
            HandleShortSCurve = 32694,
            HandleShortTaper = 45036,
            HandleSpear = 2572,
            HandleTonfa = 61254,
            RodLong = 17986,
            RodMedium = 5812,
            RodSlim40cm = 14166,
            PickaxeHead = 50294,
            CrystalGemBlue = 45754,
            CrystalLanceBlue = 45046,
            CrystalPickBlue = 8752,
            CrystalSwordBlue = 21398,
            CurledWoodenHandle = 16158,
            DebugNet = 65102,
            DebugArrow = 40108,
            DebugTool = 4248,
            Dragonfly = 15142,
            DragonflyCorpse = 47362,
            DuplicatePaper = 5490,
            Dynamite = 6856,
            EasterEgg01 = 49422,
            EasterEgg02 = 32876,
            EasterEgg03 = 32884,
            EasterEgg04 = 32892,
            AppleCoreBurnt = 17642,
            AppleCoreCooked = 17634,
            AppleCoreRipe = 18406,
            AppleCoreUnripe = 25584,
            AppleFullBurnt = 14896,
            AppleFullCooked = 12286,
            AppleFullRipe = 9502,
            AppleFullUnripe = 6606,
            AppleHalfBurnt = 3798,
            AppleHalfCooked = 1342,
            AppleHalfRipe = 64422,
            AppleHalfUnripe = 61966,
            BlueberryBiteBurnt = 53602,
            BlueberryBiteCooked = 51290,
            BlueberryBiteRipe = 48944,
            BlueberryBiteUnripe = 46598,
            BlueberryFullBurnt = 49844,
            BlueberryFullCooked = 47422,
            BlueberryFullRipe = 44966,
            BlueberryFullUnripe = 42510,
            BabuLegBone = 3636,
            BabuLegFullBurnt = 52634,
            BabuLegFullCooked = 48126,
            BabuLegFullRipe = 40732,
            BabuLegHalfBurnt = 50224,
            BabuLegHalfCooked = 50232,
            BabuLegHalfRipe = 50240,
            DaisMeatFullBurnt = 47254,
            DaisMeatFullCooked = 47262,
            DaisMeatFullRipe = 47270,
            DaisMeatHalfBurnt = 47278,
            DaisMeatHalfCooked = 47286,
            DaisMeatHalfRipe = 47294,
            SpriggullDrumstickBone = 32140,
            SpriggullDrumstickFullBurnt = 5284,
            SpriggullDrumstickFullCooked = 5268,
            SpriggullDrumstickFullRipe = 49918,
            SpriggullDrumstickHalfBurnt = 10860,
            SpriggullDrumstickHalfCooked = 8392,
            SpriggullDrumstickHalfRipe = 5924,
            MushroomBrownFullBurnt = 40370,
            MushroomBrownFullCooked = 46528,
            MushroomBrownFullRipe = 46150,
            MushroomBrownHalfBurnt = 53326,
            MushroomBrownHalfCooked = 50870,
            MushroomBrownHalfRipe = 48414,
            MushroomCaveLargeHalfBurnt = 64098,
            MushroomCaveLargeHalfCooked = 36450,
            MushroomCaveLargeHalfRipe = 26514,
            MushroomCaveSmallFullBurnt = 16332,
            MushroomCaveSmallFullCooked = 16324,
            MushroomCaveSmallFullRipe = 47116,
            MushroomCaveSmallHalfBurnt = 47094,
            MushroomCaveSmallHalfCooked = 44440,
            MushroomCaveSmallHalfRipe = 41786,
            MushroomGlowingFullBurnt = 42542,
            MushroomGlowingFullCooked = 42532,
            MushroomGlowingFullRipe = 44960,
            MushroomGlowingHalfBurnt = 57828,
            MushroomGlowingHalfCooked = 59670,
            MushroomGlowingHalfRipe = 24352,
            MushroomRedFullBurnt = 22246,
            MushroomRedFullCooked = 46900,
            MushroomRedFullRipe = 24192,
            MushroomRedHalfBurnt = 43226,
            MushroomRedHalfCooked = 40770,
            MushroomRedHalfRipe = 38314,
            Salt = 1462,
            CarrotFullBurnt = 62276,
            CarrotFullCooked = 60384,
            CarrotFullRipe = 58480,
            CarrotFullUnripe = 56576,
            CarrotHalfBurnt = 64088,
            CarrotHalfCooked = 63700,
            CarrotHalfRipe = 63296,
            CarrotHalfUnripe = 62892,
            EggplantFullBurnt = 54948,
            EggplantFullCooked = 54424,
            EggplantFullRipe = 53900,
            EggplantFullUnripe = 53376,
            EggplantHalfBurnt = 62488,
            EggplantHalfCooked = 62084,
            EggplantHalfRipe = 61680,
            EggplantHalfUnripe = 61276,
            GarlicFullBurnt = 52876,
            GarlicFullCooked = 52472,
            GarlicFullRipe = 51556,
            GarlicFullUnripe = 49096,
            GarlicHalfBurnt = 60872,
            GarlicHalfCooked = 60468,
            GarlicHalfRipe = 60064,
            GarlicHalfUnripe = 59660,
            OnionFullBurnt = 48640,
            OnionFullCooked = 48140,
            OnionFullRipe = 47000,
            OnionFullUnripe = 43860,
            OnionHalfBurnt = 59256,
            OnionHalfCooked = 58852,
            OnionHalfRipe = 58448,
            OnionHalfUnripe = 58646,
            PotatoFullBurnt = 40072,
            PotatoFullCooked = 39728,
            PotatoFullRipe = 39384,
            PotatoFullUnripe = 39040,
            PotatoHalfBurnt = 59662,
            PotatoHalfCooked = 50526,
            PotatoHalfRipe = 48048,
            PotatoHalfUnripe = 45736,
            PotatoSapling = 17268,
            PumpkinFullBurnt = 5454,
            PumpkinFullCooked = 4932,
            PumpkinFullRipe = 4410,
            PumpkinFullUnripe = 3888,
            PumpkinHalfBurnt = 3398,
            PumpkinHalfCooked = 3036,
            PumpkinHalfRipe = 2674,
            PumpkinHalfUnripe = 2312,
            PumpkinPieceBurnt = 42180,
            PumpkinPieceCooked = 41892,
            PumpkinPieceRipe = 41570,
            PumpkinPieceUnripe = 41248,
            TomatoFullBurnt = 38636,
            TomatoFullCooked = 37992,
            TomatoFullRipe = 37348,
            TomatoFullUnripe = 36704,
            TomatoHalfBurnt = 57640,
            TomatoHalfCooked = 57236,
            TomatoHalfRipe = 56832,
            TomatoHalfUnripe = 56428,
            FeatherRedTraining = 3900,
            Fingernails = 23374,
            Firebug = 10252,
            FixedLightBagAttachment = 52120,
            FlashlightLantern = 23926,
            Flint = 42570,
            FlintTraining = 54560,
            FlowerBlue = 37312,
            FlowerRed = 59174,
            ForageBagSidePouchAttachment = 19570,
            ForageBasketBag = 39858,
            GeodeHalf01 = 26494,
            GeodeHalf02 = 26236,
            GeodeItem = 45388,
            GoteraSeedlingOrb = 904,
            GrassClump = 21722,
            Hammer = 4738,
            HandleFist = 46034,
            GourdCanteen = 31744,
            KaKarimata = 8422,
            Katana = 8488,
            Kunai = 8518,
            Naginata = 37182,
            OLDKaKarimata = 60876,
            Sai = 42364,
            Wakizashi = 56464,
            HoarderBag = 5714,
            BronzeIngot = 28640,
            CarsiIngot = 24084,
            CopperIngot = 5802,
            DarksteelIngot = 50292,
            EvinonSteelIngot = 32224,
            GoldIngot = 17090,
            IronIngot = 7204,
            MythrilIngot = 60398,
            OrchiIngot = 16422,
            RedIronIngot = 30996,
            SilverIngot = 57718,
            SteelIngot = 10734,
            WhiteGoldIngot = 24016,
            IronKey = 49402,
            ItemShooter = 58148,
            Lantern = 25946,
            LargeSpikedWoodenClub = 38078,
            LargeSword = 61520,
            LightPickup = 4318,
            MenuToken = 38474,
            MetalBow = 64508,
            MetalTap = 15966,
            MetalWallHook = 19156,
            Microphone = 47350,
            ModularBagObsolete = 19888,
            ModularHook = 60202,
            ModularSack = 33698,
            Mould = 22870,
            MouldHebios = 10880,
            MouldStandard = 44232,
            OLDArrow = 5344,
            OreBag = 47602,
            OreBagSideAttachment = 19558,
            CopperOre = 42614,
            GoldOre = 5732,
            IronOre = 42566,
            MythrilOre = 4758,
            OreTraining = 57856,
            SilverOre = 5698,
            Paper = 56198,
            Pick = 3686,
            PotionHoopsSideAttachment = 52126,
            PotionMedium = 37408,
            PotionSmall = 61068,
            WaterPotionMedium = 33896,
            Pouch = 5800,
            PouchTraining = 57888,
            Prongs = 54236,
            Quiver = 25002,
            RedMushroom = 27972,
            RedMushroomShield = 59234,
            RedwoodGoteraCore = 10670,
            RustyChisel = 40332,
            RustyShield = 20582,
            SandstoneStone = 19822,
            SchmeecheeGlowing = 50490,
            SchmeecheeOrange = 65300,
            SchmeecheePoisonous = 37070,
            SchmeecheeRed = 37062,
            SelectServerOrb = 21488,
            ShieldCoreBentMiddle = 40502,
            ShieldCoreCircleMiddle = 46078,
            ShieldCoreHandle = 48112,
            ShieldCoreHoledMiddle = 46086,
            ShieldPartHalfCircle = 49054,
            ShieldPartHalfCircleHole = 46100,
            ShieldPartHalfHole = 46108,
            ShieldPartHalfPoint = 5428,
            Shield = 41410,
            ShieldSmall = 5776,
            ShieldSmallTraining = 3564,
            ShortSword = 54952,
            Slingshot = 56828,
            SpriggullFeatherBlue = 23140,
            SpriggullFeatherGreen = 39492,
            SpriggullFeatherPurple = 63734,
            SpriggullFeatherRed = 51238,
            SpriggullFletchingBlue = 56216,
            SpriggullFletchingRed = 29538,
            Spyglass = 17772,
            SpyglassLong = 984,
            StandardSidePouchAttachment = 16780,
            StandardSideToolAttachment = 11624,
            Stick = 10650,
            StickCharred = 21610,
            StickTraining = 3188,
            Stone = 42568,
            StoneTraining = 45012,
            TablePlacer = 14254,
            CandyCaneKnife = 8088,
            Firework = 44786,
            RustyAxe = 4350,
            RustyGreataxe = 29234,
            RustyGreatsword = 63122,
            RustyHammer = 36064,
            RustyPickaxe = 46812,
            RustyPitchfork = 59404,
            RustyShortSword = 44790,
            RustySpade = 33128,
            SpadeHead = 1032,
            TimberBag = 38366,
            TimberBagSidePouchAttachment = 19564,
            Torch = 4784,
            AshTreeSeed = 38612,
            BirchTreeSeed = 38618,
            OakTreeSeed = 2386,
            RedwoodTreeSeed = 38624,
            WalnutTreeSeed = 38630,
            Vacuum = 8264,
            WedgeTraining = 2932,
            Wheel = 18560,
            WoodcutWedge = 65086,
            WoodcutWedgeAshen = 49552,
            WoodcutWedgeBurnt = 32514,
            WoodcutWedgeCharred = 46842,
            WoodenBag = 35364,
            WoodenBowl = 49944,
            WoodenBucket = 1618,
            WoodenDice = 55230,
            WoodenLadle = 56890,
            WoodenNet = 58350,
            WoodenShortSword = 37948,
            WoodenStake = 38354,
            WoodenStirringSpoon = 41010,
            WyrmArm = 25392,
        }
        public enum LiquidVisualChunkDefinition
        {
            AppleBurntChunkVisual = 56934,
            AppleCookedChunkVisual = 56516,
            AppleRipeChunkVisual = 56016,
            AppleUnripeChunkVisual = 55460,
            BabuBurntChunkVisual = 12722,
            BabuCookedChunkVisual = 17228,
            BabuRipeChunkVisual = 21734,
            BlueberryBurntChunkVisual = 13156,
            BlueberryCookedChunkVisual = 9572,
            BlueberryRipeChunkVisual = 5988,
            BlueberryUnripeChunkVisual = 2404,
            CarrotBurntChunkVisual = 1502,
            CarrotCookedChunkVisual = 50674,
            CarrotRipeChunkVisual = 50666,
            CarrotUnripeChunkVisual = 1510,
            EggplantBurntChunkVisual = 50610,
            EggplantCookedChunkVisual = 50602,
            EggplantRipeChunkVisual = 50594,
            EggplantUnripeChunkVisual = 50584,
            GarlicBurntChunkVisual = 50570,
            GarlicCookedChunkVisual = 50562,
            GarlicRipeChunkVisual = 50554,
            GarlicUnripeChunkVisual = 50544,
            MushroomBrownBurntChunkVisual = 39088,
            MushroomBrownCookedChunkVisual = 20040,
            MushroomBrownRipeChunkVisual = 21422,
            MushroomCaveLargeBurntChunkVisual = 58130,
            MushroomCaveLargeCookedChunkVisual = 52698,
            MushroomCaveLargeRipeChunkVisual = 47266,
            MushroomCaveSmallBurntChunkVisual = 38392,
            MushroomCaveSmallCookedChunkVisual = 32334,
            MushroomCaveSmallRipeChunkVisual = 26276,
            MushroomGlowingBurntChunkVisual = 9532,
            MushroomGlowingCookedChunkVisual = 4142,
            MushroomGlowingRipeChunkVisual = 64288,
            MushroomRedBurntChunkVisual = 32604,
            MushroomRedCookedChunkVisual = 27048,
            MushroomRedRipeChunkVisual = 21492,
            OnionBurntChunkVisual = 50530,
            OnionCookedChunkVisual = 50522,
            OnionRipeChunkVisual = 50514,
            OnionUnripeChunkVisual = 50504,
            PotatoBurntChunkVisual = 13336,
            PotatoCookedChunkVisual = 13360,
            PotatoRipeChunkVisual = 13352,
            PotatoUnripeChunkVisual = 13344,
            PumpkinBurntChunkVisual = 30124,
            PumpkinCookedChunkVisual = 29434,
            PumpkinRipeChunkVisual = 28744,
            PumpkinUnripeChunkVisual = 28054,
            SaltChunkVisual = 13596,
            SpriggullDrumstickBurntChunkVisual = 32864,
            SpriggullDrumstickCookedChunkVisual = 27542,
            SpriggullDrumstickRipeChunkVisual = 22220,
            TomatoBurntChunkVisual = 50490,
            TomatoCookedChunkVisual = 50482,
            TomatoRipeChunkVisual = 50474,
            TomatoUnripeChunkVisual = 50464,
        }



    }
}

