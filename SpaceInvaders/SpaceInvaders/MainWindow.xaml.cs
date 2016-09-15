using System;
using System.Windows;
using System.Windows.Input;

namespace SpaceInvaders
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new SpaceInvadersViewModel();


		}
		
		private SpaceInvadersViewModel ViewModel => DataContext as SpaceInvadersViewModel;
		private SpaceInvadersModel Model => (DataContext as SpaceInvadersViewModel)?.Model;

		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.A || e.Key == Key.Left)
			{
				Model.MovePlayer(Direction.Left);
			}
			else if (e.Key == Key.D || e.Key == Key.Right)
			{
				Model.MovePlayer(Direction.Right);
			}
			else if (e.Key == Key.Space)
			{
				Model.FireShotPlayer();
			}
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			Model.StartGame();
		}
	}
}