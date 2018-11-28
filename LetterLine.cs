using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterLine : MonoBehaviour {

    public GameObject activeLetter;
    public GameObject[] otherLetterArray;

    public Transform[] spawnerPos;

    // stores position values where a letter has already been spawned
    private bool[] usedPositions = new bool[] { false, false, false, false, false };

    private int numOfActiveLetters = 0;
    private int lineAndTargetID;

    void Start(){
        
        numOfActiveLetters = Random.Range(1, 4);
        int countActiveLetters = 0;

        // Min 1 & max 3 letters out of 5 are active letters
        // Instantiate randomly letters accros letterLine so that there is a corresponding number of 'active' letters to the number above, others must be 'inactive'
        // Fill randomly with active letters

        while (countActiveLetters < numOfActiveLetters) {
            for (int x = 0; x < usedPositions.Length; x++) {

                // Return random bool value
                bool randomBool = (Random.value > 0.5f);

                if (randomBool && countActiveLetters < numOfActiveLetters && !usedPositions[x]){
                    usedPositions[x] = true;
                    CreateActiveLetter(spawnerPos[x]);
                    countActiveLetters++;
                }               
            }
        }

        // Fill the rest of the positions with inactive letters
        for (int x = 0; x < usedPositions.Length; x++) {
            if (!usedPositions[x]) {
                CreateInactiveLetter(spawnerPos[x]);
            }
        }
    }

    private void Update()
    {
        //check for active letters with given ID
        //when there are no active letters left of a certain ID, destroy destroy other letters with same ID and self
    }


    private void CreateActiveLetter(Transform position){
        GameObject activeLetterClone = Instantiate(activeLetter, position.position, transform.rotation);
        activeLetterClone.transform.SetParent(this.transform);
        Target2P activeLetterCloneTarget = activeLetterClone.GetComponent<Target2P>();
        activeLetterCloneTarget.SetLineAndTargetID(lineAndTargetID);
    }

    private void CreateInactiveLetter(Transform position){
        int positionInArray = Random.Range(0, 3);
        GameObject inactiveLetterClone = Instantiate(otherLetterArray[positionInArray], position.position, transform.rotation);
        inactiveLetterClone.transform.SetParent(this.transform);
        Target2P inactiveLetterCloneTarget = inactiveLetterClone.GetComponent<Target2P>();
        inactiveLetterCloneTarget.SetLineAndTargetID(lineAndTargetID);
    }

    public void SetLineAndTargetID(int id) {
        lineAndTargetID = id;
    }

    public void DecrementActiveLetter()
    {
        Debug.Log("HEY HEY HEEEY");
        numOfActiveLetters--;
        if (numOfActiveLetters == 0)
        {
            Destroy(gameObject);
        }
    }




}
