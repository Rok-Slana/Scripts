using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public float destroyDelay = 10.0f;

	void Update () {
		Destroy(gameObject, destroyDelay);	
	}
}
