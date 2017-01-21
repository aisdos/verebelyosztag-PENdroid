using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    [SerializeField] private Animator anim;
    [SerializeField] private bool key;

	public void Open()
    {
        if (key == true)
        {
            if (Inventory.GetKey() > 0)
            {
                anim.SetBool("open", true);
                Inventory.AddPotion(1);
                Inventory.RemoveKey(1);
            }
        }
        else
        {
            anim.SetBool("open", true);
            Inventory.AddPotion(1);
        }
    }
}

public enum ChestType
{
    hp,
    mana
}
