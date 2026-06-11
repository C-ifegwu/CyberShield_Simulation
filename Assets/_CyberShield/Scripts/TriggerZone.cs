using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public string scenarioName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is tagged "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered: " + scenarioName);
            // We will add the logic to open the UI here in Phase 4!
        }
    }
}