﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DieBieneWelcheFliegenWollte.View
{
	/// <summary>
	/// Interaction logic for FlyingBees.xaml
	/// </summary>
	public partial class FlyingBees : Page
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

			FirstBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(50));
			SecondBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(10));
			ThirdBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(100));

			var storyboard = new Storyboard();
			var animation = new DoubleAnimation();
			Storyboard.SetTarget(animation, FirstBee);
			Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));
			animation.From = 50;
			animation.To = 450;
			animation.Duration = TimeSpan.FromSeconds(3);
			animation.RepeatBehavior = RepeatBehavior.Forever;
			animation.AutoReverse = true;
			storyboard.Begin();
		}
	}
}
