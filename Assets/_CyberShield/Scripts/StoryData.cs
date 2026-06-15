using UnityEngine;

[System.Serializable]
public class Choice
{
    public string choiceText;
    public int nextNodeIndex; // Tells the game which story node to load next (-1 means close the app)
    public int scoreChange;
    public bool triggerPanicMode; // If true, the sky gets dark and heartbeat starts!
    public bool triggerResolution; // If true, the world turns bright again!
}

[System.Serializable]
public class StoryNode
{
    public string senderName; // Who is sending this message? (e.g. "Mom", "Boss", "Unknown Caller")
    
    [TextArea(3, 5)] // This makes the text box bigger in the Unity Inspector
    public string messageText;
    
    public Choice[] choices; // A list of choices the player can make
}