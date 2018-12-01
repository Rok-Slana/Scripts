using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterLine : MonoBehaviour {
    #region
    private int numOfActiveLetters = 0;
    private int lineAndTargetID;
    private int countSpawnedActiveLetters = 0;

    private static int numberOfInstances;

    private GameManager gameManager;
   
    public Transform[] spawnerPos;

    private Target2P activeLetter;
    private Target2P[] inactiveLettersArray;

    private bool[] usedPositions = new bool[] { false, false, false, false, false };    // stores position values where a letter has already been spawned   
    #endregion

    private static int count = 0;

    //Gets and instantiates active and inactive letter objects from GameManager
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        activeLetter = gameManager.ReturnActiveLetter();
        activeLetter.SetIsTheRightTarget(true);
        inactiveLettersArray = gameManager.ReturnInactiveLetters();

        //Set number of 'active letters' in the current LetterLine instance (min = 1, max = 3)
        numOfActiveLetters = Random.Range(1, 4);
        
        numberOfInstances += numOfActiveLetters;
        gameManager.IncrementTargetsSpawned(numOfActiveLetters);

        if (numberOfInstances == 10)
        {
            numberOfInstances = 0;
            /////////////////////////////////////
            Debug.Log("Count : " + count);
            count++;
            /////////////////////////////////////
        }
        else if (numberOfInstances > 10) TryAgain();
        
         // Instantiate randomly letters accros letterLine so that there is a corresponding number of 'active' letters to the number above, others must be 'inactive'
        while (countSpawnedActiveLetters < numOfActiveLetters)
        {
            for (int x = 0; x < usedPositions.Length; x++)
            {
                // Returns random bool value
                bool randomBool = (Random.value > 0.5f);
                if (randomBool && countSpawnedActiveLetters < numOfActiveLetters && !usedPositions[x])
                {
                    usedPositions[x] = true;
                    CreateActiveLetter(spawnerPos[x]);
                    countSpawnedActiveLetters++;
                }               
            }
        }

        // Fill the rest of the positions with inactive letters
        for (int x = 0; x < usedPositions.Length; x++)
        {
            if (!usedPositions[x]) CreateInactiveLetter(spawnerPos[x]);
        }
    }
    
    //Helping Method
    private void CreateActiveLetter(Transform position)
    {
        Target2P activeLetterClone = Instantiate(activeLetter, position.position, transform.rotation);
        activeLetterClone.transform.SetParent(this.transform);
        /*Target2P activeLetterCloneTarget = activeLetterClone.GetComponent<Target2P>();
        activeLetterCloneTarget.SetLineAndTargetID(lineAndTargetID);*/
    }

    //Helping Method
    private void CreateInactiveLetter(Transform position)
    {
        int positionInArray = Random.Range(0, 3);
        Target2P inactiveLetterClone = Instantiate(inactiveLettersArray[positionInArray], position.position, transform.rotation);
        inactiveLetterClone.transform.SetParent(this.transform);
        inactiveLetterClone.SetIsTheRightTarget(false);
        /*Target2P inactiveLetterCloneTarget = inactiveLetterClone.GetComponent<Target2P>();
        inactiveLetterCloneTarget.SetLineAndTargetID(lineAndTargetID);*/
    }

    //Helping Method - not sure if this one is even needed ?! :O
    public void SetLineAndTargetID(int id)
    {
        lineAndTargetID = id;
    }
    
    //Helping Method
    public void DecrementActiveLetter()
    {
        numOfActiveLetters--;
        if (numOfActiveLetters == 0) Destroy(gameObject);
    }
    
    //Helping Method for accurate instantiation of active letter objects
    public void TryAgain()
    {
        while (numberOfInstances > 10)
        {
            numberOfInstances -= numOfActiveLetters;
            numOfActiveLetters--;
            numberOfInstances += numOfActiveLetters;
        }
        numberOfInstances = 0;
        /////////////////////////////////////
        Debug.Log("Count : " + count);
        count++;
        /////////////////////////////////////
        return;
    }
}
