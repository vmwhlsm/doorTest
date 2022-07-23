using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private Door _door;

    private AudioSource _alarm;
    private float _baseVolume = 0;
    private float _targetVolume = 1;
    private float _duration = 5;
    private bool _isPlaying;
    private float _runningTime = 0;

    private void Start()
    {
        _alarm = GetComponent<AudioSource>();
        _door.Opened += PlaySound;
        _door.Closed += StopSound;
    }

    private void PlaySound() 
    {
        _alarm.volume = _baseVolume;
        _isPlaying = true;
        StartCoroutine(ChangeVolume());
    }

    private void StopSound()
    {
        _isPlaying = false;
    }

    private IEnumerator ChangeVolume()
    {
        _alarm.Play();

        while (_runningTime < _duration || _runningTime > 0) 
        {
            int sign;

            if (_isPlaying)
            {
                sign = 1;
            }
            else 
            {
                sign = -1;
            }

            _runningTime = Mathf.Clamp(_runningTime + Time.deltaTime * sign, 0, _duration);
            float normalizedTime = _runningTime / _duration;
            _alarm.volume = Mathf.Lerp(_baseVolume, _targetVolume, normalizedTime);

            yield return null;
        }

        _alarm.Stop();
    }
    
    private void OnDestroy()
    {
        _door.Opened -= PlaySound;
        _door.Closed -= StopSound;
    }
}
