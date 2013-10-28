using System;
using DevExpress.XtraGrid;

namespace SentraWinFramework
{
    public interface IGridRowMover
    {
        bool DeleteConfirm { get; set; }
        string DeleteMessage { get; set; }
        bool DeleteRowVisible { get; set; }
        GridControl GridControl { get; set; }
        bool ScrollVisible { get; set; }

        bool DeleteRowEnabled { get; set; }
        bool ScrollEnabled { get; set; }
    }
}
