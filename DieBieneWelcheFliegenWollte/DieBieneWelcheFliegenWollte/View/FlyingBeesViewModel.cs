using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DieBieneWelcheFliegenWollte.View
{
	public class FlyingBeesViewModel
	{
		private static readonly IEnumerable<string> ImageNames = new List<string>
		{
			"biene1.png",
			"biene2.png",
			"biene3.png",
			"biene3.png"
		};

		private readonly PathGeometry _beeFlyHerePath;

		private readonly Canvas _beeNest;
		private readonly Random _random;

		public FlyingBeesViewModel(Canvas beeNest, PathGeometry beeFlyHerePath)
		{
			_beeNest = beeNest;
			_beeFlyHerePath = beeFlyHerePath;
			_random = new Random();
		}

		private int RandomInt => _random.Next(150, 500);

		public async Task AddRandomBee()
		{
			await Application.Current.MainWindow.Dispatcher.InvokeAsync(() =>
			{
				var bee = new AnimatedImage(RandomInt, RandomInt);

				_beeNest.Children.Add(bee);

				AnimateBee(bee);
				bee.MoveImage(TimeSpan.FromMilliseconds(_random.Next(100, 10000)), _beeFlyHerePath);
				bee.PlaySummSummSumm();
			});
		}

		public void AnimateBee(AnimatedImage bee)
		{
			bee.AnimateImage(ImageNames, TimeSpan.FromMilliseconds(RandomInt));
		}
	}
}