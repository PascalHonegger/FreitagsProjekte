using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DieBieneWelcheFliegenWollte.View
{
	/// <summary>
	///     Interaction logic for AnimatedImage.xaml
	/// </summary>
	public partial class AnimatedImage
	{
		private readonly Dictionary<string, ImageBrush> _cachedImages = new Dictionary<string, ImageBrush>();
		private readonly MediaPlayer _player = new MediaPlayer();

		public AnimatedImage()
		{
			InitializeComponent();
		}

		public AnimatedImage(int width, int heigth) : this()
		{
			Width = width;
			Height = heigth;
		}

		public void PlaySummSummSumm()
		{
			_player.Open(new Uri("Resources/summen.mp3", UriKind.Relative));
			_player.Play();

			_player.MediaEnded += (sender, args) => _player.Play();
		}

		public void AnimateImage(IEnumerable<string> imageNames, TimeSpan interval)
		{
			var storyboard = new Storyboard
			{
				RepeatBehavior = RepeatBehavior.Forever
			};

			var animation = new ObjectAnimationUsingKeyFrames();
			Storyboard.SetTarget(animation, this);
			Storyboard.SetTargetProperty(animation, new PropertyPath("Background"));

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

			storyboard.Children.Add(animation);

			storyboard.Begin();
		}

		public void MoveImage(TimeSpan interval, PathGeometry beeFlyHerePath)
		{
			var storyboard = new Storyboard
			{
				RepeatBehavior = RepeatBehavior.Forever
			};

			var moveCircleAnimation = new DoubleAnimationUsingPath
			{
				PathGeometry = beeFlyHerePath,
				Source = PathAnimationSource.X,
				Duration = interval
			};

			Storyboard.SetTarget(moveCircleAnimation, this);
			Storyboard.SetTargetProperty(moveCircleAnimation, new PropertyPath("(Canvas.Left)"));

			var moveCircleAnimation2 = new DoubleAnimationUsingPath
			{
				PathGeometry = beeFlyHerePath,
				Source = PathAnimationSource.Y,
				Duration = interval
			};

			Storyboard.SetTarget(moveCircleAnimation2, this);
			Storyboard.SetTargetProperty(moveCircleAnimation2, new PropertyPath("(Canvas.Top)"));

			storyboard.Children.Add(moveCircleAnimation);
			storyboard.Children.Add(moveCircleAnimation2);

			storyboard.Begin();
		}

		private ImageBrush CreateImageFromAssets(string imageFileName)
		{
			try
			{
				if (_cachedImages.ContainsKey(imageFileName))
					return _cachedImages[imageFileName];


				var uri = new Uri("Resources/" + imageFileName, UriKind.Relative);
				var newBrush = new ImageBrush(new BitmapImage(uri));
				_cachedImages.Add(imageFileName, newBrush);
				return newBrush;
			}
			catch (IOException)
			{
				return new ImageBrush(new BitmapImage());
			}
		}
	}
}