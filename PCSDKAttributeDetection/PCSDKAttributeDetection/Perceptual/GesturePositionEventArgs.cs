using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSDKAttributeDetection.Perceptual
{
    public class GesturePositionEventArgs
    {
        private float p1;
        private float p2;
        private string p3;
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }


        public GesturePositionEventArgs(float p1, float p2, string p3, int ScreenWidth, int ScreenHeight)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;
        }
    }
}
