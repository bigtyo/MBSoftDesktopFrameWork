using System;
using System.Collections.Generic;
using System.Text;

namespace MicroSysFingerReader
{
    // Class to convert an IPictureDisp image to/from Image
    public class ImageConverter : System.Windows.Forms.AxHost
    {
        // Default constructor
        public ImageConverter()
            : base("59EE46BA-677D-4d20-BF10-8D8067CB8B33")
        // GUID here has no meaning. 
        {
        }

        // Convert an Image to an IPictureDisp
        public static System.Drawing.Image
            IpictureToImage(stdole.IPictureDisp IPicture)
        {
            return ImageConverter.GetPictureFromIPicture(IPicture);
        }
    }
}
