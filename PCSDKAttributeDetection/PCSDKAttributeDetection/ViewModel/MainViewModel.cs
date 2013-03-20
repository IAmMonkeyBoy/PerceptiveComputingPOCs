using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using PCSDKAttributeDetection.Model;
using PCSDKAttributeDetection.Perceptual;


namespace PCSDKAttributeDetection.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        #region DetectedGestures
        /// <summary>
        /// The <see cref="DetectedGestures" /> property's name.
        /// </summary>
        public const string DetectedGesturesPropertyName = "DetectedGestures";

        private ObservableCollection<string> detectedGestures = new ObservableCollection<string>();
        private readonly object detectedGesturesLock = new object();
        /// <summary>
        /// Sets and gets the DetectedGestures property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> DetectedGestures
        {
            get
            {
                return detectedGestures;
            }

            set
            {
                if (detectedGestures == value)
                {
                    return;
                }

                RaisePropertyChanging(DetectedGesturesPropertyName);
                detectedGestures = value;
                RaisePropertyChanged(DetectedGesturesPropertyName);
            }
        }

        #endregion

        #region VideoImageSource
        

        /// <summary>
        /// The <see cref="VideoImageSource" /> property's name.
        /// </summary>
        public const string VideoImageSourcePropertyName = "VideoImageSource";

        private ImageSource videoImageSource = null;

        /// <summary>
        /// Sets and gets the VideoImageSource property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ImageSource VideoImageSource
        {
            get
            {
                return videoImageSource;
            }

            set
            {
                if (videoImageSource == value)
                {
                    return;
                }

                RaisePropertyChanging(VideoImageSourcePropertyName);
                videoImageSource = value;
                RaisePropertyChanged(VideoImageSourcePropertyName);
            }
        }


        #endregion


        #region LastGestureName
        /// <summary>
        /// The <see cref="LastGestureName" /> property's name.
        /// </summary>
        public const string LastGestureNamePropertyName = "LastGestureName";

        private string lastGestureName = null;

        /// <summary>
        /// Sets and gets the LastGestureName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastGestureName
        {
            get
            {
                return lastGestureName;
            }

            set
            {
                if (lastGestureName == value)
                {
                    return;
                }

                RaisePropertyChanging(LastGestureNamePropertyName);
                lastGestureName = value;
                RaisePropertyChanged(LastGestureNamePropertyName);
            }
        }

        #endregion

        GesturePipeline gesturePipeline;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            // this allows us to add detected gestures from other threads and avoids cross thread issues
            BindingOperations.EnableCollectionSynchronization(DetectedGestures, detectedGesturesLock);
            gesturePipeline = new GesturePipeline();
            //gesturePipeline.FingersMoved += gesturePipeline_FingersMoved;
            gesturePipeline.GestureRecognized += gesturePipeline_GestureRecognized;
            gesturePipeline.HandMoved += gesturePipeline_HandMoved;
            gesturePipeline.ImageCapture += gesturePipeline_ImageCapture;
            gesturePipeline.Run();
        }

        void gesturePipeline_ImageCapture(object sender, ImageCaptureEventArgs e)
        {           
            VideoImageSource = e.ImageBitmap.ToBitmapSource();            
        }

        void gesturePipeline_HandMoved(object sender, GesturePositionEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        void gesturePipeline_GestureRecognized(object sender, GestureRecognizedEventArgs e)
        {
            
            LastGestureName = e.Gesture;
            DetectedGestures.Insert(0, e.Gesture);
        }

        void gesturePipeline_FingersMoved(object sender, MultipleGestureEventArgs e)
        {
            //throw new System.NotImplementedException();
        }


        
        
    }
}