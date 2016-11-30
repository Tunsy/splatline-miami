using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void LoadGame()
    {
        SceneManager.LoadScene("mainscene");
    }
}
