using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public float destroyDelay = 10.0f;

	void Update () {
		Destroy(gameObject, destroyDelay);	
	}
}
