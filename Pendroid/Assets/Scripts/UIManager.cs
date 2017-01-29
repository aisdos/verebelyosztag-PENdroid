using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] private GameObject[] UIs;
	[SerializeField] private Button deadUI_retry;
	[SerializeField] private Button pause;
	[SerializeField] private Button pauseUI_back;
	private static GameObject[] _UIs;

	void Start() {
		_UIs = UIs;
	}

	void Awake() {
		deadUI_retry.onClick.AddListener (() => {
			GameManager.ReloadScene();
		});
		pauseUI_back.onClick.AddListener (() => {
			SwitchUI("HUD");
			GameManager.Pause(false);
		});
		pause.onClick.AddListener (() => {
			SwitchUI("Pause");
			GameManager.Pause(true);
		});
	}

	public static void SwitchUI(string s) {
		bool exist = false;
		foreach(GameObject g in _UIs)
		{
			if (g.transform.name == s)
				exist = true;
		}
		if (exist)
		{
			foreach(GameObject g in _UIs)
			{
				print ("asd");
				if (g.transform.name == s)
					g.SetActive(true);
				else
					g.SetActive(false);
			}
		}
	}

	/*public static void DeadUI() {
		_deadUI.SetActive (true);
	}
	public void PauseUI(bool b) {
		pauseUI.SetActive (b);
	}*/
}
