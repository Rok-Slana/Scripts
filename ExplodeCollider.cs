using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCollider : MonoBehaviour {

	public ParticleSystem ps;
	
	private ParticleSystem psClone;
	private bool initialized = false;
	private float destroyDelay;
		
	void Update () {
		if(initialized){			
			GameObject.Destroy(gameObject, destroyDelay);
		}
	}
	
	void OnMouseDown(){
		ps.transform.position = this.transform.position;
		ps.Play();
		//ps.transform.position
		/*psClone = Instantiate(ps, transform.position, transform.rotation);
		psClone.Play();
		Debug.Log("PRE Boom");

		Destroy(psClone, 4.0f);
		Debug.Log("Boom");*/
	}
	
	public void InitializeCollider(float delay){
		destroyDelay = delay;
		initialized = true;
	}
}
