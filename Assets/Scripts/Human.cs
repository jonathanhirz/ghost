using UnityEngine;

public class Human : MonoBehaviour {

	public GameObject small_ghost;

	GameObject ghost;
	Rigidbody2D rb2d;
	Collider2D coll2d;
	Animator animator;
	bool isAlive;
	// bool isZombie;
	float maxSpeed = 5.0f;

	// basic AI: Walk towards ghost, don't hit walls
	// TODO: more advanced AI: attack ghost
	void Awake () {
		ghost = GameObject.FindGameObjectWithTag("Player");
		rb2d = GetComponent<Rigidbody2D>();
		coll2d = GetComponent<Collider2D>();
		animator = GetComponent<Animator>();
		isAlive = true;
		// isZombie = false;
	}
	
	void Update () {
		if(ghost != null && isAlive) {
			Vector2 direction = ghost.transform.position - transform.position;
			rb2d.AddForce(direction / 2);
			rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "bubble") {
			if(isAlive) {
				isAlive = false;
				rb2d.velocity = Vector2.zero;
				rb2d.isKinematic = true;
				coll2d.enabled = false;
				animator.Play("human_dead");
				Instantiate(small_ghost, new Vector2(transform.position.x, transform.position.y + 0.5f), transform.rotation);
				var control = GameObject.FindGameObjectWithTag("control");
				control.GetComponent<GameScript>().numberOfHumans--;
			}
		}
	}
}