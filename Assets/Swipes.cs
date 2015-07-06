using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Swipes : MonoBehaviour
{
	public float minSwipeDistY;
	public float minSwipeDistX;
	private bool mouseDown;
	private Vector2 startPos;
	public Text text;

	Camera camera;

	void Start() {
		camera = GetComponent<Camera>();
		camera.clearFlags = CameraClearFlags.SolidColor;
	}

	void OnGUI() {
		foreach ( Touch touch in Input.touches) {
			string message = "";
			message += "ID: " + touch.fingerId + "\n";
			message += "Phase: : " + touch.phase.ToString() + "\n";
			message += "TapCount: " + touch.tapCount + "\n";
			message += "Pos X: " + touch.position.x + "\n";
			message += "Pos Y: " + touch.position.y + "\n";
			int num = touch.fingerId;
			GUI.Label(new Rect(0 + 130 * num, 0, 120, 100), message);
		}
	}

	void Update() {
		if (Input.touchCount > 0) {
			// Touch support ------------------------------------------------------------------------------------------
			Touch touch = Input.touches [0];
			switch (touch.phase) {
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			case TouchPhase.Ended:
				float swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
				float swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
				if (swipeDistVertical > minSwipeDistY) {
					float swipeValue = Mathf.Sign (touch.position.y - startPos.y);
					if (swipeValue > 0) { 
						// Arriba
						camera.backgroundColor = Color.red;
						gameObject.GetComponent<Game>().HandleSwipe("up");
					} else if (swipeValue < 0) { 
						// Abajo
						camera.backgroundColor = Color.blue;
						gameObject.GetComponent<Game>().HandleSwipe("down");
					}
				}
				if (swipeDistHorizontal > minSwipeDistX) {
					float swipeValue = Mathf.Sign (touch.position.x - startPos.x);
					if (swipeValue > 0) { 
						// Derecha
						camera.backgroundColor = Color.gray;
						gameObject.GetComponent<Game>().HandleSwipe("right");
					} else if (swipeValue < 0) { 
						// Izquierda
						camera.backgroundColor = Color.green;
						gameObject.GetComponent<Game>().HandleSwipe("left");
					}
				}
				break;
			}
		} else {
			// Mouse support ------------------------------------------------------------------------------------------
			if (!mouseDown && Input.GetMouseButtonDown(0)) {
				mouseDown= true;
				startPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			} else if (mouseDown && Input.GetMouseButtonUp(0)) {
				mouseDown = false;
				float swipeDistVertical = (new Vector3 (0, Input.mousePosition.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
				float swipeDistHorizontal = (new Vector3 (Input.mousePosition.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
				if (swipeDistVertical > minSwipeDistY) {
					float swipeValue = Mathf.Sign (Input.mousePosition.y - startPos.y);
					if (swipeValue > 0) { 
						// Arriba
						camera.backgroundColor = Color.red;
						gameObject.GetComponent<Game>().HandleSwipe("up");
					} else if (swipeValue < 0) { 
						// Abajo
						camera.backgroundColor = Color.blue;
						gameObject.GetComponent<Game>().HandleSwipe("down");
					}
				}
				if (swipeDistHorizontal > minSwipeDistX) {
					float swipeValue = Mathf.Sign (Input.mousePosition.x - startPos.x);
					if (swipeValue > 0) { 
						// Derecha
						camera.backgroundColor = Color.gray;
						gameObject.GetComponent<Game>().HandleSwipe("right");
					} else if (swipeValue < 0) { 
						// Izquierda
						camera.backgroundColor = Color.green;
						gameObject.GetComponent<Game>().HandleSwipe("left");
					}
				}
			}
		}
	}
}