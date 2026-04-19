using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 _closedPositionAngle;
    [SerializeField] private Vector3 _openedPositionAngle;
    [SerializeField] private float _rotateSpeed = 120f;

    private bool _isOpen;

    private void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(_isOpen ? _openedPositionAngle : _closedPositionAngle);

        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            _rotateSpeed * Time.deltaTime);
    }

    public void Open()
    {
        _isOpen = true;
    }

    public void Close()
    {
        _isOpen = false;
    }
}