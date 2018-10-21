using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	[Tooltip("Time delay for letter sound at the start of the game")]
	public float playLetterDelay = 1.5f;
	[Tooltip("Time before new target is spawned")]
	public float spawnDelay = 1.5f;
	[Tooltip("Number of initial hearts/lives the player has")]
	public int numOfHearts = 3;
	[Tooltip("Position of the rightmost heart sprite")]
	public Vector3 heartLocation;
	[Tooltip("Distance to the next added 'heart' sprite")]
	public Vector3 heartLocationIncrement;
	[Tooltip("Heart/life sprite")]
	public GameObject heart;
	//[Tooltip("")]
	public GameObject nextTarget;

	private Vector3 newPosition;
	
	private AudioSource audio;
	
	private Canvas canvas;
	
	//List<Target> targets = new List<Target>();
	
	private TargetParent[] targetParents;
	
	List<GameObject> lifeHearts = new List<GameObject>();


	void Start () {
		
		targetParents = FindObjectsOfType<TargetParent>();
		
		//Play sound of the target letter with delay
		audio = GetComponent<AudioSource>();
		Invoke("PlayLetter", playLetterDelay);
		canvas = FindObjectOfType<Canvas>();
		
		//Draw number of hearts/lives player has
		for(int i = 0; i<numOfHearts; i++){
			GameObject newHeart = Instantiate(heart, heartLocation, transform.rotation);
			heartLocation+=heartLocationIncrement;			
			newHeart.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
			lifeHearts.Add(newHeart);
		}		
	}
	
	//Subtracts players life && handle related visuals
	public void SubtractLife(){
		numOfHearts--;
		if(numOfHearts<=0){
			Application.Quit();
		}
		GameObject heart = lifeHearts[lifeHearts.Count-1];
		Destroy(heart);
		lifeHearts.Remove(lifeHearts[lifeHearts.Count-1]);
		heartLocation-=heartLocationIncrement;
	}
	
	//Add life to player and handle visuals
	public void AddLife(){
		if(numOfHearts<10){numOfHearts++;}		
		//   !!! DO NOT DELETE FOLLOWING CODE !!!
		GameObject newHeart = Instantiate(heart, heartLocation, transform.rotation);
		heartLocation+=heartLocationIncrement;			
		newHeart.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
		lifeHearts.Add(newHeart);
	}	
	
	//Spawn new target
	public void SpawnNewTarget(){
		//Get missing targets index from [targetPartents]
			
		for(int i = 0; i<targetParents.Length;i++){
			if(targetParents[i].activeParent){
                newPosition = targetParents[i].transform.position;
            }
        }
        //Debug.Log(x);		
		Invoke("InstantiateNewTarget", spawnDelay);
	}
	
	public void InstantiateNewTarget(){
		//Debug.Log("INT");
		GameObject newTarget= Instantiate(nextTarget, newPosition, transform.rotation);
		PlayLetter();
	}	
	
	//Play the desired letter
	public void PlayLetter(){
		audio.Play();
	}
	
}
