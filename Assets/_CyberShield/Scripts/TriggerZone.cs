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
            // Here you can add code to trigger the scenario, such as calling a method on the ScenarioManager or starting a cutscene.
        }
    }
}