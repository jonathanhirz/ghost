using UnityEngine;
using System.Collections;

public class SmallGhost : MonoBehaviour {

	public bool isFollowing = false;
	// public float followSharpness = 0.05f;

	float followSharpness;
	Collider2D coll2d;
	GameObject bigGhost;
	GameObject portal;

	void Awake () {
		followSharpness = Random.Range(0.02f, 0.05f);
		coll2d = GetComponent<Collider2D>();
		bigGhost = GameObject.FindGameObjectWithTag("Player");
		portal = GameObject.FindGameObjectWithTag("portal");
	}

	void LateUpdate() {
		if(isFollowing) {
			transform.position += (bigGhost.transform.position - transform.position) * followSharpness;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.tag == "portal" && isFollowing) {
			var control = GameObject.FindGameObjectWithTag("control");
			control.GetComponent<GameScript>().score++;
			isFollowing = false;
			coll2d.enabled = false;
			DissolveGhost();
		}
	}

	void DissolveGhost() {
		// move toward center of portal
		StartCoroutine(MoveToPosition(transform, portal.transform.position, 1.0f));
		// spin around
		// shrink sprite
	}

	public IEnumerator MoveToPosition(Transform transform, Vector2 position, float timeToMove) {
		var currentPos = transform.position;
		var t = 0f;
		while(t < 1) {
			t += Time.deltaTime / timeToMove;
			transform.position = Vector2.Lerp(currentPos, position, t);
			transform.Rotate(0, 0, t * Random.Range(10, 15));
			StartCoroutine(WaitThenDestroy());
			yield return null;
		}
	}

	IEnumerator WaitThenDestroy() {
		yield return new WaitForSeconds(1.0f);
		Destroy(gameObject);
	}
}
