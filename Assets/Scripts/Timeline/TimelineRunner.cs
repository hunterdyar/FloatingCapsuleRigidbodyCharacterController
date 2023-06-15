using System.Collections;
using System.Collections.Generic;
using Timeline;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TimelineRunner : MonoBehaviour
{
	public GameTimeline Timeline;

	private Coroutine timelineRoutine;
	//on enter gameplay state
	void Start()
	{
		timelineRoutine=StartCoroutine(Timeline.RunTimeline());
	}
}
