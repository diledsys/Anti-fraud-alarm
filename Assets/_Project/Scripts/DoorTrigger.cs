using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _door.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _door.Close();
    }
}