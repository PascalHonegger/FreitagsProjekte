using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using JetBrains.dotMemoryUnit;

namespace DieBieneWelcheFliegenWollte.View
{
	/// <summary>
	/// Interaction logic for FlyingBees.xaml
	/// </summary>
	public partial class FlyingBees
	{
		public FlyingBees()
		{
			InitializeComponent();

			var imageNames = new List<string>
			{
				"biene1.png",
				"biene2.png",
				"biene3.png"
			};

			FirstBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(500));
			SecondBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(20));
			ThirdBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(200));

			var storyboard = new Storyboard();
			var animation = new DoubleAnimation();
			Storyboard.SetTarget(animation, FirstBee);
			Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));
			animation.From = 50;
			animation.To = 450;
			animation.Duration = TimeSpan.FromSeconds(3);
			animation.RepeatBehavior = RepeatBehavior.Forever;
			animation.AutoReverse = true;
			storyboard.Children.Add(animation);
			storyboard.Begin();
		}
	}
}
