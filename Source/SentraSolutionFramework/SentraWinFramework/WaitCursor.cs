using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SentraWinFramework
{
    [DebuggerNonUserCode]
    public class WaitCursor : IDisposable
    {
        private static Cursor Current;
        private static int RefCtr = 0;
        public WaitCursor() : this(true) { }
        public WaitCursor(bool CallDoEvent)
        {
            if (RefCtr == 0)
            {
                if (CallDoEvent) Application.DoEvents();
                Current = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
            }
            RefCtr++;
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            RefCtr--;
            if (RefCtr == 0) Cursor.Current = Current;
        }

        #endregion
    } 
}
