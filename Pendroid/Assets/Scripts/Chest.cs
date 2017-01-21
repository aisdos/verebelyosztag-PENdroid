using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    [SerializeField] private Animator baseAnim;
	[SerializeField] private Animator itemAnim;
	[SerializeField] private Animator doorAnim;
	[SerializeField] private bool key;

	public void Open()
    {
        if (key == true)
        {
            if (Inventory.GetKey() > 0)
            {
				baseAnim.SetBool("open", true);
				doorAnim.SetBool("open", true);
				itemAnim.SetInteger ("item", 1);
                Inventory.AddPotion(1);
                Inventory.RemoveKey(1);
            }
        }
        else
        {
			baseAnim.SetBool("open", true);
			doorAnim.SetBool("open", true);
			itemAnim.SetInteger ("item", 1);
            Inventory.AddPotion(1);
        }
    }
}

public enum ChestType
{
    hp,
    mana
}
