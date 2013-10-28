using System;
using System.Collections.Generic;
using System.Text;
using SentraUtility.Expression;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace SentraWinFramework
{
    public interface IInitGrid
    {
        void InitGrid(GridView GridView);
    }

    public interface IGridSelected
    {
        void GridSelected(object Data);
    }
}
