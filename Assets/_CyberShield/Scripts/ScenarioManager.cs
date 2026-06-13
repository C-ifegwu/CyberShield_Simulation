using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ScenarioManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject phoneScreenUI;
    public TextMeshProUGUI senderNameText;
    public TextMeshProUGUI messageText;
    public Button[] choiceButtons; 

    [Header("Player & World References")]
    public FirstPersonController playerController;
    
    [Header("Panic Mechanics")]
    public PanicTimer panicTimer; // WE REPLACED THE RISK PATH WITH THE FUSE!
    
    [Header("Cinematics")]
    public EnvironmentManager envManager; 

    [Header("The Story Nodes")]
    public StoryNode[] storyNodes; 
    
    private int currentScore = 0;

    public void StartStory(int startingNodeIndex)
    {
        if (phoneScreenUI.activeInHierarchy) return; 

        phoneScreenUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerController.enabled = false; 

        LoadNode(startingNodeIndex);
    }

    private void LoadNode(int nodeIndex)
    {
        foreach (Button btn in choiceButtons)
        {
            btn.gameObject.SetActive(false);
        }

        StoryNode currentNode = storyNodes[nodeIndex];
        senderNameText.text = currentNode.senderName;
        
        StopAllCoroutines();
        StartCoroutine(TypewriterEffect(currentNode.messageText, currentNode.choices));
    }

    private IEnumerator TypewriterEffect(string textToType, Choice[] currentChoices)
    {
        messageText.text = ""; 
        
        foreach (char letter in textToType.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(0.02f); 
        }

        for (int i = 0; i < currentChoices.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentChoices[i].choiceText;
            
            int choiceIndex = i; 
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnChoiceMade(currentChoices[choiceIndex]));
        }
    }

    private void OnChoiceMade(Choice selectedChoice)
    {
        currentScore += selectedChoice.scoreChange;

        // --- THE FULLY BAKED CINEMATIC TRIGGER ---
        if (selectedChoice.triggerPanicMode)
        {
            envManager.TriggerPanic(); // Drops lights, starts heartbeat
            panicTimer.StartCountdown(); // Lights the 60-second fuse!
            
            ClosePhone(); // Force the phone shut so they HAVE to run!
            return; // Stop reading code so the next node doesn't load
        }
        else if (selectedChoice.triggerResolution)
        {
            envManager.TriggerResolution(); 
            panicTimer.StopCountdown();
        }

        // Normal flow if panic wasn't triggered
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