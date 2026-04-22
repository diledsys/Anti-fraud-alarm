using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private float _rotationTolerance = 0.1f;

    private Quaternion _closedRotation;
    private Quaternion _openedRotation;
    private Coroutine _rotationCoroutine;

    private void Awake()
    {
        _closedRotation = transform.localRotation;
        _openedRotation = _closedRotation * Quaternion.Euler(0f, _openAngle, 0f);
    }

    public void Open()
    {
        StartRotation(_openedRotation);
    }

    public void Close()
    {
        StartRotation(_closedRotation);
    }

    private void StartRotation(Quaternion targetRotation)
    {
        if (_rotationCoroutine != null)
            StopCoroutine(_rotationCoroutine);

        _rotationCoroutine = StartCoroutine(Rotate(targetRotation));
    }

    private IEnumerator Rotate(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > _rotationTolerance)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime);

            yield return null;
        }

        transform.localRotation = targetRotation;
        _rotationCoroutine = null;
    }
}