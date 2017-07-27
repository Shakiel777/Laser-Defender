using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string name)
    {
		Debug.Log ("Level load requested for: "+name);
        SceneManager.LoadScene(name);
	}
	public void QuitLevel()
    {
		Debug.Log ("Quit game request received");
	}

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        print("Load next level request");

    }
}
