using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _changeSpeed = 1f;

    private AudioSource _audioSource;
    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    public void EnableAlarm()
    {
        StartVolumeChanging(_maxVolume);
    }

    public void DisableAlarm()
    {
        StartVolumeChanging(0f);
    }

    private void StartVolumeChanging(float targetVolume)
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        _volumeCoroutine = StartCoroutine(ChangeVolume(targetVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(
                _audioSource.volume,
                targetVolume,
                _changeSpeed * Time.deltaTime);

            yield return null;
        }

        if (Mathf.Approximately(targetVolume, 0f))
            _audioSource.Stop();

        _volumeCoroutine = null;
    }
}