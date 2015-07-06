using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	EnemyStack[] arrEnemies;
	int nPlayerStatus;
	int nPlayerCoins;
	int nPlayerKills;
	int nComboCounter;
	int nEnemySwipe;
	float fTickTime;
	public Text text;
	//int nEffect <-- Array

	// Use this for initialization
	void Start() {
		fTickTime = 1.0f;
		nEnemySwipe  = -1;
		arrEnemies = new EnemyStack[4];
		for (int i = 0; i < 4; i++) {
			arrEnemies[i] = new EnemyStack();
		}
		InitEnemies();
		InvokeRepeating("Tick", 0.0f, fTickTime);
	}
	
	// Update is called once per frame
	void Update() {
	}

	public bool HandleSwipe(string direction) {
		int nRet = -1;
		switch (direction) {
		case "up":
			nRet = (int)arrEnemies[0].stack.Peek() == 1 ? 0 : -1;
			break;
		case "right":
			nRet = (int)arrEnemies[1].stack.Peek() == 1 ? 1 : -1;
			break;
		case "down":
			nRet = (int)arrEnemies[2].stack.Peek() == 1 ? 2 : -1;
			break;
		case "left":
			nRet = (int)arrEnemies[3].stack.Peek() == 1 ? 3 : -1;
			break;
		}
		nEnemySwipe = nRet;
		return (nEnemySwipe >= 0); 
	}

	void Tick() {
		int[] nEnemies;
		RandomizeEnemies();
		// Now get the four enemies
		nEnemies = AdvanceEnemies();
		text.text = "Nutin'";
		for (int i = 0; i < 4; i++) {
			if (nEnemies[i] == 1 && i == nEnemySwipe) {
				// Correct swipe! Killed the enemy
				text.text = ":) Enemy killed";
			}
		}
		nEnemySwipe = -1;
		// 
		Debug.Log("-------------------");
		for (int i = 0; i < 4; i++) {
			object[] stack = arrEnemies[i].stack.ToArray();
			Debug.Log( ((int)stack[0]).ToString() + " " + ((int)stack[1]).ToString() + " " + ((int)stack[2]).ToString() );
		}

	}
	
	void InitEnemies() {
		for (int i = 0; i < 4; i++) {
			arrEnemies[i].stack.Enqueue(0);
			arrEnemies[i].stack.Enqueue(0);
			arrEnemies[i].stack.Enqueue(0);
		}
	}
	
	int[] AdvanceEnemies() {
		int[] nRet = new int[4];
		for (int i = 0; i < 4; i++) {
			nRet[i] = (int) arrEnemies[i].stack.Dequeue();
		}
		return nRet;
	}

	void RandomizeEnemies() {
		int nAddTo = Random.Range(0, 4);
		// Add a new enemy in ONE of the stacks
		for (int i = 0; i < 4; i++) {
			arrEnemies[i].stack.Enqueue(i == nAddTo ? 1 : 0);
			Debug.Log("bla");
		}
	}
}
