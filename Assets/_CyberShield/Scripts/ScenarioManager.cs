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
    public RiskPathManager riskPath;
    
    [Header("Cinematics")]
    public EnvironmentManager envManager; // Phase 3 Cinematic Link

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
        riskPath.AddDecisionPoint(selectedChoice.scoreChange);

        // --- PHASE 3 FIX: ACTUALLY TRIGGERING THE CINEMATICS ---
        if (selectedChoice.triggerPanicMode)
        {
            envManager.TriggerPanic(); // Drops the lights, starts the heartbeat!
        }
        else if (selectedChoice.triggerResolution)
        {
            envManager.TriggerResolution(); // Brings the sun back, stops heartbeat!
        }

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