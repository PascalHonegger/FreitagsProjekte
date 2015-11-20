using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpaceInvaders
{
	class SpaceInvadersViewModel
	{
		private readonly ObservableCollection<FrameworkElement> _sprites = new ObservableCollection<FrameworkElement>();

		public INotifyCollectionChanged Sprites => _sprites;

		public bool GameOver => _model.GameOver;

		private readonly ObservableCollection<object> _lives = new ObservableCollection<object>();

		public INotifyCollectionChanged Lives => _lives;

		public bool Paused { get; set; }
		private bool _lastPaused = true;

		public static double Scale { get; private set; }

		public int Score { get; private set; }

		public Size PlayAreaSize
		{
			set
			{
				Scale = value.Width/405;
				_model.UpdateAllShipsAndStars();
				RecreateScanLines();
			}
		}
		private readonly SpaceInvadersModel _model = new SpaceInvadersModel();

	}
}
