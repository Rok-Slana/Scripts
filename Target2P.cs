using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2P : MonoBehaviour {
    #region
    public float velocity = 1.0f;

    public GameObject destroyedTarget;

    public bool isTheRightTarget = false;

    private int lineAndTargetID;

    private LetterLine letterLineParent;

    private GameManager gameManager;
    #endregion

    //Get parent (LargeTargetSpawner) and gameManager, set downward velocity
    public void Start()
    {
        letterLineParent = gameObject.GetComponentInParent<LetterLine>();
        gameManager = FindObjectOfType<GameManager>();
        velocity = gameManager.SetTargetVelocity();
    }

    //Move down
    public void Update ()
    {
        transform.Translate(Vector3.down*Time.deltaTime * velocity, Space.World);		
	}

    //Check if the target is an 'active object'
    public bool IsTarget()
    {
        if (isTheRightTarget) return true;
        else return false;
    }

    //Handle collisions
    void OnCollisionEnter(Collision collision)
    {   
        if (isTheRightTarget)
        {
            GameObject dtClone = Instantiate(destroyedTarget, transform.position, transform.rotation);
            letterLineParent.DecrementActiveLetter();
            Destroy(dtClone, 5.0f);
            Destroy(gameObject);
        }
      
    }

    //Set IDs
    public void SetLineAndTargetID(int id)
    {
        lineAndTargetID = id;
    }

    //Set IF the target is right or not
    public void SetIsTheRightTarget(bool state)
    {
        isTheRightTarget = state;
    }
}
