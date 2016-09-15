using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DieBieneWelcheFliegenWollte.View
{
	/// <summary>
	///     Interaction logic for FlyingBees.xaml
	/// </summary>
	public partial class FlyingBees
	{
		private readonly FlyingBeesViewModel _viewModel;

		public FlyingBees()
		{
			InitializeComponent();

			DataContext = _viewModel = new FlyingBeesViewModel(BeeNest, BeeFlyHerePath);

			_viewModel.AnimateBee(AddBee);
		}

		private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			await _viewModel.AddRandomBee();
		}

		private readonly Random _random = new Random();

		private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			RandomlyPlaceButton((Button)sender);
		}

		private void RandomlyPlaceButton(Button button)
		{
			Canvas.SetTop(button, _random.Next(0, 700));
			Canvas.SetLeft(button, _random.Next(100, 1000));
		}
	}
}