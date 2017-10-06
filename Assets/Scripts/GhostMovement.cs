using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GhostMovement : MonoBehaviour {

	public float movementSpeed;
	public float fireRate = 0.1f;
	public GameObject bubble;
	public float bubbleSpeed = 7;
	public bool isAlive = true;
	public GameObject[] arrayOfSmallGhosts;
	public Text gameOverText;

	Rigidbody2D rb2d;
	SpriteRenderer rend;
	Animator anim;
	Vector2 bubbleDirection;
	float lastShot;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	void Update() {
		if(rb2d.velocity.x > 0) {
			rend.flipX = false;
		}
		if(rb2d.velocity.x < 0) {
			rend.flipX = true;
		}

		// limit rate of fire
		if(Input.GetKey(KeyCode.UpArrow)) {
			bubbleDirection = new Vector2(0,1) + rb2d.velocity / 10;
			if(Time.time > lastShot) {
				FireBubble();
				lastShot = Time.time + fireRate;
			}
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			bubbleDirection = new Vector2(0,-1) + rb2d.velocity / 10;
			if(Time.time > lastShot) {
				FireBubble();
				lastShot = Time.time + fireRate;
			}
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			bubbleDirection = new Vector2(-1,0) + rb2d.velocity / 10;
			if(Time.time > lastShot) {
				FireBubble();
				lastShot = Time.time + fireRate;
			}
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			bubbleDirection = new Vector2(1,0) + rb2d.velocity / 10;
			if(Time.time > lastShot) {
				FireBubble();
				lastShot = Time.time + fireRate;
			}
		}
	}

	void FixedUpdate() {
		float move_h = Input.GetAxis("Horizontal");
		float move_v = Input.GetAxis("Vertical");
		rb2d.AddForce(new Vector2(move_h, move_v) * movementSpeed);
	}

	void FireBubble() {
		GameObject thisBubble = Instantiate(bubble, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		thisBubble.GetComponent<Rigidbody2D>().velocity = bubbleDirection * bubbleSpeed;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "enemy") {
			if(isAlive) {
				isAlive = false;
				anim.Play("ghost_death");
				rb2d.velocity = Vector2.zero;
				StartCoroutine(WaitThenDeactivate());
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "smallGhost") {
			var smallGhostScript = coll.gameObject.GetComponent<SmallGhost>();
			smallGhostScript.isFollowing = true;
		}
	}

	IEnumerator WaitThenDeactivate() {
		yield return new WaitForSeconds(0.2f);
		gameObject.SetActive(false);
		var finalScore = GameObject.FindGameObjectWithTag("control").GetComponent<GameScript>().score;
		gameOverText.GetComponent<Text>().text = "YOU DIED? \n YOU COLLECTED " + finalScore.ToString() + " SMALL GHOSTS \n PRESS 'R' TO TRY AGAIN";
		gameOverText.GetComponent<Text>().enabled = true;
		GameObject.FindGameObjectWithTag("control").GetComponent<GameScript>().playerDead = true;
	}

}
