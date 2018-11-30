using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	[Tooltip("Insert rocket here")]	
	public Rocket rocket;

	[Tooltip("Insert camera here")]
	public Camera camera;

	[Tooltip("Time between two rocket launches")]
	public float time = 1.0f;
	
	private int numOfRockets;
	
	//private Vector3 playerPos;
	
	private bool rocketActive = false;
	
	private float timer;
	
	
	void Start(){
		//playerPos = this.transform.position;
	}
	
	void Update () {		
		
		//Launch a rocket if rocket is not active
		if(Input.GetMouseButtonDown(0) && !rocketActive){		
			Vector3 targetPos = MousePos();			
			Rocket rocketClone = Instantiate(rocket, this.transform.position, Quaternion.identity);
            //Debug.Log(rocketClone);
			rocketClone.LaunchRocket(targetPos);			
		}
		
		//Subtratct time from timer if rocket is active
		if(rocketActive){
			timer-=Time.deltaTime;
			//Debug.Log(timer);
			if(timer<=0.0f){
				rocketActive = false;
				timer = time;
			}
		}		
	}
	
	//Return position of the mouse click/player tap on screen
	public Vector3 MousePos(){
		Vector3 mousePos = Input.mousePosition;
		Vector3 mousePosToWorld = camera.ScreenToWorldPoint(mousePos);		
		mousePosToWorld.z = 0.0f;
		return mousePosToWorld;
	}
	
	//Set rocketActive
	public void SetRocketActive(bool state){
		rocketActive = state;
	}
	
}
