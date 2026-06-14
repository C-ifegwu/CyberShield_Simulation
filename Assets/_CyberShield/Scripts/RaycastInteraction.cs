using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastInteraction : MonoBehaviour
{
    private PlayerControls controls;
    
    [Header("Raycast Settings")]
    public float interactDistance = 5f; // How far the laser reaches
    public Camera mainCamera; // Where the laser shoots from
    public ScenarioManager scenarioManager; // Reference to the Scenario Manager

    private void Awake()
    {
        controls = new PlayerControls();
        
        // Listen for the exact moment the Left Mouse Button is clicked
        controls.Player.Interact.performed += ctx => ShootLaser();
    }

    private void OnEnable() { controls.Player.Enable(); }
    private void OnDisable() { controls.Player.Disable(); }

    private void ShootLaser()
    {
        // 1. Create the invisible ray starting from the camera and pointing dead ahead
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        // 2. Draw a red line in the Scene view so you can visually debug it (invisible to players)
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 2f);

        // 3. Actually shoot the raycast and see if it hits a collider within our distance
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                // Look for the PhoneData script on the phone we just clicked
                PhoneData phone = hit.collider.GetComponent<PhoneData>();
                
                if (phone != null)
                {
                    // Start the story at the exact node this specific phone asks for!
                    scenarioManager.StartStory(phone.startingNode);
                }
            }
        }
    }
}