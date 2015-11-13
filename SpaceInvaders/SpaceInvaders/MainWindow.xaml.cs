using System.Windows;
using System.Windows.Input;

namespace SpaceInvaders
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private SpaceInvadersViewModel SpaceInvaders => DataContext as SpaceInvadersViewModel;
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new SpaceInvadersViewModel();
		}

		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.A || e.Key == Key.Left)
			{
				SpaceInvaders.MovePlayer(Direction.Left);
			}
			else if (e.Key == Key.D || e.Key == Key.Right)
			{
				SpaceInvaders.MovePlayer(Direction.Right);
			}
			else if(e.Key == Key.Space)
			{
				SpaceInvaders.FireShotPlayer();
			}
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			SpaceInvaders.StartGame();
		}

		private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
		{

		}
	}
}
