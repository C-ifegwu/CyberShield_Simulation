using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads the City Scene (make sure you spell your city scene name perfectly here!)
        SceneManager.LoadScene("CityScene"); // CHANGE "CityScene" to whatever your city scene is actually named!
    }
}