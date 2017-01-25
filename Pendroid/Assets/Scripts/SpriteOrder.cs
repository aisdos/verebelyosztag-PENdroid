using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour {

	private Rigidbody2D player;
	private Rigidbody2D rig;
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private bool door;

	void Start() {
		rig = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (rig.position.y > player.position.y)
			sprite.sortingOrder = 0;
		else if (rig.position.y < player.position.y)
			sprite.sortingOrder = 2;
		else if (!door)
			sprite.sortingOrder = 1;
	}
}
