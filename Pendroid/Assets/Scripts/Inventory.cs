using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    [SerializeField] private Player player;
	[SerializeField] private GameObject itemIcon;
	[SerializeField] private GameObject itemIcon_usable;
	[SerializeField] private ItemIcon[] icons;
	[SerializeField] private GameObject hud;
	private static List<Item> items = new List<Item> ();
	private static Player _player;
	private static ItemIcon[] _icons;
	private static GameObject _itemIcon;
	private static GameObject _itemIcon_usable;
	private static GameObject _hud;

    void Start () {
		//
		//		Azt a sok static változót
		//
		_player = player;
		_icons = icons;
		_itemIcon = itemIcon;
		_itemIcon_usable = itemIcon_usable;
		_hud = hud;
	}
	
	//
	//		Item ikonok dinamikussá tétele
	//
	static void UpdateHUD() {
		GameObject[] old = GameObject.FindGameObjectsWithTag ("InventoryIcon");
		foreach (GameObject g in old) {
			Destroy (g);
		}
		int j = 0;
		int k = 0;
		bool unusableExist = false;
		foreach (Item i in items) {
			if (i.type != ItemType.potion)
				unusableExist = true;
		}
		foreach (Item i in items) {
			GameObject tmp;
			if (i.type == ItemType.potion) {
				tmp = Instantiate (_itemIcon_usable, Vector2.zero, Quaternion.identity, _hud.transform);
				if (unusableExist)
					tmp.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (96, -32 - k * 64);
				else
					tmp.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (32, -32 - k * 64);
				tmp.gameObject.GetComponent<Button> ().onClick.AddListener (() => {
					if (GetItemAmount(ItemType.potion) > 0) {
						_player.Heal (4);
						RemoveItem(ItemType.potion,1);
					}
				});
				k++;
			} else {
				tmp = Instantiate (_itemIcon, Vector2.zero, Quaternion.identity, _hud.transform);
				tmp.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (32, -32 - j * 64);
				j++;
			}
			tmp.GetComponentInChildren<Text> ().text = i.amount + "x";
			if (GetItemImage (i.type).anim != null) {
				tmp.GetComponentInChildren<Animator> ().runtimeAnimatorController = GetItemImage (i.type).anim;
			} else {
				tmp.transform.FindChild("Image").GetComponent<Image> ().sprite = GetItemImage (i.type).icon;
			}

		}
	}

	//
	//		Itemek kezelése
	//
	public static Item GetItem(ItemType t) {
		foreach (Item i in items) {
			if (i.type == t)
				return i;
		}
		return (Item)null;
	}
	public static void AddItem(ItemType t, int j) {
		if (GetItem (t) == null) {
			Item i = new Item();
			i.amount = j;
			i.type = t;
			items.Add (i);
		}
		else {
			GetItem (t).amount += j;
		}
		UpdateHUD ();
	}
	public static void RemoveItem(ItemType t, int j) {
		Item i = GetItem (t);
		if (i != null) {
			i.amount -= j;
			if (i.amount <= 0) {
				items.Remove (i);
			}
		}
		UpdateHUD ();
	}
	public static int GetItemAmount(ItemType t) {
		Item i = GetItem (t);
		if (i != null) {
			return i.amount;
		} else {
			return 0;
		}
	}

	static ItemIcon GetItemImage(ItemType t) {
		foreach (ItemIcon i in _icons) {
			if (i.type == t)
				return i;
		}
		return (ItemIcon)null;
	}
}

public enum ItemType
{
	potion,
	goldkey,
	key
}

[System.Serializable]
public class Item {
	public ItemType type;
	public int amount;
}
[System.Serializable]
public class ItemIcon {
	public ItemType type;
	public Sprite icon;
	public RuntimeAnimatorController anim;
}