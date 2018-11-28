using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2P : MonoBehaviour {

    public float velocity = 1.0f;
    public GameObject destroyedTarget;
    public bool isTheRightTarget = false;

    private int lineAndTargetID;
    private LetterLine letterLineParent;


    public void Start()
    {
        letterLineParent = gameObject.GetComponentInParent<LetterLine>();
    }

    public bool IsTarget(){
        if (isTheRightTarget){return true;}
        else{return false;}
    }

    void Update (){
        transform.Translate(Vector3.down*Time.deltaTime * velocity, Space.World);		
	}

    void OnCollisionEnter(Collision collision){    
        
        if (isTheRightTarget){

            GameObject dtClone = Instantiate(destroyedTarget, transform.position, transform.rotation);
            letterLineParent.DecrementActiveLetter();
            Destroy(dtClone, 5.0f);
            Destroy(gameObject);
        }
      
    }

    public void SetLineAndTargetID(int id){
        lineAndTargetID = id;
    }
}
