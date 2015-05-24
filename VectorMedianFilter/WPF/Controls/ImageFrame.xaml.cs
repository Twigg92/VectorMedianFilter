using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VectorMedianFilter.WPF.Controls
{
    /// <summary>
    /// Description for ImageFrame.
    /// </summary>
    public partial class ImageFrame : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the ImageFrame class.
        /// </summary>
        public ImageFrame()
        {
            InitializeComponent();
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageFrame), new PropertyMetadata());

        public String ImageText
        {
            get { return (String)GetValue(ImageTextProperty); }
            set { SetValue(ImageTextProperty, value); }
        }

        public static readonly DependencyProperty ImageTextProperty =
            DependencyProperty.Register("ImageText", typeof(String), typeof(ImageFrame), new PropertyMetadata());
    }
}