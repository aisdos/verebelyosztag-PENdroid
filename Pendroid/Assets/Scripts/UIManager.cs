using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	[SerializeField] private GameObject deadUI;
	[SerializeField] private Button deadUI_retry;
	private static GameObject _deadUI;

	void Awake() {
		_deadUI = deadUI;
		deadUI_retry.onClick.AddListener (() => {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		});
	}

	void Start () {
		
	}

	public static void DeadUI() {
		_deadUI.SetActive (true);
	}
}
