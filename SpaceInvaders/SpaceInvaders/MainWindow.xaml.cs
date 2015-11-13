﻿using System;
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

		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.A || e.Key == Key.Left)
			{
				ViewModel.MovePlayer(Direction.Left);
			}
			else if (e.Key == Key.D || e.Key == Key.Right)
			{
				ViewModel.MovePlayer(Direction.Right);
			}
			else if (e.Key == Key.Space)
			{
				ViewModel.FireShotPlayer();
			}
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.StartGame();
		}

		private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			UpdatePlayAreaSize(new Size(e.NewSize.Width, e.NewSize.Height - 160));
		}

		private void PlayArea_OnLoaded(object sender, RoutedEventArgs e)
		{
			UpdatePlayAreaSize(PlayArea.RenderSize);
		}

		private void UpdatePlayAreaSize(Size newPlayAreaSize)
		{
			double targetWidth;
			double targetHeight;
			if (newPlayAreaSize.Width > newPlayAreaSize.Height)
			{
				targetWidth = newPlayAreaSize.Height*4/3;
				targetHeight = newPlayAreaSize.Height;
				var leftrightMargin = (newPlayAreaSize.Width - targetWidth)/2;
				PlayArea.Margin = new Thickness(leftrightMargin, 0, leftrightMargin, 0);
			}
			else
			{
				targetHeight = newPlayAreaSize.Width*3/4;
				targetWidth = newPlayAreaSize.Width;
				var topBottomMargin = (newPlayAreaSize.Height - targetHeight)/2;
				PlayArea.Margin = new Thickness(0, topBottomMargin, 0, topBottomMargin);
			}
			PlayArea.Width = targetWidth;
			PlayArea.Height = targetHeight;
			ViewModel.PlayAreaSize = new Size(targetWidth, targetHeight);
		}
	}
}