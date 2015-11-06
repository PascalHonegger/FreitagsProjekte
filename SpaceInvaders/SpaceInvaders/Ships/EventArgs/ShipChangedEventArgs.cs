using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Ships.EventArgs
{
	class ShipChangedEventArgs
	{
		public IShip ShipUpdated { get; private set; }
		public bool Killed { get; private set; }

		public ShipChangedEventArgs(IShip shipUpdated, bool killed)
		{
			ShipUpdated = shipUpdated;
			Killed = killed;
		}
	}
}
