using UnityEngine;
using System.Collections;

public class EnemyStack : MonoBehaviour {

	public Queue queue;
	public Queue instances;

	public EnemyStack() {
		queue = new Queue();
		instances = new Queue();
	}

}
