using System.Windows;
using System.Windows.Input;

namespace SpaceInvaders
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly SpaceInvadersViewModel _viewModel;
		public MainWindow()
		{
			InitializeComponent();
			DataContext = _viewModel = new SpaceInvadersViewModel();
		}

		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.A || e.Key == Key.Left)
			{
				_viewModel.MovePlayer(Direction.Left);
			}
			else if (e.Key == Key.D || e.Key == Key.Right)
			{
				_viewModel.MovePlayer(Direction.Right);
			}
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			_viewModel.StartGame();
		}
	}
}
