using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using SpaceInvaders.Ships;
using SpaceInvaders.Ships.Invader;

namespace SpaceInvaders
{
	class SpaceInvadersViewModel
	{
		private readonly SpaceInvadersModel _model = new SpaceInvadersModel();

		private readonly SpaceInvadersModel _model = new SpaceInvadersModel();
		private readonly DispatcherTimer _timer = new DispatcherTimer();
		private FrameworkElement _playerControl = null;
		private bool _playerFlashing = false;
		private readonly Dictionary<Invader, FrameworkElement> _invaders = 
										new Dictionary<Invader, FrameworkElement>(); 
		private readonly  Dictionary<FrameworkElement, DateTime> _shotInvader =
										new Dictionary<FrameworkElement, DateTime>();
		private readonly Dictionary<Shot, FrameworkElement> _shots =
										new Dictionary<Shot, FrameworkElement>();
		private readonly  Dictionary<Point,FrameworkElement> _stars =
										new Dictionary<Point, FrameworkElement>();
		private readonly List<FrameworkElement> _scanLines =
										new List<FrameworkElement>();
	}
}
