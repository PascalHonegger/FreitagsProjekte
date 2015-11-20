﻿using System.Windows.Controls;

namespace SpaceInvaders.UserControls
{
	/// <summary>
	/// Interaction logic for ShipControl.xaml
	/// </summary>
	public partial class ShipControl : UserControl
	{
		public ShipControl()
		{
			InitializeComponent();
		}

		public void InvaderShot()
		{
			InvaderShotStoryboard.Begin();
		}

		public void StartFlashing()
		{
			ShipTexture.BeginStoryboard(FlashStoryBoard);
		}

		public void StopFlashing()
		{
			ShipTexture.Stop();
		}
	}
}
