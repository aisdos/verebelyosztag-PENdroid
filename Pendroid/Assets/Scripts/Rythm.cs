using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rythm : MonoBehaviour {

	//
	//			MŰKÖDJ!!!!!!
	//			Ritmus rendszer szombaton - Norbi
	//

	[SerializeField] private float beginDelay = 0;
	[SerializeField] private float beatDelay = 1;
	[SerializeField] private Image indicator;
	public static float BeginDelay;
	public static float BeatDelay;				//	Just For Fun 
	private static float toNextBeat = 1;
	public static float indicatorFillAmount = 0;

	void Start () {
		BeginDelay = beginDelay;
		BeatDelay = beatDelay;
		toNextBeat = beginDelay;
	}

	void Update() {
		toNextBeat -= Time.deltaTime;
		if (toNextBeat < 0) {
			toNextBeat = beatDelay;
		}

		indicatorFillAmount = (toNextBeat / beatDelay);
		indicator.fillAmount = indicatorFillAmount;
	}

	public static float NextBeat() {
		return toNextBeat;
	}

	public static void Beat() {
		toNextBeat = BeatDelay + toNextBeat;
	}
}
