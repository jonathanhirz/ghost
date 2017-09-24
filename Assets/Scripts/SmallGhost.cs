using UnityEngine;

public class SmallGhost : MonoBehaviour {

	public bool isFollowing = false;
	// public float followSharpness = 0.05f;

	GameObject bigGhost;

	void Awake () {
		bigGhost = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
		if(isFollowing) {
			// transform.position = new Vector2(bigGhost.transform.position.x - 5, bigGhost.transform.position.y);
		}
	}

	void LateUpdate() {
		if(isFollowing) {
			var followSharpness = Random.Range(0.03f, 0.07f);
			transform.position += (bigGhost.transform.position - transform.position) * followSharpness;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// Debug.Log(coll);
	}
}
