using System;
using System.Collections.Generic;
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
			// navigationHelper = new NavigationHelper(this);
			// navigationHelper.LoadState += navigatioHelper_LoadState;
			// navigationHelper.SaveState += navigatioHelper_SaveState;

			var imageNames = new List<string>
			{
				"biene1.png",
				"biene2.png",
				"biene3.png"
			};

			FirstBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(50));
			SecondBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(10));
			ThirdBee.StartAnimation(imageNames, TimeSpan.FromMilliseconds(100));
		}
	}
}
