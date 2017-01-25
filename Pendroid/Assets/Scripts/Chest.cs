using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    [SerializeField] private Animator baseAnim;
	[SerializeField] private Animator itemAnim;
	[SerializeField] private Animator doorAnim;
	[SerializeField] private bool key;
	[SerializeField] private bool gold;
	[SerializeField] private bool open = false;
	[SerializeField] private ItemType loot;


	//
	//		Van mit szépíteni - Norbi
	//
	public void Open()
    {
		if (!open) {
			if (key) {
				if (!gold) {
					if (Inventory.GetItemAmount(ItemType.key) > 0) {
						Loot ();
					}
				} else {
					if (Inventory.GetItemAmount(ItemType.goldkey) > 0) {
						Loot ();
					}
				}
			} else {
				Loot ();
			}
		}
    }

	void Loot() {
		baseAnim.SetBool("open", true);
		doorAnim.SetBool("open", true);
		if (loot == ItemType.potion) {
			itemAnim.SetInteger ("item", 1);
			Inventory.AddItem (ItemType.potion, 1);
		} else if (loot == ItemType.goldkey) {
			if (!gold) {
				itemAnim.SetInteger ("item", 2);
				Inventory.AddItem (ItemType.goldkey, 1);
			} else
				Debug.LogWarning ("Gold láda gold kulcsot dob? Komolyan?!");
		} else {
			Debug.LogWarning ("Ezt chestbe? Vagy csak lehet nincs lekódolva?");
		}
		if (key) {
			if (gold)
				Inventory.RemoveItem (ItemType.goldkey,1);
			else
				Inventory.RemoveItem (ItemType.key, 1);
		}
		open = true;
	}
}


