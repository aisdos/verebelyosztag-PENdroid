using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour {

	private Rigidbody2D rig;
	private Rigidbody2D player;
	private bool flipCheck = false;
	private Vector2 lastMove = Vector2.zero;
	public MobType type;
	[SerializeField] private GameObject spriteTrans;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
		StartCoroutine (FirstMove ());
	}

	void Update() {
		spriteTrans.transform.position = Vector2.MoveTowards (spriteTrans.transform.position, gameObject.transform.position, Time.deltaTime * 4);
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
		if (lastMove == Vector2.left)
			move = Vector2.right;
		else
			move = Vector2.left;

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
		if (lastMove == Vector2.up)
			move = Vector2.down;
		else
			move = Vector2.up;

		lastMove = move;
		rig.MovePosition (rig.position + move);

		yield return new WaitForSeconds (Rythm.BeatDelay);
		StartCoroutine (ForBack ());
	}

	//
	//		Követés
	//
	IEnumerator Follow() {
		Vector2 move = Vector2.zero;
		if (lastMove == Vector2.up || lastMove == Vector2.down && rig.position.x != player.position.x)
			flipCheck = true;
		else
			flipCheck = false;
		if (!flipCheck) {
			if (rig.position.x > player.position.x)
				move = Vector2.left;
			else if (rig.position.x < player.position.x)
				move = Vector2.right;
			else {
				if (rig.position.y > player.position.y)
					move = Vector2.down;
				else
					move = Vector2.up;
			}
		} else {
			if (rig.position.y > player.position.y)
				move = Vector2.down;
			else if (rig.position.y < player.position.y)
				move = Vector2.up;
			else {
				if (rig.position.x > player.position.x)
					move = Vector2.left;
				else
					move = Vector2.right;
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
