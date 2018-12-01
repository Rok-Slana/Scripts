using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverTrigger : MonoBehaviour {

    private Player player;

    public void Start()
    {
        player = FindObjectOfType<Player>();
    }

    //Lose Trigger/Collider
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Target")
        {
            Debug.Log("Trigger");
            SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
            Destroy(player);
            Destroy(gameObject);
        }
    }
}
