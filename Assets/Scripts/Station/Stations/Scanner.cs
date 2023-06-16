﻿using Timeline;
using UnityEngine;

namespace Ship
{
	public class Scanner : Station
	{
		public GameTimeline Timeline;
		protected override void DoStationAction()
		{
			//todo: need to add that you can't scan while moving,and i would like to do that in Try, not Do.
			base.DoStationAction();
			var scan = Timeline.TryScanForShipEvents(transform.rotation.eulerAngles.y.RoundAndNormalizeDegrees360(), out var results);
			if (scan)
			{
				Debug.Log($"Scan successful! In {results.BeatsUntilEvent} turns there will be a {results.ScannedEvent.displayName} at {results.ScannedEvent.eventLocation} degrees.");
			}
			else
			{
				Debug.Log($"Scan unsuccessful!");
			}
		}
	}
}