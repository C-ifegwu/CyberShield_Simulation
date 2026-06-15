using UnityEngine;

public class ProximityDoor : MonoBehaviour
{
    private Animator doorAnimator;

    void Start()
    {
        // Get the Animator component attached to the same GameObject as this script
        doorAnimator = GetComponent<Animator>();
    }

    // This runs when the player walks into the green Box Collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Flips the Animator switch to True, opening the door!
            doorAnimator.SetBool("isOpen", true);
        }
    }

    // This runs when the player walks out of the green Box Collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Flips the Animator switch back to False, closing the door!
            doorAnimator.SetBool("isOpen", false);
        }
    }
}