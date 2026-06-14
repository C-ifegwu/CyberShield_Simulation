using UnityEngine;

public class ProximityDoor : MonoBehaviour
{
    private Animator doorAnimator;

    void Start()
    {
        // Grabs the Animator component attached to this door
        doorAnimator = GetComponent<Animator>();
    }

    // This runs the exact frame the player touches your green Box Collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Flips the Animator switch to True!
            doorAnimator.SetBool("isOpen", true);
        }
    }

    // This runs when the player walks out of the green Box Collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Flips the Animator switch to False, closing the door!
            doorAnimator.SetBool("isOpen", false);
        }
    }
}