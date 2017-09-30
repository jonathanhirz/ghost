using UnityEngine;

public class SmallGhost : MonoBehaviour {

	public bool isFollowing = false;
	// public float followSharpness = 0.05f;

	float followSharpness;

	GameObject bigGhost;

	void Awake () {
		bigGhost = GameObject.FindGameObjectWithTag("Player");
		followSharpness = Random.Range(0.02f, 0.08f);
	}
	
	void Update () {
		if(isFollowing) {
			// transform.position = new Vector2(bigGhost.transform.position.x - 5, bigGhost.transform.position.y);
		}
	}

	void LateUpdate() {
		if(isFollowing) {
			transform.position += (bigGhost.transform.position - transform.position) * followSharpness;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// Debug.Log(coll);
	}
}
