using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MedianVectorFilter.Factories;
using MedianVectorFilter.Filtering;
using MedianVectorFilter.Loaders;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VectorMedianFilter.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ApplicationTitle = "Vector Median Filter";
            CurrentStatus = "Wczytaj obraz aby rozpocz¹æ";
            LoadImageCommand = new RelayCommand(LoadImage);
            TransformImageCommand = new RelayCommand(TransformImage, () => IsImageLoaded);
        }

        #endregion

        #region Fields

        private Bitmap bitmap;

        #endregion

        #region Properties

        public string ApplicationTitle { get; set; }

        private string _currentStatus;
        public string CurrentStatus 
        {
            get { return _currentStatus; }
            set
            {
                _currentStatus = value;
                RaisePropertyChanged(() => CurrentStatus);
            }
        }

        private ImageSource _image;
        public ImageSource Image 
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        private ImageSource _transformedImage;
        public ImageSource TransformedImage
        {
            get { return _transformedImage; }
            set
            {
                _transformedImage = value;
                RaisePropertyChanged(() => TransformedImage);
            }
        }

        private int _maskSize = 3;
        public int MaskSize
        {
            get { return _maskSize; }
            set
            {
                _maskSize = value;
                RaisePropertyChanged(() => MaskSize);
            }
        }

        private Boolean _isImageLoaded = false;
        public Boolean IsImageLoaded 
        {
            get { return _isImageLoaded; }
            set
            {
                _isImageLoaded = value;
                RaisePropertyChanged(() => IsImageLoaded);
            }
        }

        private Boolean _isImageTransformed = false;
        public Boolean IsImageTransformed
        {
            get { return _isImageTransformed; }
            set
            {
                _isImageTransformed = value;
                RaisePropertyChanged(() => IsImageTransformed);
            }
        }

        private string _transformedImageText;
        public string TransformedImageText 
        {
            get { return _transformedImageText; }
            set
            {
                _transformedImageText = value;
                RaisePropertyChanged(() => TransformedImageText);
            }
        }

        #endregion

        #region Commands

        public RelayCommand LoadImageCommand { get; set; }
        public void LoadImage()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "jpg files (*.jpg)|*.jpg";

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    // Create a new Bitmap object from the picture file on disk,
                    // and assign that to the PictureBox.Image property
                    Image = BitmapToImageSource(new Bitmap(dlg.FileName));
                    CurrentStatus = "Obraz wczytano pomyœlnie";

                    IsImageLoaded = true;
                    TransformImageCommand.RaiseCanExecuteChanged();

                    bitmap = ImageLoader.LoadAsBitmap(dlg.FileName);
                    IsImageTransformed = false;
                }
            }
        }

        public RelayCommand TransformImageCommand { get; set; }
        public void TransformImage()
        {
            if (IsMaskCorrect())
            {
                var mask = MaskFactory.CreateMask(MaskSize);
                VmfFilter.ApplyOnBitmap(bitmap, mask);

                TransformedImage = BitmapToImageSource(bitmap);
                TransformedImageText = "Filtr medianowy " + MaskSize + "x" + MaskSize;
                IsImageTransformed = true;

                CurrentStatus = "Na³o¿ono filtr medianowy";
            }
        }

        #endregion

        #region Methods

        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public Boolean IsMaskCorrect()
        {
            if ((MaskSize % 2).Equals(0) || (MaskSize < 2))
            {
                CurrentStatus = "Niepoprawna wartoœæ maski";
                return false;
            }
            return true;
        }

        #endregion
    }
}