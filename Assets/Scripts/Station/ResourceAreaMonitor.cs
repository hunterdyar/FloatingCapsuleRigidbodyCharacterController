using System;
using System.Collections.Generic;
using System.Linq;
using Resources;
using UnityEngine;
using UnityEngine.Serialization;

namespace Station
{
	public class ResourceAreaMonitor : MonoBehaviour
	{
		public Action OnResourcesChange;
		
		//Tracks the number of Resources in a station.
		//for example, used by power cell area to track power cells.
		public int ResourceCount => GetResourceCount();

		[SerializeField] private ShipResource _resourceToMonitor;
		private List<ResourceElement> _resourcesInArea;
		private Collider _collider;

		private void Awake()
		{
			_resourcesInArea = new List<ResourceElement>();
			_collider = GetComponent<Collider>();
		}

		private int GetResourceCount()
		{
			//probably a way to do this with linq aggregate
			int count = 0;
			foreach (var res in _resourcesInArea)
			{
				count += res.Count;
			}

			return count;
		}

		private void OnTriggerEnter(Collider other)
		{
			var rese = other.GetComponent<ResourceElement>();
			if (rese != null && rese.Resource == _resourceToMonitor)
			{
				if (!_resourcesInArea.Contains(rese))
				{
					_resourcesInArea.Add(rese);
					OnResourcesChange?.Invoke();
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			var rese = other.GetComponent<ResourceElement>();
			if (rese != null)
			{
				if (_resourcesInArea.Contains(rese))//no need to check if the element matches, it probably wont be slower than checking it its in the list or not.
				{
					_resourcesInArea.Remove(rese);
					OnResourcesChange?.Invoke();
				}
			}
		}
	}
}