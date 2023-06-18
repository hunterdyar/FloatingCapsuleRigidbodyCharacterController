using System;
using Ship;

namespace Timeline
{
	[Serializable]
	public class ShipEvent
	{
		//A ship event is a single thing that happens to a ship. It could be an enemy attack, storm, etc.
		//todo naming
		public string displayName;
		public Sector sector;
		public int damage;
		public ShipDamageType DamageType;
		public StatusEffect StatusEffect;

		public ShipEvent(ShipEvent clone)
		{
			displayName = clone.displayName;
			sector = clone.sector;
			damage = clone.damage;
			DamageType = clone.DamageType;
		}
		//Asteroid Impact


		public MessageInfo GetMessage()
		{
			return new MessageInfo("Event: " + displayName);
		}
	}
}