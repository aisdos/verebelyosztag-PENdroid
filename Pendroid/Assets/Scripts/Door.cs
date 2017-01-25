using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	[SerializeField] private Animator anim;
	[SerializeField] private bool key;
	[SerializeField] private bool gold;
	[SerializeField] private bool open = false;
	[SerializeField] private Collider2D col;

	public void Open() {
		if (!open) {
			if (key) {
				if (!gold) {
					if (Inventory.GetItemAmount(ItemType.key) > 0) {
						_Open ();
					}
				} else {
					if (Inventory.GetItemAmount(ItemType.goldkey) > 0) {
						_Open ();
					}
				}
			} else {
				_Open ();
			}
		}
	}

	void _Open() {
		anim.SetBool("open", true);
		if (key) {
			if (gold)
				Inventory.RemoveItem (ItemType.goldkey,1);
			else
				Inventory.RemoveItem (ItemType.key, 1);
		}
		col.enabled = false;
		open = true;
	}
}
