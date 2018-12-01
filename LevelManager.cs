using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

	public void QuitApplication()
    {
        Application.Quit();
    }

    public void LoadLevel(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
