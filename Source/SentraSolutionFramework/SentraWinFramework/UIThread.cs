using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraWinFramework
{
    public delegate void DelFunc();
    public static class UIThread
    {
        public static void BeginInvoke(Form frm, DelFunc del)
        {
            if (frm.InvokeRequired)
                frm.BeginInvoke(del);
            else
                del();
        }
    }
}
