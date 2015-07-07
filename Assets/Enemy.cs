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
		float fOrigin = Screen.width / 2.0f;
		fOrigin /= Screen.height / 10.0f;
		float fDelta = Screen.width / 14.0f;
		fDelta /= Screen.height / 10.0f;
		
		switch(i) {
		case 0:
			fOrigin += fDelta;
			pos = new Vector3 (0, fOrigin, 0);
			break;
			
		case 1:
			
			fOrigin += fDelta;
			pos = new Vector3 (fOrigin, 0, 0);
			break;
			
		case 2:
			fOrigin += fDelta;
			pos = new Vector3 (0, -fOrigin, 0);
			break;
			
		case 3:
			fOrigin += fDelta;
			pos = new Vector3 (-fOrigin, 0, 0);
			break;
		}
		
		nSpawn = i;
		objBolita = (GameObject) Instantiate(Resources.Load("Prefabs/Bolita"));
		objBolita.transform.position = pos;
	}
	
	public void Kill() {
		Destroy(objBolita);
		Destroy(this);
	}
	
	public void Move() {
		Vector3 vPos;
		float fOrigin = Screen.width;
		float fDelta = fOrigin / 7.0f;
		fDelta /= Screen.height / 10.0f;
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
		objBolita.transform.Translate(vPos);
	}
}
