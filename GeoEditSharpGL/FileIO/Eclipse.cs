using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pexel.Eclipse
{

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class DATA
    {
        public RUNSPEC RUNSPEC { set; get; }
        public Section GRID { set; get; }
        public Section EDIT { set; get; }
        public Section PROPS { set; get; }
        public Section REGIONS { set; get; }
        public Section SOLUTION { set; get; }
        public Section SUMMARY { set; get; }
        public SCHEDULE SCHEDULE { set; get; }

        public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.AddRange(RUNSPEC.Lines());
            result.AddRange(GRID.Lines());
            result.AddRange(EDIT.Lines());
            result.AddRange(PROPS.Lines());
            result.AddRange(REGIONS.Lines());
            result.AddRange(SOLUTION.Lines());
            result.AddRange(SUMMARY.Lines());
            result.AddRange(SCHEDULE.Lines());
            return result;
        }

        public bool Export(string file)
        {
            try
            {
                File.WriteAllLines(file, Lines());
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }


    


    public abstract class Section
    {
        public string Title { protected set; get; }
        public List<KW> KWs { set; get; } = new List<KW>();
        virtual public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.Add(new string('-', 100));
            result.Add(Title);
            result.Add(new string('-', 100));
            result.Add(string.Empty);
            foreach (KW kw in KWs.OrderBy(x => x.Date).ThenBy(x => x.Priority))
                result.AddRange(kw.Lines());
            return result;
        }
    }

    

    public class RUNSPEC : Section
    {
        const string title = "RUNSPEC";
        public RUNSPEC()
        {
            Title = title;
        }
    }

    public class SCHEDULE : Section
    {
        const string title = "SCHEDULE";
        public enum FrequencyType { DAY, MONTH, YEAR }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public FrequencyType Frequency { set; get; } = FrequencyType.MONTH;
        public SCHEDULE()
        {
            Title = title;
        }
        void CompliteDates()
        {
            DateTime date = new DateTime(Start.Year, Start.Month, 1);
            while (date < End)
            {
                switch (Frequency)
                {
                    case FrequencyType.DAY: date = date.AddDays(1); break;
                    case FrequencyType.YEAR: date = date.AddYears(1); break;
                    default: date = date.AddMonths(1); break;
                }
                Remove(date);
                this.KWs.Add(new DATES() { Date = date });
            }
        }
        public bool Remove(DateTime date)
        {
            List<int> remove = new List<int>();
            for (int i = 0; i < KWs.Count; ++i)
                if (KWs[i] is DATES && KWs[i].Date == date)
                    remove.Add(i);
            remove.Reverse();
            foreach (int i in remove) KWs.RemoveAt(i);
            return remove.Count > 0;
        }
        void CreateExistingDates()
        {
            List<DATES> dates = new List<DATES>();
            foreach (KW kw in KWs)
            {
                Remove(kw.Date);
                dates.Add(new DATES() { Date = kw.Date });
            }
            KWs.AddRange(dates);
        }
        override public List<string> Lines()
        {
            CreateExistingDates();
            CompliteDates();
            return base.Lines();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public abstract class KW
    {
        protected const int priority_dates = 0;
        protected const int priority_welspecs = 1;
        protected const int priority_compdat = 2;
        protected const int priority_wconprod = 3;
        protected const int priority_wconinje = 4;
        protected const int priority_wecon = 5;
        protected const int priority_wefac = 6;

        public string Title { protected set; get; }
        public DateTime Date { set; get; }
        public int Priority { protected set; get; }

        abstract public List<string> Lines();
    }

    public abstract class KWContent
    {
        protected const string default_value = "1*";
        public abstract string Line();
        protected static string Join(List<object> items)
        {
            List<object> result = new List<object>();
            int count = 0;
            foreach (object item in items)
            {
                if (item != default_value)
                {
                    if (count > 0)
                    {
                        result.Add(count.ToString() + "*");
                        count = 0;
                    }
                    result.Add(item);
                }
                else ++count;
            }
            return string.Join(" ", result);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class WCONPROD : KW
    {
        const string title = "WCONPROD";
        public WCONPROD()
        {
            Title = title;
            Priority = priority_wconprod;
        }
        public List<WCONPRODContent> Data { set; get; } = new List<WCONPRODContent>();
        override public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.Add(Title);
            foreach (KWContent data in Data)
                result.Add(data.Line());
            result.Add("/");
            result.Add(string.Empty);
            return result;
        }
    }

    public class WCONPRODContent : KWContent
    {
        public string WellName { set; get; } = default_value;
        public OpenShutFlagType OpenShutFlag { set; get; } = OpenShutFlagDefalut;
        public ControlModeType ControlMode { set; get; } = ControlModeTypeDefalut;
        public double? OilRateTargetOrUpperLimit { set; get; } = null;
        public double? WaterRateTargetOrUpperLimit { set; get; } = null;
        public double? GasRateTargetOrUpperLimit { set; get; } = null;
        public double? LiquidRateTargetOrUpperLimit { set; get; } = null;
        public double? ReservoirFluidVolumeRateTargetOrUpperLimit { set; get; } = null;
        public double? BHPTargetOrLowerLimit { set; get; } = null;
        public double? THPTargetOrLowerLimit { set; get; } = null;
        public int? ProductionWellVFPTableNumber { set; get; } = null;
        public double? ArtificialLiftQuantity { set; get; } = null;
        public double? WetGasProductionRateTargetOrUpperlimit { set; get; } = null;
        public double? TotalMolarRateTargetOrUpperLimit { set; get; } = null;
        public double? SteamProductionRateTargetOrUpperLimit { set; get; } = null;
        public double? PressureOffset { set; get; } = null;
        public double? TemperatureOffset { set; get; } = null;
        public double? CalorificRateTargetOrUpperLimit { set; get; } = null;
        public double? LinearlyCombinedRateTargetOrUpperLimit { set; get; } = null;


        public enum OpenShutFlagType { OPEN, STOP, SHUT, AUTO };
        public const OpenShutFlagType OpenShutFlagDefalut = OpenShutFlagType.OPEN;
        public enum ControlModeType { UNDEF, ORAT, WRAT, GRAT, LRAT, CRAT, RESV, BHP, THP, WGRA, TMRA, STRA, SATP, SATT, CVAL, GRUP };
        public const ControlModeType ControlModeTypeDefalut = ControlModeType.UNDEF;



        override public string Line()
        {
            List<object> result = new List<object>();
            result.Add(WellName is null ? default_value : WellName);
            switch (OpenShutFlag)
            {
                case OpenShutFlagType.OPEN: result.Add("OPEN"); break;
                case OpenShutFlagType.STOP: result.Add("STOP"); break;
                case OpenShutFlagType.SHUT: result.Add("SHUT"); break;
                case OpenShutFlagType.AUTO: result.Add("AUTO"); break;
            }
            switch (ControlMode)
            {
                case ControlModeType.UNDEF: result.Add(default_value); break;
                case ControlModeType.ORAT: result.Add("ORAT"); break;
                case ControlModeType.WRAT: result.Add("WRAT"); break;
                case ControlModeType.GRAT: result.Add("GRAT"); break;
                case ControlModeType.LRAT: result.Add("LRAT"); break;
                case ControlModeType.CRAT: result.Add("CRAT"); break;
                case ControlModeType.RESV: result.Add("RESV"); break;
                case ControlModeType.BHP: result.Add("BHP"); break;
                case ControlModeType.THP: result.Add("THP"); break;
                case ControlModeType.WGRA: result.Add("WGRA"); break;
                case ControlModeType.TMRA: result.Add("TMRA"); break;
                case ControlModeType.STRA: result.Add("STRA"); break;
                case ControlModeType.SATP: result.Add("SATP"); break;
                case ControlModeType.SATT: result.Add("SATT"); break;
                case ControlModeType.CVAL: result.Add("CVAL"); break;
                case ControlModeType.GRUP: result.Add("GRUP"); break;
            }
            result.Add(OilRateTargetOrUpperLimit is null ? default_value : OilRateTargetOrUpperLimit.ToString());
            result.Add(WaterRateTargetOrUpperLimit is null ? default_value : WaterRateTargetOrUpperLimit.ToString());
            result.Add(GasRateTargetOrUpperLimit is null ? default_value : GasRateTargetOrUpperLimit.ToString());
            result.Add(LiquidRateTargetOrUpperLimit is null ? default_value : LiquidRateTargetOrUpperLimit.ToString());
            result.Add(ReservoirFluidVolumeRateTargetOrUpperLimit is null ? default_value : ReservoirFluidVolumeRateTargetOrUpperLimit.ToString());
            result.Add(BHPTargetOrLowerLimit is null ? default_value : BHPTargetOrLowerLimit.ToString());
            result.Add(THPTargetOrLowerLimit is null ? default_value : THPTargetOrLowerLimit.ToString());
            result.Add(ProductionWellVFPTableNumber is null ? default_value : ProductionWellVFPTableNumber.ToString());
            result.Add(ArtificialLiftQuantity is null ? default_value : ArtificialLiftQuantity.ToString());
            result.Add(WetGasProductionRateTargetOrUpperlimit is null ? default_value : WetGasProductionRateTargetOrUpperlimit.ToString());
            result.Add(TotalMolarRateTargetOrUpperLimit is null ? default_value : TotalMolarRateTargetOrUpperLimit.ToString());
            result.Add(SteamProductionRateTargetOrUpperLimit is null ? default_value : SteamProductionRateTargetOrUpperLimit.ToString());
            result.Add(PressureOffset is null ? default_value : PressureOffset.ToString());
            result.Add(TemperatureOffset is null ? default_value : TemperatureOffset.ToString());
            result.Add(CalorificRateTargetOrUpperLimit is null ? default_value : CalorificRateTargetOrUpperLimit.ToString());
            result.Add(LinearlyCombinedRateTargetOrUpperLimit is null ? default_value : LinearlyCombinedRateTargetOrUpperLimit.ToString());
            result.Add("/");
            return Join(result);
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class WCONINJE : KW
    {
        const string title = "WCONINJE";
        public WCONINJE()
        {
            Title = title;
            Priority = priority_wconinje;
        }
        public List<WCONINJEContent> Data { set; get; } = new List<WCONINJEContent>();
        override public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.Add(Title);
            foreach (KWContent data in Data)
                result.Add(data.Line());
            result.Add("/");
            result.Add(string.Empty);
            return result;
        }
    }

    public class WCONINJEContent : KWContent
    {
        public string WellName { set; get; } = default_value;
        public InjectorType Injector { set; get; } = InjectorTypeDefault;
        public OpenShutFlagType OpenShutFlag { set; get; } = OpenShutFlagDefalut;
        public ControlModeType ControlMode { set; get; } = ControlModeTypeDefalut;
        public double? SurfaceFlowRateTargetOrUpperLimit { set; get; } = null;
        public double? ReservoirFluidVolumRateTargeOrUpperLimit { set; get; } = null;
        public double? BHPTargetOrLowerLimit { set; get; } = null;
        public double? THPTargetOrLowerLimit { set; get; } = null;
        public int? InjectionWellVFPTableNumber { set; get; } = null;
        public double? VaporizedOilOrDissolvedGasConcentration { set; get; } = null;
        public double? RatioOfGasVolumeToSteamVolume { set; get; } = null;
        public double? SurfaceVolumeProportionOilInMultiPhaseInjector { set; get; } = null;
        public double? SurfaceVolumeProportionWaterInMultiPhaseInjector { set; get; } = null;
        public double? SurfaceVolumeProportionGasInMultiPhaseInjector { set; get; } = null;


        public enum InjectorType { UNDEF, WATER, GAS, STEAM_GAS, OIL, MULTI };
        public const InjectorType InjectorTypeDefault = InjectorType.UNDEF;
        public enum OpenShutFlagType { OPEN, STOP, SHUT, AUTO };
        public const OpenShutFlagType OpenShutFlagDefalut = OpenShutFlagType.OPEN;
        public enum ControlModeType { UNDEF, RATE, RESV, BHP, THP, GRUP };
        public const ControlModeType ControlModeTypeDefalut = ControlModeType.UNDEF;


        override public string Line()
        {
            List<object> result = new List<object>();
            result.Add(WellName is null ? default_value : WellName);
            switch (Injector)
            {
                case InjectorType.UNDEF: result.Add(default_value); break;
                case InjectorType.WATER: result.Add("WATER"); break;
                case InjectorType.GAS: result.Add("GAS"); break;
                case InjectorType.STEAM_GAS: result.Add("STEAM-GAS"); break;
                case InjectorType.OIL: result.Add("OIL"); break;
                case InjectorType.MULTI: result.Add("MULTI"); break;
            }
            switch (OpenShutFlag)
            {
                case OpenShutFlagType.OPEN: result.Add("OPEN"); break;
                case OpenShutFlagType.STOP: result.Add("STOP"); break;
                case OpenShutFlagType.SHUT: result.Add("SHUT"); break;
                case OpenShutFlagType.AUTO: result.Add("AUTO"); break;
            }
            switch (ControlMode)
            {
                case ControlModeType.UNDEF: result.Add(default_value); break;
                case ControlModeType.RATE: result.Add("RATE"); break;
                case ControlModeType.RESV: result.Add("RESV"); break;
                case ControlModeType.BHP: result.Add("BHP"); break;
                case ControlModeType.THP: result.Add("THP"); break;
                case ControlModeType.GRUP: result.Add("GRUP"); break;
            }
            result.Add(SurfaceFlowRateTargetOrUpperLimit is null ? default_value : SurfaceFlowRateTargetOrUpperLimit.ToString());
            result.Add(ReservoirFluidVolumRateTargeOrUpperLimit is null ? default_value : ReservoirFluidVolumRateTargeOrUpperLimit.ToString());
            result.Add(BHPTargetOrLowerLimit is null ? default_value : BHPTargetOrLowerLimit.ToString());
            result.Add(THPTargetOrLowerLimit is null ? default_value : THPTargetOrLowerLimit.ToString());
            result.Add(InjectionWellVFPTableNumber is null ? default_value : InjectionWellVFPTableNumber.ToString());
            result.Add(VaporizedOilOrDissolvedGasConcentration is null ? default_value : VaporizedOilOrDissolvedGasConcentration.ToString());
            result.Add(RatioOfGasVolumeToSteamVolume is null ? default_value : RatioOfGasVolumeToSteamVolume.ToString());
            result.Add(SurfaceVolumeProportionOilInMultiPhaseInjector is null ? default_value : SurfaceVolumeProportionOilInMultiPhaseInjector.ToString());
            result.Add(SurfaceVolumeProportionWaterInMultiPhaseInjector is null ? default_value : SurfaceVolumeProportionWaterInMultiPhaseInjector.ToString());
            result.Add(SurfaceVolumeProportionGasInMultiPhaseInjector is null ? default_value : SurfaceVolumeProportionGasInMultiPhaseInjector.ToString());
            result.Add("/");
            return Join(result);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class COMPDAT : KW
    {
        const string title = "COMPDAT";
        public COMPDAT()
        {
            Title = title;
            Priority = priority_compdat;
        }
        public List<COMPDATContent> Data { set; get; } = new List<COMPDATContent>();
        override public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.Add(Title);
            foreach (KWContent data in Data)
                result.Add(data.Line());
            result.Add("/");
            result.Add(string.Empty);
            return result;
        }
    }

    public class COMPDATContent : KWContent
    {
        public string WellName { set; get; } = default_value;
        public int? ILocation { set; get; } = null;
        public int? JLocation { set; get; } = null;
        public int? K1Location { set; get; } = null;
        public int? K2Location { set; get; } = null;
        public OpenShutFlagType OpenShutFlag { set; get; } = OpenShutFlagDefalut;
        public int? SATNumber { set; get; } = null;
        public double? TransmissibilityFactor { set; get; } = null;
        public double? WellBoreDiameter { set; get; } = null;
        public double? EffectiveKH { set; get; } = null;
        public double? SkinFactor { set; get; } = null;
        public double? DFactor { set; get; } = null;
        public DirectionWellPenetratesType DirectionWellPenetrates { set; get; } = DirectionWellPenetratesDefalut;
        public double? PressureEquivalentRadius { set; get; } = null;


        public enum OpenShutFlagType { OPEN, SHUT, AUTO };
        public const OpenShutFlagType OpenShutFlagDefalut = OpenShutFlagType.OPEN;
        public enum DirectionWellPenetratesType { X, Y, Z, FX, FY };
        public const DirectionWellPenetratesType DirectionWellPenetratesDefalut = DirectionWellPenetratesType.Z;

        override public string Line()
        {
            List<object> result = new List<object>();
            result.Add(WellName is null ? default_value : WellName);
            result.Add(ILocation is null ? default_value : ILocation.ToString());
            result.Add(JLocation is null ? default_value : JLocation.ToString());
            result.Add(K1Location is null ? default_value : K1Location.ToString());
            result.Add(K2Location is null ? default_value : K2Location.ToString());
            switch (OpenShutFlag)
            {
                case OpenShutFlagType.OPEN: result.Add("OPEN"); break;
                case OpenShutFlagType.SHUT: result.Add("SHUT"); break;
                case OpenShutFlagType.AUTO: result.Add("AUTO"); break;
            }
            result.Add(SATNumber is null ? default_value : SATNumber.ToString());
            result.Add(TransmissibilityFactor is null ? default_value : TransmissibilityFactor.ToString());
            result.Add(WellBoreDiameter is null ? default_value : WellBoreDiameter.ToString());
            result.Add(EffectiveKH is null ? default_value : EffectiveKH.ToString());
            result.Add(SkinFactor is null ? default_value : SkinFactor.ToString());
            result.Add(DFactor is null ? default_value : DFactor.ToString());
            switch (DirectionWellPenetrates)
            {
                case DirectionWellPenetratesType.X: result.Add("X"); break;
                case DirectionWellPenetratesType.Y: result.Add("Y"); break;
                case DirectionWellPenetratesType.Z: result.Add("Z"); break;
                case DirectionWellPenetratesType.FX: result.Add("FX"); break;
                case DirectionWellPenetratesType.FY: result.Add("FY"); break;
            }
            result.Add(PressureEquivalentRadius is null ? default_value : PressureEquivalentRadius.ToString());
            result.Add("/");
            return Join(result);
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class WELSPECS : KW
    {
        const string title = "WELSPECS";
        public WELSPECS()
        {
            Title = title;
            Priority = priority_welspecs;
        }
        public List<WELSPECSContent> Data { set; get; } = new List<WELSPECSContent>();
        override public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.Add(Title);
            foreach (KWContent data in Data)
                result.Add(data.Line());
            result.Add("/");
            result.Add(string.Empty);
            return result;
        }
    }

    public class WELSPECSContent : KWContent
    {
        public string WellName { set; get; } = default_value;
        public string GroupName { set; get; } = GroupNameDefault;
        public int ILocation { set; get; }
        public int JLocation { set; get; }
        public double? ReferenceDepth { set; get; } = null;
        public PreferredPhaseType PreferredPhase { set; get; } = PreferredPhaseType.OIL;
        public double DrainageRadius { set; get; } = DrainageRadiusDefault;
        public SpecialInflowEquationType SpecialInflowEquation { set; get; } = SpecialInflowEquationType.STD;
        public InstructionsForAutomaticShutInType InstructionsForAutomaticShutIn { set; get; } = InstructionsForAutomaticShutInDefault;
        public CrossflowAbilityType CrossflowAbility { set; get; } = CrossflowAbilityType.YES;
        public int PressureTableNumber { set; get; } = PressureTableNumberDefault;
        public DensityCalculationType DensityCalculation { set; get; } = DensityCalculationDefault;
        public int FIPNum { set; get; } = FIPNumDefault;
        public string Reserved1 { get; } = default_value;
        public string Reserved2 { get; } = default_value;
        public WellModelType WellModel { set; get; } = WellModelDefault;

        public const string GroupNameDefault = "FIELD";
        public enum PreferredPhaseType { OIL, WATER, GAS, LIQ };
        public const double DrainageRadiusDefault = 0;
        public enum SpecialInflowEquationType { STD, NO, RG, YES, PP, GPP };
        public const SpecialInflowEquationType SpecialInflowEquationDefalut = SpecialInflowEquationType.STD;
        public enum InstructionsForAutomaticShutInType { STOP, SHUT };
        public const InstructionsForAutomaticShutInType InstructionsForAutomaticShutInDefault = InstructionsForAutomaticShutInType.SHUT;
        public enum CrossflowAbilityType { YES, NO };
        public const CrossflowAbilityType CrossflowAbilityDefault = CrossflowAbilityType.YES;
        public const int PressureTableNumberDefault = 0;
        public enum DensityCalculationType { SEG, AVG };
        public const DensityCalculationType DensityCalculationDefault = DensityCalculationType.AVG;
        public const int FIPNumDefault = 0;
        public enum WellModelType { STD, HMIW };
        public const WellModelType WellModelDefault = WellModelType.STD;

        override public string Line()
        {
            List<object> result = new List<object>();
            result.Add(WellName is null ? default_value : WellName);
            result.Add(GroupName);
            result.Add(ILocation.ToString());
            result.Add(JLocation.ToString());
            result.Add(ReferenceDepth is null ? default_value : ReferenceDepth.ToString());
            switch (PreferredPhase)
            {
                case PreferredPhaseType.OIL: result.Add("OIL"); break;
                case PreferredPhaseType.WATER: result.Add("WATER"); break;
                case PreferredPhaseType.GAS: result.Add("GAS"); break;
                case PreferredPhaseType.LIQ: result.Add("LIQ"); break;
            }
            result.Add(DrainageRadius == DrainageRadiusDefault ? default_value : DrainageRadius.ToString());
            switch (SpecialInflowEquation)
            {
                case SpecialInflowEquationType.STD: result.Add(default_value); break;
                case SpecialInflowEquationType.NO: result.Add(default_value); break;
                case SpecialInflowEquationType.RG: result.Add("R-G"); break;
                case SpecialInflowEquationType.YES: result.Add("YES"); break;
                case SpecialInflowEquationType.PP: result.Add("P-P"); break;
                case SpecialInflowEquationType.GPP: result.Add("GPP"); break;
            }
            switch (InstructionsForAutomaticShutIn)
            {
                case InstructionsForAutomaticShutInType.SHUT: result.Add(default_value); break;
                case InstructionsForAutomaticShutInType.STOP: result.Add("STOP"); break;
            }
            switch (CrossflowAbility)
            {
                case CrossflowAbilityType.YES: result.Add(default_value); break;
                case CrossflowAbilityType.NO: result.Add("NO"); break;
            }
            result.Add(PressureTableNumber == PressureTableNumberDefault ? default_value : PressureTableNumber.ToString());
            switch (DensityCalculation)
            {
                case DensityCalculationType.SEG: result.Add(default_value); break;
                case DensityCalculationType.AVG: result.Add("AVG"); break;
            }
            result.Add(FIPNum == FIPNumDefault ? default_value : FIPNum.ToString());
            result.Add(Reserved1);
            result.Add(Reserved2);
            switch (WellModel)
            {
                case WellModelType.STD: result.Add(default_value); break;
                case WellModelType.HMIW: result.Add("HMIW"); break;
            }
            result.Add("/");
            return Join(result);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class WEFAC : KW
    {
        const string title = "WEFAC";
        public WEFAC()
        {
            Title = title;
            Priority = priority_wefac;
        }
        public List<WEFACContent> Data { set; get; } = new List<WEFACContent>();
        override public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.Add(Title);
            foreach (KWContent data in Data)
                result.Add(data.Line());
            result.Add("/");
            result.Add(string.Empty);
            return result;
        }
    }

    public class WEFACContent : KWContent
    {
        public string WellName { set; get; } = string.Empty;
        public double EfficiencyFactor { set; get; } = EfficiencyFactorDefault;

        public const double EfficiencyFactorDefault = 1.0;

        override public string Line()
        {
            List<object> result = new List<object>();
            result.Add(WellName is null ? default_value : WellName);
            result.Add((EfficiencyFactor == EfficiencyFactorDefault) ? default_value : EfficiencyFactor.ToString());
            result.Add("/");
            return Join(result);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class WECON : KW
    {
        const string title = "WECON";
        public WECON()
        {
            Title = title;
            Priority = priority_wecon;
        }
        public List<WECONContent> Data { set; get; } = new List<WECONContent>();
        override public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.Add(Title);
            foreach (KWContent data in Data)
                result.Add(data.Line());
            result.Add("/");
            result.Add(string.Empty);
            return result;
        }
    }

    public class WECONContent : KWContent
    {
        public string WellName { set; get; } = string.Empty;
        public double MinOPR { set; get; } = MinOPRDefault;
        public double MinGPR { set; get; } = MinGPRDefault;
        public double MaxWCT { set; get; } = MaxWCTDefault;
        public double MaxGOR { set; get; } = MaxGORDefault;
        public double MaxWGR { set; get; } = MaxWGRDefault;
        public WorkoverProcedureType WorkoverProcedure { set; get; } = WorkoverProcedureDefault;
        public EndRunFlagType EndRunFlag { set; get; } = EndRunFlagDefault;
        public string FollowOnWellName { set; get; } = default_value;
        public QuantityMinimumEconomicLimitsType QuantityMinimumEconomicLimits { set; get; } = QuantityMinimumEconomicLimitsDefault;
        public double SecondaryMaxWCT { set; get; } = SecondaryMaxWCTDefault;
        public SecondaryWorkoverProcedureType SecondaryWorkoverProcedure { set; get; } = SecondaryWorkoverProcedureDefault;
        public double MaxGLR { set; get; } = MaxGLRDefault;
        public double MaxLPR { set; get; } = MaxLPRDefault;
        public double MaxTemperature { set; get; } = MaxTemperatureDefault;


        public const double MinOPRDefault = 0;
        public const double MinGPRDefault = 0;
        public const double MaxWCTDefault = double.MaxValue;
        public const double MaxGORDefault = double.MaxValue;
        public const double MaxWGRDefault = double.MaxValue;
        public enum WorkoverProcedureType { NONE, CON, plusCON, WELL, PLUG };
        public const WorkoverProcedureType WorkoverProcedureDefault = WorkoverProcedureType.NONE;
        public enum EndRunFlagType { YES, NO };
        public const EndRunFlagType EndRunFlagDefault = EndRunFlagType.NO;
        public enum QuantityMinimumEconomicLimitsType { RATE, POTN };
        public const QuantityMinimumEconomicLimitsType QuantityMinimumEconomicLimitsDefault = QuantityMinimumEconomicLimitsType.RATE;
        public const double SecondaryMaxWCTDefault = 0;
        public enum SecondaryWorkoverProcedureType { NONE, CON, plusCON, WELL, PLUG, LAST };
        public const SecondaryWorkoverProcedureType SecondaryWorkoverProcedureDefault = SecondaryWorkoverProcedureType.NONE;
        public const double MaxGLRDefault = double.MaxValue;
        public const double MaxLPRDefault = 0;
        public const double MaxTemperatureDefault = double.MaxValue;


        override public string Line()
        {
            List<object> result = new List<object>();
            result.Add(WellName is null ? default_value : WellName);
            result.Add(MinOPR == MinOPRDefault ? default_value : MinOPR.ToString());
            result.Add(MinGPR == MinGPRDefault ? default_value : MinGPR.ToString());
            result.Add(MaxWCT == MaxWCTDefault ? default_value : MaxWCT.ToString());
            result.Add(MaxGOR == MaxGORDefault ? default_value : MaxGOR.ToString());
            result.Add(MaxWGR == MaxWGRDefault ? default_value : MaxWGR.ToString());
            switch(WorkoverProcedure)
            {
                case WorkoverProcedureType.NONE: result.Add(default_value); break;
                case WorkoverProcedureType.CON:  result.Add("CON"); break;
                case WorkoverProcedureType.plusCON: result.Add("+CON"); break;
                case WorkoverProcedureType.WELL: result.Add("WELL"); break;
                case WorkoverProcedureType.PLUG: result.Add("PLUG"); break;
            }
            switch (EndRunFlag)
            {
                case EndRunFlagType.NO: result.Add(default_value); break;
                case EndRunFlagType.YES: result.Add("YES"); break;
            }
            result.Add(FollowOnWellName);
            switch (QuantityMinimumEconomicLimits)
            {
                case QuantityMinimumEconomicLimitsType.RATE: result.Add(default_value); break;
                case QuantityMinimumEconomicLimitsType.POTN: result.Add("POTN"); break;
            }
            result.Add(SecondaryMaxWCT == SecondaryMaxWCTDefault ? default_value : SecondaryMaxWCT.ToString());
            switch (SecondaryWorkoverProcedure)
            {
                case SecondaryWorkoverProcedureType.NONE: result.Add(default_value); break;
                case SecondaryWorkoverProcedureType.CON: result.Add("CON"); break;
                case SecondaryWorkoverProcedureType.plusCON: result.Add("+CON"); break;
                case SecondaryWorkoverProcedureType.WELL: result.Add("WELL"); break;
                case SecondaryWorkoverProcedureType.PLUG: result.Add("PLUG"); break;
                case SecondaryWorkoverProcedureType.LAST: result.Add("LAST"); break;
            }
            result.Add(MaxGLR == MaxGLRDefault ? default_value : MaxGLR.ToString());
            result.Add(MaxLPR == MaxLPRDefault ? default_value : MaxLPR.ToString());
            result.Add(MaxTemperature == MaxTemperatureDefault ? default_value : MaxTemperature.ToString());
            result.Add("/");
            return Join(result);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public class DATES : KW
    {
        const string DATES_Title = "DATES";
        public DATES()
        {
            Title = DATES_Title;
            Priority = priority_dates;
        }

        override public List<string> Lines()
        {
            List<string> result = new List<string>();
            result.Add(Title);
            // 1 JAN 1987 19:45:15.3333 /
            if (Date.Hour == 0 && Date.Minute == 0 && Date.Second == 0)
                result.Add(Date.ToString("dd MMM yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " /");
            else
                result.Add(Date.ToString("dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " /");
            result.Add("/");
            result.Add(string.Empty);
            return result;
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
