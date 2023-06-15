using UnityEngine;

namespace Ship
{
	public class EnergyHarvester : Station
	{
		//it might NOT be a station?
		
		//it can be turned on/off. 
		
		//It's a converter. It has an input resource, an output resource, a number of times per beat to generate, and a distribution curve.
		//every frame we check if the current time left in the beat is, on the distribution curve, larger than the next threshold.
		
		//what powers the energy harvester?
		//It also creates WASTE, which need to be sent in the trash chute.
		
		//are there other converters?
	}
}