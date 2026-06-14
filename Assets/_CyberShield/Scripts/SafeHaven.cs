using UnityEngine;
// using UnityEngine.SceneManagement; // We will uncomment this in Phase 4!

public class SafeHaven : MonoBehaviour
{
    [Header("System Links")]
    public EnvironmentManager envManager;
    public PanicTimer panicTimer;

    [Header("Haven Settings")]
    public string havenName = "Police Station";
    public int survivalBonus = 100; // Massive points for doing the right thing

    private bool isResolved = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the physical player ran into the trigger box
        if (other.CompareTag("Player") && !isResolved)
        {
            isResolved = true; // Prevents triggering it twice
            
            Debug.Log($"PLAYER REACHED THE {havenName.ToUpper()}! YOU ARE SAFE.");

            // 1. Kill the Panic! Bring the sun back and stop the heartbeat.
            envManager.TriggerResolution();
            
            // 2. Stop the burning fuse!
            panicTimer.StopCountdown();

            // 3. Save the score permanently (Prep for our final Results screen)
            int currentScore = PlayerPrefs.GetInt("TotalScore", 0);
            PlayerPrefs.SetInt("TotalScore", currentScore + survivalBonus);

            // 4. In Phase 4, we will load the final Results Screen right here!
            // SceneManager.LoadScene(2); 
        }
    }
}