using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

	[SerializeField] private Slider volume_slider;
	[SerializeField] private Text volume_text;
	[SerializeField] private Slider dpad_slider;
	[SerializeField] private Text dpad_text;
	public static float volume;
	public static float dpad;

	void Update () {
		volume = volume_slider.value;
		dpad = dpad_slider.value;

		AudioListener.volume = volume_slider.value;
		volume_text.text = string.Format("Hangerő [{0:0}%]",100*volume_slider.value);
		dpad_text.text = string.Format("DPad mérete [{0:0}%]",100*dpad_slider.value);


	}
}
