using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {

    public float timeMultiplier;
    public float minTime = 2.5f;
    public float maxTime = 5.0f;    

    public Target2P targetObject2P;

    //private bool targetSpawned = false;

    private float time = 0;

    private void Start()
    {
        SetRandomTime();
    }

    void Update () {
        time -= Time.deltaTime;
        if (time <= 0){
            SetRandomTime();
            SpawnNewTarget();
        }


    }

    private void SetRandomTime() {
        time = Random.Range(minTime, maxTime);        
    }

    private void SpawnNewTarget() {
        Target2P targetClone = Instantiate(targetObject2P, transform.position, targetObject2P.transform.rotation);
    }

    //HAS TO SPAWN TARGETS
}
