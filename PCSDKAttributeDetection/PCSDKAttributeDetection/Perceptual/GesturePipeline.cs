using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PCSDKAttributeDetection.Logging;

namespace PCSDKAttributeDetection.Perceptual
{
    public class GesturePipeline : AsyncPipelineBase
    {


        public event EventHandler<GesturePositionEventArgs> HandMoved;
//        public event EventHandler<MultipleGestureEventArgs> FingersMoved;
        public event EventHandler<GestureRecognizedEventArgs> GestureRecognized;
        public event EventHandler<ImageCaptureEventArgs> ImageCapture;
        private int ScreenWidth = 1024;
        private int ScreenHeight = 960;



        public GesturePipeline()
        {
            EnableGesture();
            EnableImage(PXCMImage.ColorFormat.COLOR_FORMAT_RGB32);
            searchPattern = GetSearchPattern();
        }
        
        
        public void SetBounds(int width, int height)
        {
            this.ScreenWidth = width;
            this.ScreenHeight = height;
        }


        public override void OnGestureSetup(ref PXCMGesture.ProfileInfo pinfo)
        {
            // Limit how close we have to get.
            pinfo.activationDistance = 70;
            base.OnGestureSetup(ref pinfo);
        }


        public override void OnGesture(ref PXCMGesture.Gesture gesture)
        {
            var handler = GestureRecognized;
            if (handler != null)
            {
                handler(this, new GestureRecognizedEventArgs(gesture.label.ToString()));
            }
            base.OnGesture(ref gesture);
        }
        
        
        public override bool OnNewFrame()
        {
            // We only query the gesture if we are connected. If not, we shouldn't
            // attempt to query the gesture.
            try
            {
                if (!IsDisconnected())
                {
                    HandleGestureFrame();
                    HandleImageFrame();
                }
                Sleep(20);
            }
            catch (Exception ex)
            {
                this.Log(ex);
            }
            return true;
        }


        private void HandleImageFrame()
        {
            var image = QueryImage(PXCMImage.ImageType.IMAGE_TYPE_COLOR);
            System.Drawing.Bitmap b;
            var status = image.QueryBitmap(this.QuerySession(), out b);
            if (status >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                var handler = ImageCapture;
                handler(this, new ImageCaptureEventArgs(b));
            }
        }


        private void HandleGestureFrame()
        {
            var gesture = QueryGesture();
            PXCMGesture.GeoNode[] nodeData = new PXCMGesture.GeoNode[6];
            PXCMGesture.GeoNode singleNode;
            searchPattern = PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY;
            var status = gesture.QueryNodeData(0, GetSearchPattern(), out singleNode);
            if (status >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                var handler = HandMoved;
                if (handler != null)
                {
                    handler(this, new GesturePositionEventArgs(singleNode.positionImage.x - 85,
                    singleNode.positionImage.y - 75,
                    singleNode.body.ToString(),
                    ScreenWidth,
                    ScreenHeight));
                }
            }
        }



        private PXCMGesture.GeoNode.Label searchPattern;
        
        private PXCMGesture.GeoNode.Label GetSearchPattern()
        {
            return PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY |
            PXCMGesture.GeoNode.Label.LABEL_FINGER_INDEX |
            PXCMGesture.GeoNode.Label.LABEL_FINGER_MIDDLE |
            PXCMGesture.GeoNode.Label.LABEL_FINGER_PINKY |
            PXCMGesture.GeoNode.Label.LABEL_FINGER_RING |
            PXCMGesture.GeoNode.Label.LABEL_FINGER_THUMB |
            PXCMGesture.GeoNode.Label.LABEL_HAND_FINGERTIP;
        }
    }
}
