using UnityEngine;
using System.Collections;

public class EnemyStack : MonoBehaviour {

	public Queue stack;

	public EnemyStack() {
		stack = new Queue();
	}

}
