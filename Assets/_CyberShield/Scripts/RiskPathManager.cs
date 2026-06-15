using UnityEngine;

public class RiskPathManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int currentStep = 0;
    private Vector3 currentPosition = Vector3.zero; // Starts at 0,0,0 relative to the Hologram object

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        // Start the line with exactly 1 point
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, currentPosition);
    }

    // This method is called whenever the player makes a decision that affects their risk score. It updates the line renderer to visually represent the player's path through the scenario.
    public void AddDecisionPoint(int scoreChange)
    {
        currentStep++; // Move to the next step in the timeline
        lineRenderer.positionCount = currentStep + 1; // Add a new vertex to the line

        // 1. Move the line forward on the desk (Z axis)
        currentPosition.z += 0.2f; 

        // 2. Move the line UP if it was a good choice, DOWN if it was a bad choice (Y axis)
        if (scoreChange > 0)
        {
            currentPosition.y += 0.15f; // Safe! Path goes up.
        }
        else
        {
            currentPosition.y -= 0.15f; // Danger! Path goes down.
        }

        // 3. Draw the new line segment
        lineRenderer.SetPosition(currentStep, currentPosition);
    }
}