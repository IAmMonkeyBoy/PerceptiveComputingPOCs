using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PCSDKAttributeDetection.Perceptual
{
    public class ImageCaptureEventArgs : EventArgs
    {
        public Bitmap ImageBitmap { get; set; }
        public ImageCaptureEventArgs(System.Drawing.Bitmap imageBitmap)
        {
            // TODO: Complete member initialization
            ImageBitmap = imageBitmap;
        }

    }
}
