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
		float fOrigin = Screen.currentResolution.width / 2;
		fOrigin /= 100;
		
		switch(i) {
		case 0:
			
			pos = new Vector3 (0, fOrigin, 0);
			break;
			
		case 1:
			pos = new Vector3 (fOrigin, 0, 0);
			break;
			
		case 2:
			pos = new Vector3 (0, -fOrigin, 0);
			break;
			
		case 3:
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
		float fOrigin = Screen.currentResolution.width;
		float fDelta = fOrigin / 7;
		fDelta = fDelta / 100;
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
