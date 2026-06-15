using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class PanicTimer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    
    [Header("Fuse Settings")]
    public float timeLimit = 60f; // How long does the player have to run before the scammer catches them?
    private float currentTime;
    private bool isTimerRunning = false;

    [Header("Visuals")]
    public float maxRadius = 15f; // How big should the panic circle be at the start? Adjust this to fit your scene!
    public int segments = 50; // How smooth should the circle look? More segments = smoother but more performance cost.

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = false; // Start with the circle hidden until the timer starts
        
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

            // Calculate how much time is left as a percentage of the total time limit
            float timePercent = currentTime / timeLimit; 
            
            // The radius of the circle should shrink as time runs out, creating a sense of urgency. When timePercent is 1 (full time), the radius is maxRadius. When timePercent is 0 (time's up), the radius is 0.
            float currentRadius = maxRadius * timePercent;

            DrawCircle(currentRadius);

            if (currentTime <= 0)
            {
                isTimerRunning = false;
                Debug.Log("TIME IS UP! SCAMMER EXPOSED YOU. GAME OVER.");
                PlayerPrefs.SetInt("TotalScore", -500); // Harsh penalty for getting caught, but it will be interesting to see on the Results screen!
                SceneManager.LoadScene("ResultsScene");
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