using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	[SerializeField] private ItemType type;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
			if (type == ItemType.key) 
				Inventory.AddItem(ItemType.key, 1);
			else if (type == ItemType.goldkey) 
				Inventory.AddItem(ItemType.goldkey, 1);
			else if (type == ItemType.potion) 
				Inventory.AddItem(ItemType.potion, 1);
        }
    }
}
