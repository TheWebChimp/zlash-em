using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	int nSpawn;
	GameObject objBolita;

	// Use this for initialization
	void Start () {
		nSpawn = -1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Spawn(int i) {
		Vector3 pos = new Vector3(0,0,0);
		
		switch(i) {
		case 0:
			
			pos = new Vector3 (0, 3, 0);
			break;
			
		case 1:
			pos = new Vector3 (6, 0, 0);
			break;
			
		case 2:
			pos = new Vector3 (0, -3, 0);
			break;
			
		case 3:
			pos = new Vector3 (-6, 0, 0);
			break;
		}

		nSpawn = i;
		objBolita = (GameObject) Instantiate(Resources.Load("Prefabs/Bolita"));
		objBolita.transform.position = pos;
	}
	
	public void Kill() {
		//
	}
	
	public void Move() {
		Vector3 vPos;
		float fDelta = 0.25f;
		switch(nSpawn) {
		case 0:
			vPos = new Vector3(0, -fDelta, 0);
			break;
			
		case 1:
			vPos = new Vector3(-fDelta, 0, 0);
			break;
			
		case 2:
			vPos = new Vector3(0, fDelta, 0);
			break;
			
		case 3:
			vPos = new Vector3(fDelta, 0, 0);
			break;
		default:
			vPos = new Vector3(0, 0, 0);
			break;
		}
		vPos.x += objBolita.transform.position.x;
		vPos.y += objBolita.transform.position.y;
		vPos.z += objBolita.transform.position.z;
		objBolita.transform.position = vPos;
	}
}
