using UnityEngine;
using System.Collections.Generic;

public class GameScript : MonoBehaviour {

	public GameObject human;
	public int numberOfHumans = 0;
	public int maxNumberOfHumans = 5;

	GameObject ghost;
	GameObject[] spawnPoints;
	GameObject enemyList;

	void Start() {
		InvokeRepeating("SpawnAHuman", 1.0f, 1.0f);
	}

	void Awake () {
		spawnPoints = GameObject.FindGameObjectsWithTag("spawn");
		enemyList = GameObject.FindGameObjectWithTag("enemies");
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

	void SpawnAHuman() {
		if(numberOfHumans < maxNumberOfHumans) {
			var randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
			Instantiate(human, randomSpawn.transform.position, randomSpawn.transform.rotation, enemyList.transform);
			numberOfHumans++;
		}
		
	}
}
