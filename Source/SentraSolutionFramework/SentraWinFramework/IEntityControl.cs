using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Windows.Forms;

namespace SentraWinFramework
{
    public interface IEntityControl
    {
        void InitNavigator(IUINavigator Navigator);
    }
}
