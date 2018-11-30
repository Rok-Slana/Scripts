using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region
    [Tooltip("Time delay for letter sound at the start of the game")]
    public float playLetterDelay = 1.5f;

    [Tooltip("Set targets downward velocity")]
    public float targetVelocity = 1.0f;

    [Tooltip("Set time between two letter lines spawn")]
    public float timeBetweenLetterSpawn = 4.0f;

    [Tooltip("Number of initial hearts/lives the player has")]
    public int numOfHearts = 3;

    [Tooltip("Number of targets destroyed needed for progression on to the next letter")]
    public int requiredHits;

    [Tooltip("Position of the rightmost heart sprite")]
    public Vector3 heartLocation;

    [Tooltip("Distance to the next added 'heart' sprite")]
    public Vector3 heartLocationIncrement;

    [Tooltip("Heart/life sprite")]

    public GameObject heart;

    public GameObject nextTarget;

    private Vector3 newPosition;
    
    List<GameObject> lifeHearts = new List<GameObject>();

    
    private int numberOfInstances = 0;                          //holds number of created instances of current active letter
    private int targetsSpawned = 0;
    private int activeLetterIndex = 0;
    private int[] inactiveLettersIndex;
    public Target2P[] alphabet;                                 //Store all letters here in alphabetical order   
    private Target2P activeLetter;                              //Store active letter object here
    private Target2P[] inactiveLetters = new Target2P[3];       //Store other three inactive letter objects here

    private Text text;
    #endregion

    void Start()
    {
        //Draw number of hearts/lives player has
        for (int i = 0; i < numOfHearts; i++)
        {
            GameObject newHeart = Instantiate(heart, heartLocation, transform.rotation);
            heartLocation += heartLocationIncrement;
            newHeart.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
            lifeHearts.Add(newHeart);
        }

        AsignLetterObjects();

        text = FindObjectOfType<Text>();
    }

    //Subtracts players life && handle related visuals
    public void SubtractLife()
    {
        numOfHearts--;
        if (numOfHearts <= 0)
        {
            Application.Quit();
        }
        GameObject heart = lifeHearts[lifeHearts.Count - 1];
        Destroy(heart);
        lifeHearts.Remove(lifeHearts[lifeHearts.Count - 1]);
        heartLocation -= heartLocationIncrement;
    }

    //Add life to player and handle visuals
    public void AddLife()
    {
        if (numOfHearts < 10)  numOfHearts++;

        GameObject newHeart = Instantiate(heart, heartLocation, transform.rotation);
        heartLocation += heartLocationIncrement;
        newHeart.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
        lifeHearts.Add(newHeart);
    }
    
    public void IncrementTargetsSpawned(int num)
    {
        targetsSpawned += num;

        if (targetsSpawned >= requiredHits)
        {
            targetsSpawned = 0;
            activeLetterIndex++;

            if (activeLetterIndex >= alphabet.Length) activeLetterIndex = 0;

            AsignLetterObjects();
        }

        text.GetComponent<UnityEngine.UI.Text>().text = "Targets Hit : " + targetsSpawned;

    }

    //Asign-ReAsign active and inactive letter objects
    private void AsignLetterObjects()
    {
        activeLetter = alphabet[activeLetterIndex];
        FillInactiveLetters();
    }

    // Fills inactive letters array with inactive letter objects
    private void FillInactiveLetters()
    {
        int newIndex = activeLetterIndex + 1;
        int sum = alphabet.Length - activeLetterIndex;

        if (sum <= 3) newIndex -= 4;

        for (int x = 0; x < 3; x++)
        {
            inactiveLetters[x] = alphabet[newIndex];
            inactiveLetters[x].SetIsTheRightTarget(false);
            newIndex++;
        }
    }

    #region
    //Returns active letter Object to LetterLine
    public Target2P ReturnActiveLetter() { return activeLetter; }

    //Returns inactive letters objects to LetterLine
    public Target2P[] ReturnInactiveLetters() { return inactiveLetters; }

    //Returns target downward velocity - sets velocity in Target2P
    public float SetTargetVelocity() { return targetVelocity; }

    //Returns target spawn rate - sets spawn rate in LargeTargetSpawner
    public float SetTargetSpawnRate() { return timeBetweenLetterSpawn; }

    //Not sure if this is used at all ?!
    public int GetNumberOfRequiredHits() { return requiredHits; }

    //Increments numberOfInstances value by value
    public void SetNumberOfInstances(int value) { numberOfInstances += value; }

    //Returns number of instances - to LineLetter??
    public int GetNumberOfInstances() { return numberOfInstances; }
    #endregion
}
