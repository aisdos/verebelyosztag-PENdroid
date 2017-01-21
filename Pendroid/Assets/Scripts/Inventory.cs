using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private static int potions = 0;
    private static int key = 0;
    [SerializeField] private Player player;
    [SerializeField] private Button button_potion;
    [SerializeField] private Text text_potion;
    [SerializeField] private Text text_key;

    void Start () {
        button_potion.onClick.AddListener(() =>
        {
            if (potions > 0)
            {
                player.Heal(4);
                potions--;
            }
        });
	}
	
	void Update () {
        text_potion.text = potions + "x";
        text_key.text = key + "x";

        potions = Mathf.Clamp(potions, 0, 2);
        key = Mathf.Clamp(key, 0, 1);
    }

    public static void AddKey(int i)
    {
        key += i;
    }
    public static void RemoveKey(int i)
    {
        key -= i;
    }
    public static int GetKey()
    {
        return key;
    }
    public static void AddPotion(int i)
    {
        potions += i;
    }
}
