using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace SentraUtility
{
    /// <summary>
    /// LPrintWriter - A TextWriter derived class for printing text to a print.
    /// </summary>
    /// <example><pre>
    /// LPrintWriter lprint = new LPrintWiter();
    /// 
    /// lprint.WriteLine("Hello, Nurse!");
    /// 
    /// foreach (Char c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
    /// {
    ///     lprint.Write(c);
    /// }
    ///
    /// for(int i = 1; i &lt; 20; i++)
    /// {
    ///     lprint.WriteLine(i);
    /// }
    ///
    /// lprint.Font = new System.Drawing.Font("Arial", 22.0f);
    /// lprint.TextColor = Color.Blue;
    ///
    /// </pre></example>
    ///<remarks>
    /// Copyright &#169; 2006, James M. Curran. <br/>
    /// First published on CodeProject.com, June 2006 <br/>
    /// May be used freely.
    ///</remarks>
    public class LPrintWriter : StringWriter
    {
        #region Private Fields
        private PrintDocument printDocument;
        private string[] lines;
        private int linesPrinted;
        #endregion

        public Font Font = new Font("Courier New", 10.0f);
        public Color TextColor = Color.Black;

        #region Overriddden Methods
        /// <summary>
        /// Closes the current <see cref="T:Util.LPrintWriter"/> and the underlying stream.
        /// </summary>
        public override void Close()
        {
            this.Flush();
            base.Close();
        }
        /// <summary>
        /// Clears all buffers for the current writer and causes any buffered
        /// data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
            if (printDocument == null)
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = new PrintDocument();
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument = printDialog.Document;
                    this.printDocument.BeginPrint += new PrintEventHandler(this.OnBeginPrint);
                    this.printDocument.PrintPage += new PrintPageEventHandler(this.OnPrintPage);

                }
            }
            if (printDocument != null)
            {
                printDocument.Print();
                base.GetStringBuilder().Length = 0;
            }
            base.Flush();
        }
        #endregion

        #region Event Handlers
        // OnBeginPrint 
        /// <summary>
        /// Called when [begin print].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PrintEventArgs"/> instance containing the event data.</param>
        private void OnBeginPrint(object sender, PrintEventArgs e)
        {
            char[] param = { '\n' };
            lines = ToString().Split(param);

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }

        // OnPrintPage
        /// <summary>
        /// Called when [print page].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PrintPageEventArgs"/> instance containing the event data.</param>
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            SizeF size = e.Graphics.MeasureString("W", Font);
            Brush brush = new System.Drawing.SolidBrush(TextColor);
            int DeltaY = (int)size.Height;

            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString(lines[linesPrinted++], Font, brush, x, y);
                y += DeltaY;
                if (y >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            linesPrinted = 0;
            e.HasMorePages = false;
        }
        #endregion
    }
}
