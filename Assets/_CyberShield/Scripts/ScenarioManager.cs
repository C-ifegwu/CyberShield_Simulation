using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject phoneScreenUI;
    public TextMeshProUGUI scenarioText;
    public Button button1;
    public Button button2;
    public Button button3;

    [Header("Player Reference")]
    public FirstPersonController playerController;

    // This gets called by our Raycast script!
    public void OpenSextortionScenario()
    {
        // 1. Show the UI
        phoneScreenUI.SetActive(true);
        
        // 2. Set the scenario text
        scenarioText.text = "Unknown Number:\n'Send me money right now or I will leak your private photos to your entire contact list.'";
        
        // 3. Set up the buttons (Button, Text, Score Change)
        SetupButton(button1, "Pay them the money", -10);
        SetupButton(button2, "Block and Report", 20);
        SetupButton(button3, "Tell a trusted adult", 30);

        // 4. Unlock the mouse cursor so you can click the buttons!
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // 5. Stop the player from walking around while on the phone
        playerController.enabled = false;
    }

    private void SetupButton(Button btn, string choiceText, int scoreChange)
    {
        btn.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => MakeChoice(scoreChange));
    }

    public void MakeChoice(int scoreChange)
    {
        Debug.Log("Choice selected! Score changed by: " + scoreChange);
        
        // Phase 5: This is where we will tell the Line Renderer to draw a path!
        
        CloseScenario();
    }

    private void CloseScenario()
    {
        phoneScreenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerController.enabled = true; // Let the player walk again
    }
}