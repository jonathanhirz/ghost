using UnityEngine;

public class Bubble : MonoBehaviour {

	Animator animator;
	Rigidbody2D rb2d;

	void Awake () {
		animator = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collision) {

		animator.Play("bubble_burst");
		rb2d.velocity = Vector2.zero;
		Destroy(gameObject, 0.2f);
		// 0.2f is hard coded length of the burst animation. find way to get this length from anim
	}

	// TODO: pool bubbles for better performance (later)
	void OnBecameInvisible() {
		// TODO: fix this so bubbles get destroyed when they leave the level area, not just the screen. This way you can hit enemies off camera
		Destroy(gameObject);
	}
}
