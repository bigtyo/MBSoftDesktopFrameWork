using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SentraUtility
{
    public class PrintDirect
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, long pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        private static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
       {
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();

            di.pDocName = "Sentra Direct-Print";
            di.pDataType = "RAW";

            if (!OpenPrinter(szPrinterName, out hPrinter, 0))
                return false;

            if (!StartDocPrinter(hPrinter, 1, di))
            {
                ClosePrinter(hPrinter);
                return false;
            }

            if (!StartPagePrinter(hPrinter)) 
            {
                EndDocPrinter(hPrinter);
                ClosePrinter(hPrinter);
            }

            try
            {
                Int32 dwWritten;
                bool bSuccess = WritePrinter(hPrinter, pBytes, 
                    dwCount, out dwWritten);
                return bSuccess && dwCount == dwWritten;
            }
            finally
            {
                EndPagePrinter(hPrinter);
                EndDocPrinter(hPrinter);
                ClosePrinter(hPrinter);
            }
            return false;
        }

        private const string CMD_ESC = "\x1b";

        public static string EscCommand(params char[] Commands)
        {
            return CMD_ESC + string.Concat(Commands);
        }
        public static string SetFontQuality(bool IsNearLetterQuality)
        {
            return IsNearLetterQuality ? EscCommand('x', '\x1') : EscCommand('x', '\x0');
        }
        public static string SetFontExpanded(bool IsExpanded)
        {
            return IsExpanded ? EscCommand('W', '\x1') : EscCommand('x', '\x0');
        }
        public static string SetFontDoubleHeight(bool IsDoubleHeight)
        {
            return IsDoubleHeight ? EscCommand('w', '\x1') : EscCommand('w', '\x0');
        }
        public static string SetFontUnderline(bool IsUnderline)
        {
            //return IsUnderline ? EscCommand('-', '\x1') : EscCommand('-', '\x0');
            return EscCommand((char)45, IsUnderline ? (char)49 : (char)48);
        }
        public static string SetFontSuperscript(bool IsSuperscript)
        {
            return IsSuperscript ? EscCommand('S', '\x1') : EscCommand('S', '\x0');
        }
        public static string SetFontBold(bool IsBold)
        {
            return EscCommand(IsBold ? (char)69 : (char)70);
        }
        public static string SetFontItalic(bool IsItalic)
        {
            return EscCommand(IsItalic ? (char)52 : (char)53);
        }
        public static string SetFontStyle(int FontStyle)
        {
            return EscCommand('k',(char) FontStyle);
        }
        public static string SetFontCondensed(bool IsCondensed)
        {
            return IsCondensed ? ((char)15).ToString() : ((char)18).ToString();
        }
        public static string SetFont10CPI()
        {
            return EscCommand((char)80);
        }
        public static string SetFont12CPI()
        {
            return EscCommand((char)77);
        }
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;

            // How many characters are in the string?

            dwCount = szString.Length;

            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.

            pBytes = Marshal.StringToCoTaskMemAnsi(szString);

            // Send the converted ANSI string to the printer.
            try
            {
                return SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pBytes);
            }
        }
    }
}
