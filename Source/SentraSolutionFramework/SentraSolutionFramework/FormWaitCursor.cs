using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SentraSolutionFramework
{
    public delegate object DoWaitCursor(bool CallDoEvent);
    public delegate void DoRestoreCursor(object LastCursor);

    public static class _IWaitCursor
    {
        public static DoWaitCursor DoWaitCursor;
        public static DoRestoreCursor DoRestoreCursor;
    }

    [DebuggerNonUserCode]
    public class FormWaitCursor : IDisposable
    {
        private static object Current;
        private static int RefCtr = 0;
        public FormWaitCursor() : this(true) { }
        public FormWaitCursor(bool CallDoEvent)
        {
            if (RefCtr == 0)
                Current = _IWaitCursor.DoWaitCursor(CallDoEvent);
            RefCtr++;
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            RefCtr--;
            if (RefCtr == 0) _IWaitCursor.DoRestoreCursor(Current);
        }

        #endregion
    } 
}
