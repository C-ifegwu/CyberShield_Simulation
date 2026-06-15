using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads the scene called "Simulation" when the player clicks the Play button in the main menu
        SceneManager.LoadScene("Simulation");
    }
}