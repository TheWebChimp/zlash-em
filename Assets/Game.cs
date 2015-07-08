using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	EnemyStack[] arrEnemies;
	int nPlayerStatus;
	int nPlayerCoins;
	int nPlayerKills;
	int nComboCounter;
	int nSwipeX;
	int nSwipeY;
	bool bIsDeath;
	float fTickTime;
	public Text text;
	//int nEffect <-- Array

	// Use this for initialization
	void Start() {
		bIsDeath = false;
		fTickTime = 1.0f;
		nSwipeX  = 0;
		nSwipeY  = 0;
		arrEnemies = new EnemyStack[4];
		for (int i = 0; i < 4; i++) {
			arrEnemies[i] = new EnemyStack();
		}
		InitEnemies();
		Tick();
		// Create player sprite
		Instantiate(Resources.Load("Prefabs/Player"));
	}

	// Update is called once per frame
	void Update() {
	}

	public bool HandleSwipe(string direction) {
		int nRet = -1;
		switch (direction) {
		case "up":
			nRet = (int)arrEnemies[0].queue.Peek() == 1 ? 0 : -1;
			nSwipeX = 1;
			break;
		case "right":
			nRet = (int)arrEnemies[1].queue.Peek() == 1 ? 1 : -1;
			nSwipeY = 1;
			break;
		case "down":
			nRet = (int)arrEnemies[2].queue.Peek() == 1 ? 2 : -1;
			nSwipeX = -1;
			break;
		case "left":
			nRet = (int)arrEnemies[3].queue.Peek() == 1 ? 3 : -1;
			nSwipeY = -1;
			break;
		}
		return (nSwipeX != 0 || nSwipeY != 0);
	}

	bool IsSwipeKill(int nSpawn) {
		bool bRet = false;
		switch (nSpawn) {
			case 0: // Top
				bRet = nSwipeX == 1;
				break;
			case 1: // Right
				bRet = nSwipeY == 1;
				break;
			case 2: // Bottom
				bRet = nSwipeX == -1;
				break;
			case 3: // Left
				bRet = nSwipeY == -1;
				break;
		}
		return bRet;
	}

	void Tick() {
		int[] nEnemies;
		RandomizeEnemies();
		text.text = "";
		for (int i = 0; i < 4; i++) {
			object[] queue = arrEnemies[i].queue.ToArray();
			if ( (int)queue[3] == 1 ) {
				Enemy enemy = new Enemy();
				arrEnemies[i].instances.Enqueue(enemy);
				enemy.Spawn(i);
			}
			//
			object[] instances = arrEnemies[i].instances.ToArray();
			for (int j = 0; j < instances.Length; j++) {
				Enemy enemy = (Enemy)instances[j];
				enemy.Move();
			}
		}
		// Now get the four enemies
		nEnemies = AdvanceEnemies();
		for (int i = 0; i < 4; i++) {
			//
			if (nEnemies[i] == 1 && IsSwipeKill(i)) {
				// Correct swipe! Killed the enemy
				text.text = ":) Enemy killed";
				fTickTime -= fTickTime > 0.25f ? 0.05f : 0;
			} else if (nEnemies[i] == 1 && !IsSwipeKill(i)) {
				text.text = "You die!!";
				bIsDeath = true;
			}
			//
			if (nEnemies[i] == 1) {
				// Enemy poped up
				Enemy enemy = (Enemy) arrEnemies[i].instances.Dequeue();
				enemy.Kill();
			}
		}
		// Consume swipes
		nSwipeX = 0;
		nSwipeY = 0;
		//
		Debug.Log("-------------------");
		for (int i = 0; i < 4; i++) {
			object[] queue = arrEnemies[i].queue.ToArray();
			Debug.Log( ((int)queue[0]).ToString() + " " + ((int)queue[1]).ToString() + " " + ((int)queue[2]).ToString() );
		}
		// 10/10 would invoke again
		if (! bIsDeath ) {
			Invoke("Tick", fTickTime);
		}
	}

	void InitEnemies() {
		for (int i = 0; i < 4; i++) {
			arrEnemies[i].queue.Enqueue(0);
			arrEnemies[i].queue.Enqueue(0);
			arrEnemies[i].queue.Enqueue(0);
		}
	}

	int[] AdvanceEnemies() {
		int[] nRet = new int[4];
		for (int i = 0; i < 4; i++) {
			nRet[i] = (int) arrEnemies[i].queue.Dequeue();
		}
		return nRet;
	}

	void RandomizeEnemies() {
		int nAddTo = Random.Range(0, 4);
		// Add a new enemy in ONE of the queues
		for (int i = 0; i < 4; i++) {
			arrEnemies[i].queue.Enqueue(i == nAddTo ? 1 : 0);
		}
	}
}
