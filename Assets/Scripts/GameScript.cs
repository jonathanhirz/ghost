using UnityEngine;

public class GameScript : MonoBehaviour {

	GameObject ghost;

	// TODO: add a 'Human spawner' to create enemies around the map
	void Awake () {
		
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha0)) {
			// ghost = GameObject.FindGameObjectWithTag("Player");
			// this needs to be this way because the above code can't find an object that is deactivated. Need to .Find() the object by its parent
			ghost = transform.Find("ghost").gameObject;
			ghost.SetActive(true);
			ghost.GetComponent<GhostMovement>().isAlive = true;
		}
	}
}
