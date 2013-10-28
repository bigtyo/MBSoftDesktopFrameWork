using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Localization;
using System.Diagnostics;

namespace SentraWinFramework.Localizer
{
    [DebuggerNonUserCode]
    internal class BahasaGridLocalizer : GridLocalizer
    {
        public override string Language { get { return "Bahasa"; } }
        public override string GetLocalizedString(GridStringId id)
        {
            switch(id) {
                case GridStringId.MenuFooterMaxFormat:
                    return "MAX={0:#,###.##;;0}";
                case GridStringId.MenuFooterMinFormat:
                    return "MIN={0:#,###.##;;0}";
                case GridStringId.MenuFooterSumFormat:
                    return "SUM={0:#,###.##;;0}";
                case GridStringId.MenuFooterCountFormat:
                    return "CNT={0:#,###.##;;0}";
                case GridStringId.MenuFooterAverageFormat:
                    return "AVG={0:#,###.##;;0}";
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
}
