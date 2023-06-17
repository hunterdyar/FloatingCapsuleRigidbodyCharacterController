using System;
using Ship;

namespace Timeline
{
	[Serializable]
	public class ShipEvent
	{
		//A ship event is a single thing that happens to a ship. It could be an enemy attack, storm, etc.
		public string displayName;
		public Sector sector;
		public int damage;
		public ShipDamageType DamageType;

		public ShipEvent(ShipEvent clone)
		{
			displayName = clone.displayName;
			sector = clone.sector;
			damage = clone.damage;
			DamageType = clone.DamageType;
		}
		//Asteroid Impact
		
		
	}
}