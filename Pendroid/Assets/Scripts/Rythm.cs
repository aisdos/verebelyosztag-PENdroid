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
	[SerializeField] private Text _judge;
	public static Text judge;
	public static float BeginDelay;
	public static float BeatDelay;				//	Just For Fun 
	private static float toNextBeat = 1;
	public static float indicatorFillAmount = 0;
	public static bool step = false;
	public static bool earlyStep = false;
	public static bool steppedInBeat = false;

	void Start () {
		BeginDelay = beginDelay;
		BeatDelay = beatDelay;
		toNextBeat = beginDelay;
		judge = _judge;
		judge.text = "";
	}

	void Update() {
		toNextBeat -= Time.deltaTime;
		if (toNextBeat < 0) {
			toNextBeat = beatDelay;
			if (!steppedInBeat) 
				judge.text = "Skipped";
			steppedInBeat = false;
		}

		indicatorFillAmount = (toNextBeat / beatDelay);
		indicator.fillAmount = indicatorFillAmount;

		if (indicatorFillAmount <= 0.75f) {
			step = true;
			if (indicatorFillAmount <= 0.75f && indicatorFillAmount >= 0.20f)
				earlyStep = true;
			else
				earlyStep = false;
		} else
			step = false;
	}

	public static float NextBeat() {
		return toNextBeat;
	}



	public static void Beat() {
		toNextBeat = BeatDelay + toNextBeat;
	}
}
