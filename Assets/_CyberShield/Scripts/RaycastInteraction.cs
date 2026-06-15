using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastInteraction : MonoBehaviour
{
    private PlayerControls controls;
    
    [Header("Raycast Settings")]
    public float interactDistance = 5f; // How far can the player interact with objects? Adjust this to fit your scene!
    public Camera mainCamera; //Reference to the player's camera, so we know where to shoot the ray from and in which direction
    public ScenarioManager scenarioManager; // Reference to the ScenarioManager so we can trigger story events when the player interacts with phones

    private void Awake()
    {
        controls = new PlayerControls();
        
        // Set up our input callback for the Interact action, which will trigger our ShootLaser method whenever the player presses the interact button (default is left mouse click)
        controls.Player.Interact.performed += ctx => ShootLaser();
    }

    private void OnEnable() { controls.Player.Enable(); }
    private void OnDisable() { controls.Player.Disable(); }

    private void ShootLaser()
    {
        // 1. Create a ray that starts at the camera's position and points forward in the direction the camera is facing
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