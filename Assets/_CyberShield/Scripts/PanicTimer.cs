using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PanicTimer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    
    [Header("Fuse Settings")]
    public float timeLimit = 60f; // 60 seconds to find the Police Station!
    private float currentTime;
    private bool isTimerRunning = false;

    [Header("Visuals")]
    public float maxRadius = 15f; // Starts huge, like a wide net
    public int segments = 50; // Makes the circle smooth

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = false; // Hide it until the panic starts
        
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    public void StartCountdown()
    {
        currentTime = timeLimit;
        isTimerRunning = true;
        lineRenderer.enabled = true;
    }

    public void StopCountdown()
    {
        isTimerRunning = false;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            // Calculate percentage of time left (1.0 down to 0.0)
            float timePercent = currentTime / timeLimit; 
            
            // The circle physically shrinks as time runs out!
            float currentRadius = maxRadius * timePercent;

            DrawCircle(currentRadius);

            if (currentTime <= 0)
            {
                isTimerRunning = false;
                Debug.Log("TIME IS UP! SCAMMER EXPOSED YOU. GAME OVER.");
                // We will load the Game Over screen here in Phase 4!
            }
        }
    }

    private void DrawCircle(float radius)
    {
        float angle = 0f;
        for (int i = 0; i < (segments + 1); i++)
        {
            // Trigonometry to draw a perfect circle around the player
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            // Draws the circle down near the floor (-0.8f on the Y axis)
            lineRenderer.SetPosition(i, new Vector3(x, -0.8f, z));
            angle += (360f / segments);
        }
    }
}