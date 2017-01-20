using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

	private Rigidbody2D rig;
	[SerializeField] private Animator anim;					//	[SerializeField] = Szerkesztőben láthatóvá teszi a privát változót
	[SerializeField] private GameObject spriteTrans;
	[SerializeField] private GameObject sprite;
	[SerializeField] private Button dpad_up;
	[SerializeField] private Button dpad_down;
	[SerializeField] private Button dpad_left;
	[SerializeField] private Button dpad_right;
	private RaycastHit2D hit_up;
	private RaycastHit2D hit_left;
	private RaycastHit2D hit_down;
	private RaycastHit2D hit_right;
	private float rythm = 0.5f;

	void Start () {
		rig = GetComponent<Rigidbody2D> ();
		dpad_up.onClick.AddListener (() => {
			Move(2);
		});
		dpad_down.onClick.AddListener (() => {
			Move(4);
		});
		dpad_left.onClick.AddListener (() => {
			Move(1);
		});
		dpad_right.onClick.AddListener (() => {
			Move(3);
		});
	}

	void Update() {
		//Mégha ez itt működne - Norbi
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName("idle_left") && !anim.GetCurrentAnimatorStateInfo (0).IsName("idle_down") && !anim.GetCurrentAnimatorStateInfo (0).IsName("idle_up"))
			anim.SetBool ("move", false);
		
		//
		//	Karakter képének mozgatása
		//
		spriteTrans.transform.position = Vector2.MoveTowards (spriteTrans.transform.position, gameObject.transform.position, Time.deltaTime * 4);
	}

	//
	//		Collision Kezelés mivel a beépített verziót nem tudtam hasznosítani a Grid alapú mozgás miatt
	//		Később talán lesz egy kevésbé macerás megoldásom
	//		- Norbi -
	//
	void FixedUpdate() {
		hit_up = Physics2D.Raycast (rig.position + (new Vector2(0, 0.51f)), Vector2.up, 0.25f);
		hit_left = Physics2D.Raycast (rig.position + (new Vector2(-0.51f, 0)), Vector2.left,0.25f);
		hit_right = Physics2D.Raycast (rig.position + (new Vector2(0.51f, 0)), Vector2.right,0.25f);
		hit_down = Physics2D.Raycast (rig.position + (new Vector2(0, -0.51f)), Vector2.down,0.25f);

		Debug.DrawRay (rig.position + (new Vector2(0, 0.51f)), Vector2.up/4, Color.red);
		Debug.DrawRay (rig.position + (new Vector2(-0.51f, 0)), Vector2.left/4, Color.blue);
		Debug.DrawRay (rig.position + (new Vector2(0.51f, 0)), Vector2.right/4, Color.green);
		Debug.DrawRay (rig.position + (new Vector2(0, -0.51f)), Vector2.down/4, Color.yellow);

		if (hit_up.collider != null)
			print ("Fel: " + hit_up.collider.name);
		if (hit_left.collider != null)
			print ("Balra: " + hit_left.collider.name);
		if (hit_right.collider != null)
			print ("Jobbra: " + hit_right.collider.name);
		if (hit_down.collider != null)
			print ("Le: " + hit_down.collider.name);
	}

	//
	//		Mozgás
	//
	void Move(int dir) {
		Vector2 move = Vector2.zero;
		//if (Rythm.NextBeat() < Rythm.BeatDelay*rythm) {
			if (dir != 3) {
				if (dir != 0) {
					sprite.transform.rotation = Quaternion.Euler (0, 0, 0);
					anim.SetInteger ("dir", dir);
					anim.SetBool ("move", true);

					if (dir == 1) {
						if (hit_left.collider == null)
							move = Vector2.left;
					} else if (dir == 2) {
						if (hit_up.collider == null)
							move = Vector2.up;
					} else if (dir == 4) {
						if (hit_down.collider == null)
							move = Vector2.down;
					}
				}
			} 
			else {
				sprite.transform.rotation = Quaternion.Euler (0, 180, 0);
				anim.SetInteger ("dir", 1);
				anim.SetBool ("move", true);

				if (hit_right.collider == null)
					move = Vector2.right;
			}
		//}

		rig.MovePosition (rig.position + move);
	}
}
