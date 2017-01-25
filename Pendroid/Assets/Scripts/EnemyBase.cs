using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour {

	public int health;
	public bool flipCheck = false;
	public MobType type;
	public int damage = 1;
	[SerializeField] private GameObject spriteTrans;
	[SerializeField] private GameObject drop;
	private Vector2 lastMove = Vector2.zero;
	private Rigidbody2D rig;
	private Rigidbody2D player;
	private RaycastHit2D hit_up;
	private RaycastHit2D hit_left;
	private RaycastHit2D hit_down;
	private RaycastHit2D hit_right;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
		StartCoroutine (FirstMove ());
	}

	void Update() {
		spriteTrans.transform.position = Vector2.MoveTowards (spriteTrans.transform.position, gameObject.transform.position, Time.deltaTime * 4);
	}

	void FixedUpdate() {
		hit_up = Physics2D.Raycast (rig.position + (new Vector2(0, 0.51f)), Vector2.up, 0.25f);
		hit_left = Physics2D.Raycast (rig.position + (new Vector2(-0.51f, 0)), Vector2.left,0.25f);
		hit_right = Physics2D.Raycast (rig.position + (new Vector2(0.51f, 0)), Vector2.right,0.25f);
		hit_down = Physics2D.Raycast (rig.position + (new Vector2(0, -0.51f)), Vector2.down,0.25f);

		Debug.DrawRay (rig.position + (new Vector2(0, 0.51f)), Vector2.up/4, Color.red);
		Debug.DrawRay (rig.position + (new Vector2(-0.51f, 0)), Vector2.left/4, Color.blue);
		Debug.DrawRay (rig.position + (new Vector2(0.51f, 0)), Vector2.right/4, Color.green);
		Debug.DrawRay (rig.position + (new Vector2(0, -0.51f)), Vector2.down/4, Color.yellow);
	}

	//
	//		Sebződés
	//
	public void Damage(int dmg) {
		health -= dmg;
		if (health <= 0) {
			if (drop != null) {
				Instantiate (drop, gameObject.transform.position, gameObject.transform.rotation);
			}
			Destroy (gameObject);
		}
	}

	IEnumerator FirstMove() {
		yield return new WaitForSeconds (Rythm.BeginDelay);
		if (type == MobType.Follow)
			StartCoroutine (Follow ());
		else if (type == MobType.ForBack)
			StartCoroutine (ForBack ());
		else if (type == MobType.LeftRight)
			StartCoroutine (LeftRight ());
	}

	//
	//		Pattogás jobbra-balra
	//
	IEnumerator LeftRight() {
		Vector2 move = Vector2.zero;
		if (!flipCheck) {
			if (lastMove == Vector2.zero)
				lastMove = Vector2.left;
		} else {
			if (lastMove == Vector2.zero)
				lastMove = Vector2.right;
		}
		if (lastMove == Vector2.left) {
			if (hit_right.collider == null)
				move = Vector2.right;
			else if (hit_right.collider.GetComponent<Player> () != null)
				hit_right.collider.GetComponent<Player> ().Damage (damage);
		} else {
			if (hit_left.collider == null)
				move = Vector2.left;
			else if (hit_left.collider.GetComponent<Player> () != null)
				hit_left.collider.GetComponent<Player> ().Damage (damage);
		}

		if (move != Vector2.zero)
			lastMove = move;
		rig.MovePosition (rig.position + move);

		yield return new WaitForSeconds (Rythm.BeatDelay);
		StartCoroutine (LeftRight ());
	}

	//
	//		Pattogás előre-hátra
	//
	IEnumerator ForBack() {
		Vector2 move = Vector2.zero;
		if (!flipCheck) {
			if (lastMove == Vector2.zero)
				lastMove = Vector2.up;
		} else {
			if (lastMove == Vector2.zero)
				lastMove = Vector2.down;
		}
		if (lastMove == Vector2.up) {
			if (hit_down.collider == null)
				move = Vector2.down;
			else if (hit_down.collider.GetComponent<Player> () != null)
				hit_down.collider.GetComponent<Player> ().Damage (damage);
		} else {
			if (hit_up.collider == null)
				move = Vector2.up;
			else if (hit_up.collider.GetComponent<Player> () != null)
				hit_up.collider.GetComponent<Player> ().Damage (damage);
		}

		if (move != Vector2.zero)
			lastMove = move;
		rig.MovePosition (rig.position + move);

		yield return new WaitForSeconds (Rythm.BeatDelay);
		StartCoroutine (ForBack ());
	}

	//
	//		Követés
	//		NE KÉRDEZD HOGY DE MŰKÖDIK!!! - Norbi
	//
	IEnumerator Follow() {
		Vector2 move = Vector2.zero;
		if (lastMove == Vector2.up || lastMove == Vector2.down && rig.position.x != player.position.x)
			flipCheck = true;
		else
			flipCheck = false;

		if (!flipCheck) {
			if (rig.position.x > player.position.x) {
				if (hit_left.collider == null)
					move = Vector2.left;
				else if (hit_left.collider.GetComponent<Player> () != null)
					hit_left.collider.GetComponent<Player> ().Damage (damage);
				else {
					if (rig.position.y > player.position.y) {
						if (hit_down.collider == null)
							move = Vector2.down;
						else if (hit_down.collider.GetComponent<Player> () != null)
							hit_down.collider.GetComponent<Player> ().Damage (damage);
					} else if (rig.position.y < player.position.y) {
						if (hit_up.collider == null)
							move = Vector2.up;
						else if (hit_up.collider.GetComponent<Player> () != null)
							hit_up.collider.GetComponent<Player> ().Damage (damage);
					}
				}
			} else if (rig.position.x < player.position.x) {
				if (hit_right.collider == null)
					move = Vector2.right;
				else if (hit_right.collider.GetComponent<Player> () != null)
					hit_right.collider.GetComponent<Player> ().Damage (damage);
				else {
					if (rig.position.y > player.position.y) {
						if (hit_down.collider == null)
							move = Vector2.down;
						else if (hit_down.collider.GetComponent<Player> () != null)
							hit_down.collider.GetComponent<Player> ().Damage (damage);
					} else if (rig.position.y < player.position.y) {
						if (hit_up.collider == null)
							move = Vector2.up;
						else if (hit_up.collider.GetComponent<Player> () != null)
							hit_up.collider.GetComponent<Player> ().Damage (damage);
					}
				}
			} else {
				if (rig.position.y < player.position.y) {
					if (hit_up.collider == null)
						move = Vector2.up;
					else if (hit_up.collider.GetComponent<Player> () != null)
						hit_up.collider.GetComponent<Player> ().Damage (damage);
				} else if (rig.position.y > player.position.y) {
					if (hit_down.collider == null)
						move = Vector2.down;
					else if (hit_down.collider.GetComponent<Player> () != null)
						hit_down.collider.GetComponent<Player> ().Damage (damage);
				}
			}
		} else {
			if (rig.position.y < player.position.y) {
				if (hit_up.collider == null)
					move = Vector2.up;
				else if (hit_up.collider.GetComponent<Player> () != null)
					hit_up.collider.GetComponent<Player> ().Damage (damage);
				else {
					if (rig.position.x < player.position.x) {
						if (hit_right.collider == null)
							move = Vector2.right;
						else if (hit_right.collider.GetComponent<Player> () != null)
							hit_right.collider.GetComponent<Player> ().Damage (damage);
					} else if (rig.position.x > player.position.x) {
						if (hit_left.collider == null)
							move = Vector2.left;
						else if (hit_left.collider.GetComponent<Player> () != null)
							hit_left.collider.GetComponent<Player> ().Damage (damage);
					}
				}
			} else if (rig.position.y > player.position.y) {
				if (hit_down.collider == null)
					move = Vector2.down;
				else if (hit_down.collider.GetComponent<Player> () != null)
					hit_down.collider.GetComponent<Player> ().Damage (damage);
				else {
					if (rig.position.x < player.position.x) {
						if (hit_right.collider == null)
							move = Vector2.right;
						else if (hit_right.collider.GetComponent<Player> () != null)
							hit_right.collider.GetComponent<Player> ().Damage (damage);
					} else if (rig.position.x > player.position.x) {
						if (hit_left.collider == null)
							move = Vector2.left;
						else if (hit_left.collider.GetComponent<Player> () != null)
							hit_left.collider.GetComponent<Player> ().Damage (damage);
					}
				}
			} else {
				if (rig.position.x < player.position.x) {
					if (hit_right.collider == null)
						move = Vector2.right;
					else if (hit_right.collider.GetComponent<Player> () != null)
						hit_right.collider.GetComponent<Player> ().Damage (damage);
				} else if (rig.position.x > player.position.x) {
					if (hit_left.collider == null)
						move = Vector2.left;
					else if (hit_left.collider.GetComponent<Player> () != null)
						hit_left.collider.GetComponent<Player> ().Damage (damage);
				}
			}
		}

		lastMove = move;
		rig.MovePosition (rig.position + move);

		yield return new WaitForSeconds (Rythm.BeatDelay);
		StartCoroutine (Follow ());
	}
}

public enum MobType {
	Follow,
	LeftRight,
	ForBack
}
