using UnityEngine;

public class rightGun : MonoBehaviour
{
    private bool playerInRange;

    public bool PlayerInRange { get => playerInRange; set => playerInRange = value; }

    public void OnTriggerEnter(Collider other) {
        if (string.Compare(other.tag, "Player") == 0) {
            PlayerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (string.Compare(other.tag, "Player") == 0) {
            PlayerInRange = false;
        }
    }
}
