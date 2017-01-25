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
	[SerializeField] private GameObject health_0;
	[SerializeField] private GameObject health_1;
	[SerializeField] private GameObject health_2;
	[SerializeField] private GameObject healthBar;
	private RaycastHit2D hit_up;
	private RaycastHit2D hit_left;
	private RaycastHit2D hit_down;
	private RaycastHit2D hit_right;
	private float rythm = 0.5f;
	public int health;
	public int maxHealth = 6;

	void Start () {
		health = maxHealth;
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
		UpdateHealthBar ();
	}

	void Update() {
		//
		//	Mégha ez itt működne - Norbi
		//	Már működik - Norbi
		//
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName("idle_left") && !anim.GetCurrentAnimatorStateInfo (0).IsName("idle_down") && !anim.GetCurrentAnimatorStateInfo (0).IsName("idle_up"))
			anim.SetBool ("move", false);
		
		//
		//	Karakter képének mozgatása
		//
		spriteTrans.transform.position = Vector2.MoveTowards (spriteTrans.transform.position, gameObject.transform.position, Time.deltaTime * 4);

		//
		//	Életerő értékének intervallumának megadása
		//
		health = Mathf.Clamp (health, 0, maxHealth);
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
    //      Healelés
    //
    public void Heal(int hp)
    {
        health += hp;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();
    }

	//
	//		Sebződés
	//
	public void Damage(int dmg) {
		health -= dmg;
		if (health <= 0) {
			Die ();
		}
		UpdateHealthBar ();
	}

	//
	//		GameOver
	//
	public void Die() {
		UIManager.DeadUI ();
		sprite.SetActive (false);
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}

	//
	//		Életerőcsík Updateolása
	//		Ne is nézz erre még szépíteni kell
	//		- Norbi -
	//
	public void UpdateHealthBar() {
		GameObject[] oldIcons = GameObject.FindGameObjectsWithTag ("HealthIcon");
		foreach (GameObject g in oldIcons) {
			Destroy (g);
		}
		float tmpHealth = health;
		bool needHalf = false;
		int i = 0;
		int j = 0;
		if (health > 0) {
			if (health % 2 != 0) {
				tmpHealth++;
				needHalf = true;
			}
			for (j = 0; j < (maxHealth - tmpHealth) / 2; j++) {
				GameObject tmp = (GameObject)Instantiate (health_0, Vector2.zero, Quaternion.identity, healthBar.transform);
				tmp.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-24 - i * 48, -24);
				i++;
			}
			if (needHalf) {
				GameObject tmp = (GameObject)Instantiate (health_1, Vector2.zero, Quaternion.identity, healthBar.transform);
				tmp.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-24 - i * 48, -24);
				i++;
				tmpHealth -= 2;
			}
			for (j = 0; j < tmpHealth / 2; j++) {
				GameObject tmp = (GameObject)Instantiate (health_2, Vector2.zero, Quaternion.identity, healthBar.transform);
				tmp.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-24 - i * 48, -24);
				i++;
			}
		} else {
			for (j = 0; j < maxHealth / 2; j++) {
				GameObject tmp = (GameObject)Instantiate (health_0, Vector2.zero, Quaternion.identity, healthBar.transform);
				tmp.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-24 - i * 48, -24);
				i++;
			}
		}
	}

	//
	//		Mozgás
	//
	void Move(int dir) {
		if (health > 0) {
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
                        else
                            CheckHit(hit_left);
                    } else if (dir == 2) {
                        if (hit_up.collider == null)
                            move = Vector2.up;
                        else
                            CheckHit(hit_up);
					} else if (dir == 4) {
                        if (hit_down.collider == null)
                            move = Vector2.down;
                        else
                            CheckHit(hit_down);
                    }
				}
			} else {
				sprite.transform.rotation = Quaternion.Euler (0, 180, 0);
				anim.SetInteger ("dir", 1);
				anim.SetBool ("move", true);

                if (hit_right.collider == null)
                    move = Vector2.right;
                else
                    CheckHit(hit_right);
			}
			//}

			rig.MovePosition (rig.position + move);
		}
	}

    void CheckHit(RaycastHit2D hit)
    {
        if (hit.collider.GetComponent<EnemyBase>() != null)
            hit.collider.GetComponent<EnemyBase>().Damage(1);
        else if (hit.collider.GetComponent<Chest>() != null)
            hit.collider.GetComponent<Chest>().Open();
		else if (hit.collider.GetComponent<Door>() != null)
			hit.collider.GetComponent<Door>().Open();
    }
}
