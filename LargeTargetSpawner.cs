using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeTargetSpawner : MonoBehaviour {
    #region
    public LetterLine line;

    private float time;
    private float timeToNextSpawn;

    private static int lineAndTargetID;

    private bool letterSpawnerHalt = false;

    private GameManager gameManager;
    #endregion

    //get timeToNextSpawn from gameManager and set time
    public void Start()
    {
        lineAndTargetID = 0;
        gameManager = FindObjectOfType<GameManager>();
        timeToNextSpawn = gameManager.SetTargetSpawnRate();
        time = timeToNextSpawn;
    }

    //Check time and instantiate new letter line every timeToNextSpawn
    public void Update()
    {
        if (!letterSpawnerHalt)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = timeToNextSpawn;
                SpawnNewLine();
            }
        } 
    }

    private void SpawnNewLine()
    {
        LetterLine letterLineClone= Instantiate(line, transform.position, line.transform.rotation);
        letterLineClone.SetLineAndTargetID(lineAndTargetID);
        lineAndTargetID++;
    }

    public void HaltLetterSpawner(bool state)
    {
        letterSpawnerHalt = state;
    }
}
