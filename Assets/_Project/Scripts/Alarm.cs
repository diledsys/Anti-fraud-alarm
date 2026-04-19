using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _changeSpeed = 0.1f;

    private AudioSource _audioSource;
    private bool _isThiefInside;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
        //_audioSource.loop = true;

        if (_audioSource.isPlaying == false)
            _audioSource.Play();
    }

    private void Update()
    {
        float targetVolume = _isThiefInside ? _maxVolume : 0f;

        _audioSource.volume = Mathf.MoveTowards(
            _audioSource.volume,
            targetVolume,
            _changeSpeed * Time.deltaTime);
    }

    public void SetThiefInside(bool isInside)
    {
        _isThiefInside = isInside;
    }
}