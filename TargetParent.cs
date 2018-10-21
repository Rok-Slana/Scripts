using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParent : MonoBehaviour {

    public bool activeParent = false;//This is used by GameManager for locating the position of the active target
	public float speed = 50.0f;
    public Vector3 turningPoint;// = new Vector3(0.0f,50.0f,0.0f);

    private void Start()
    {
        turningPoint = GameObject.FindGameObjectWithTag("wheel").GetComponent<Transform>().position;
    }

    void Update(){
		transform.RotateAround(turningPoint, Vector3.forward, speed *  Time.deltaTime);
		transform.rotation = Quaternion.identity;
	}	
}
