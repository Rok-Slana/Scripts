using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterLine : MonoBehaviour {

    public GameObject activeLetter;
    public GameObject[] otherLetters;

    public Transform[] spawnerPos;

    private GameObject[] letters;

    private Vector3[] childPos;


    void Start(){

        foreach(Transform position in spawnerPos){
            CreateLetter(position);
        }
    }


    private void CreateLetter(Transform position){

        GameObject activeLetterClone = Instantiate(activeLetter, position.position, transform.rotation);
    }

}
