using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI messageText;

    void Start()
    {
        // Unlock the mouse so the player can click "Quit" or "Restart" if you add those buttons later!
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Fetch the score from the device's permanent memory
        int finalScore = PlayerPrefs.GetInt("TotalScore", 0);
        scoreText.text = "Final Score: " + finalScore;

        // Dynamic educational messaging based on their survival
        if (finalScore > 0)
        {
            messageText.text = "You survived the simulation.\n\nRemember: The internet is permanent, but you are never trapped. Always seek help from a trusted adult in the real world.";
            messageText.color = Color.white;
        }
        else
        {
            messageText.text = "You were exposed.\n\nScammers and bullies rely on your fear and silence. If this happens in real life, block them, do not pay, and tell an adult immediately.";
            messageText.color = Color.red;
        }
    }
}