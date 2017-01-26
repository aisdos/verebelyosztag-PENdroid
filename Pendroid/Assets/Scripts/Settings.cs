using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

	[SerializeField] private Slider volume_slider;

	void Update () {
		AudioListener.volume = volume_slider.value;
	}
}
