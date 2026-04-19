using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class SimpleCameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _targetOffset = new Vector3(0f, 1.6f, 0f);

    [Header("Distance")]
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float _minDistance = 2f;
    [SerializeField] private float _maxDistance = 8f;
    [SerializeField] private float _zoomSpeed = 2f;

    [Header("Rotation")]
    [SerializeField] private float _mouseSensitivity = 0.12f;
    [SerializeField] private float _minPitch = -30f;
    [SerializeField] private float _maxPitch = 70f;
    [SerializeField] private float _rotationSmoothTime = 0.03f;

    [Header("Follow")]
    [SerializeField] private float _positionSmoothTime = 0.05f;

    private float _targetYaw;
    private float _targetPitch;
    private float _currentYaw;
    private float _currentPitch;

    private float _yawVelocity;
    private float _pitchVelocity;

    private Vector3 _positionVelocity;

    private void Start()
    {
        if (_target == null)
        {
            enabled = false;
            Debug.LogError($"{nameof(SimpleCameraFollow)}: target is not assigned.", this);
            return;
        }

        Vector3 angles = transform.eulerAngles;
        _targetYaw = angles.y;
        _targetPitch = angles.x;
        _currentYaw = angles.y;
        _currentPitch = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        ReadMouseInput();
        HandleZoom();
        UpdateRotation();
        UpdatePosition();
    }

    private void ReadMouseInput()
    {
        if (Mouse.current == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        _targetYaw += mouseDelta.x * _mouseSensitivity;
        _targetPitch -= mouseDelta.y * _mouseSensitivity;
        _targetPitch = Mathf.Clamp(_targetPitch, _minPitch, _maxPitch);
    }

    private void HandleZoom()
    {
        if (Mouse.current == null)
            return;

        float scroll = Mouse.current.scroll.ReadValue().y * 0.01f;

        if (Mathf.Abs(scroll) > 0.001f)
        {
            _distance -= scroll * _zoomSpeed;
            _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
        }
    }

    private void UpdateRotation()
    {
        _currentYaw = Mathf.SmoothDampAngle(
            _currentYaw,
            _targetYaw,
            ref _yawVelocity,
            _rotationSmoothTime);

        _currentPitch = Mathf.SmoothDampAngle(
            _currentPitch,
            _targetPitch,
            ref _pitchVelocity,
            _rotationSmoothTime);
    }

    private void UpdatePosition()
    {
        Vector3 targetPoint = _target.position + _targetOffset;
        Quaternion rotation = Quaternion.Euler(_currentPitch, _currentYaw, 0f);

        Vector3 desiredPosition = targetPoint - rotation * Vector3.forward * _distance;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref _positionVelocity,
            _positionSmoothTime);

        transform.rotation = rotation;
    }
}