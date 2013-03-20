using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSDKAttributeDetection.Perceptual
{
    public class GestureRecognizedEventArgs
    {
        public string Gesture { get; set; }

        public GestureRecognizedEventArgs(string gesture)
        {
            Gesture = gesture;
        }
    }
}
