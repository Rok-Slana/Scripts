using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterLine : MonoBehaviour {
    public GameObject activeLetter;
    public GameObject[] otherLetters;
    public Transform[] spawnerPos;

    private GameObject[] letters;
    private Vector3[] childPos;

    void Start()
    {
        foreach(Transform position in spawnerPos)
        {
            CreateLetter(position);
        }
    //fill letters[] with active and other letters of which at least one has to be active letter
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if there are any active letters left
        //if(left) do nothing
        //else destroy this line
    }

    private void CreateLetter(Transform position)
    {
        GameObject activeLetterClone = Instantiate(activeLetter, position.position, transform.rotation);
    }

}
