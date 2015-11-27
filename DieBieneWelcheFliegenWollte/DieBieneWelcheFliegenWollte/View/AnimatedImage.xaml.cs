using System;
using System.Collections.Generic;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DieBieneWelcheFliegenWollte.View
{
	/// <summary>
	/// Interaction logic for AnimatedImage.xaml
	/// </summary>
	public partial class AnimatedImage : UserControl
	{
		public AnimatedImage()
		{
			InitializeComponent();
		}

		public AnimatedImage(IEnumerable<string> imageNames, TimeSpan interval) : this()
		{
			StartAnimation(imageNames, interval);
		}

		public void StartAnimation(IEnumerable<string> imageNames, TimeSpan interval)
		{
			var storyboard = new Storyboard();
			var animation = new ObjectAnimationUsingKeyFrames();
			Storyboard.SetTarget(animation, Image);
			Storyboard.SetTargetProperty(animation, new PropertyPath("Source"));

			var currentInterval = TimeSpan.FromMilliseconds(0);
			foreach (var imageName in imageNames)
			{
				var keyFrame = new DiscreteObjectKeyFrame
				{
					Value = CreateImageFromAssets(imageName),
					KeyTime = currentInterval
				};
				animation.KeyFrames.Add(keyFrame);
				currentInterval = currentInterval.Add(interval);
			}
			storyboard.RepeatBehavior = RepeatBehavior.Forever;
			storyboard.AutoReverse = true;
			storyboard.Children.Add(animation);
			storyboard.Begin();
		}

		private static BitmapImage CreateImageFromAssets(string imageName)
		{
			try
			{
				var uri = new Uri(imageName, UriKind.RelativeOrAbsolute);
				return new BitmapImage(uri);

			}
			catch (System.IO.IOException)
			{
				return new BitmapImage();
			}
		}
	}
}
