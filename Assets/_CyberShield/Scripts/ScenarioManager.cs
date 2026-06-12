using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections; // Required for the Typewriter effect

public class ScenarioManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject phoneScreenUI;
    public TextMeshProUGUI senderNameText;
    public TextMeshProUGUI messageText;
    public Button[] choiceButtons; // Array to hold our 3 buttons

    [Header("Player & World References")]
    public FirstPersonController playerController;
    public RiskPathManager riskPath;
    
    [Header("The Story Nodes")]
    public StoryNode[] storyNodes; // This holds our entire branching story!
    
    private int currentScore = 0;

    // Triggered by the Raycast!
    public void StartStory(int startingNodeIndex)
    {
        // THE FIX: If the phone UI is already open, ignore the laser pointer!
        if (phoneScreenUI.activeInHierarchy) return; 

        phoneScreenUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerController.enabled = false; // Freeze player

        LoadNode(startingNodeIndex);
    }

    private void LoadNode(int nodeIndex)
    {
        // Hide all buttons first
        foreach (Button btn in choiceButtons)
        {
            btn.gameObject.SetActive(false);
        }

        StoryNode currentNode = storyNodes[nodeIndex];
        senderNameText.text = currentNode.senderName;
        
        // Start the realistic typing effect
        StopAllCoroutines();
        StartCoroutine(TypewriterEffect(currentNode.messageText, currentNode.choices));
    }

    private IEnumerator TypewriterEffect(string textToType, Choice[] currentChoices)
    {
        messageText.text = ""; // Clear old text
        
        // Type out each letter one by one for realism
        foreach (char letter in textToType.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(0.02f); // Typing speed
        }

        // Once typing is done, show the correct amount of buttons!
        for (int i = 0; i < currentChoices.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(true);
            
            // Set button text
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentChoices[i].choiceText;
            
            // Wire up the button's click event dynamically
            int choiceIndex = i; // Store locally for the listener
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnChoiceMade(currentChoices[choiceIndex]));
        }
    }

    private void OnChoiceMade(Choice selectedChoice)
    {
        // 1. Update Score and Hologram
        currentScore += selectedChoice.scoreChange;
        riskPath.AddDecisionPoint(selectedChoice.scoreChange);

        // 2. Check for Environmental Shifts (Phase 3 Prep)
        if (selectedChoice.triggerPanicMode)
        {
            Debug.Log("PANIC MODE TRIGGERED! The world goes dark...");
            // We will add the audio and light shifts here in Phase 3!
        }
        else if (selectedChoice.triggerResolution)
        {
            Debug.Log("RESOLUTION TRIGGERED! The world is safe.");
        }

        // 3. Go to next node OR close the phone
        if (selectedChoice.nextNodeIndex == -1)
        {
            ClosePhone();
        }
        else
        {
            LoadNode(selectedChoice.nextNodeIndex);
        }
    }

    private void ClosePhone()
    {
        phoneScreenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerController.enabled = true;
    }
}