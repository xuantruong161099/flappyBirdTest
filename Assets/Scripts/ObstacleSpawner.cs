using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	[Range(1,3)] 
	[SerializeField] private int difficulty = 1;
	[SerializeField] private float waitTime;
	[SerializeField] private GameObject[] obstaclePrefabs;
	int obstacleID;
	int count = 0;
	private float tempTime;

	void Start(){
		tempTime = waitTime - Time.deltaTime;
		obstacleID = Random.Range(0, obstaclePrefabs.Length - 1);
	}

	void LateUpdate () {
		if(GameManager.Instance.GameState()){
			tempTime += Time.deltaTime;
			if(tempTime > waitTime){
				// Wait for some time, create an obstacle, then set wait time to 0 and start again
				tempTime = 0;
				GameObject pipeClone = Instantiate(obstaclePrefabs[obstacleID], transform.position, transform.rotation);
				
				if ((obstacleID + difficulty) > obstaclePrefabs.Length - 1)
					 obstacleID = obstacleID + difficulty - obstaclePrefabs.Length; 
				else
					obstacleID += difficulty;

				// ví dụ về tăng độ khó cho game
				if (count > 3) difficulty = 2;
				else count++; 
                
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.transform.parent != null){
			Destroy(col.gameObject.transform.parent.gameObject);
		}else{
			Destroy(col.gameObject);
		}
	}

}
