using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject loading;
    [SerializeField] private GameObject[] menus;
    [SerializeField] private Button main_play;
    [SerializeField] private Button main_settings;
    [SerializeField] private Button settings_back;

    // Use this for initialization
    void Start()
    {
        main_play.onClick.AddListener(() =>
        {
			loading.SetActive(true);
            SceneManager.LoadScene(1);
        });
        main_settings.onClick.AddListener(() =>
        {
            SwitchMenu("Settings");
        });
        settings_back.onClick.AddListener(() =>
        {
            SwitchMenu("Main");
        });
    }

    void SwitchMenu(string name)
    {
        bool exist = false;
        foreach(GameObject g in menus)
        {
            if (g.transform.name == name)
                exist = true;
        }
        if (exist)
        {
            foreach(GameObject g in menus)
            {
                if (g.transform.name == name)
                    g.SetActive(true);
                else
                    g.SetActive(false);
            }
        }
    }


}
