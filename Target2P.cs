using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2P : MonoBehaviour {

    public float velocity = 1.0f;
    public GameObject destroyedTarget;
    public bool isTheRightTarget = false;
       
    public bool IsTarget(){
        if (isTheRightTarget){return true;}
        else{return false;}
    }

    void Update (){
        transform.Translate(Vector3.down*Time.deltaTime * velocity, Space.World);		
	}

    void OnCollisionEnter(Collision collision){    
        GameObject dtClone = Instantiate(destroyedTarget, transform.position, transform.rotation);
        Destroy(dtClone, 5.0f);
        Destroy(gameObject);
    }
}
