using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    //r2
    /*
	public int hitsAlowed = 3;
	public bool isTheRightTarget = false;
	public GameObject destroyedTarget;
	public GameManager gameManager;
	
	private int hits = 0;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public bool IsTarget(){
		if(isTheRightTarget){
			return true;
		}else{
			return false;
		}
	}
	
	void OnCollisionEnter(Collision collision){
		hits++;
		if(hits >= hitsAlowed && isTheRightTarget){
			GameObject dtClone = Instantiate(destroyedTarget,this.transform.position, this.transform.rotation);
			//targetCounter.addTargetsDestroyedCount();
			Destroy(dtClone, 5.0f);
			//Call GameManager, instantiate next target
			gameManager.SpawnNewTarget();
			Destroy(gameObject);
		}
	}
	*/

    public int hitsAlowed = 3;
    public bool isTheRightTarget = false;
    public GameObject destroyedTarget;
 //   public GameManager gameManager;

    private int hits = 0;
/*
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
*/
    public bool IsTarget()
    {
        if (isTheRightTarget)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        hits++;
        if (hits >= hitsAlowed && isTheRightTarget)
        {
            GameObject dtClone = Instantiate(destroyedTarget, this.transform.position, this.transform.rotation);
            //targetCounter.addTargetsDestroyedCount();
            Destroy(dtClone, 5.0f);
            //Call GameManager, instantiate next target
  //          gameManager.SpawnNewTarget();
            Destroy(gameObject);
        }
    }



}
