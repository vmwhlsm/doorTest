using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private Door _door;

    private AudioSource _alarm;
    private float _baseVolume = 0;
    private float _targetVolume = 1;
    private float _duration = 5;
    private bool _isPlaying;
    private float _runningTime = 0;
    private int _activeChangeVolumeNumber = 0;
    private float _minRunningTime = 0;

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
        _activeChangeVolumeNumber++;

        while (_alarm.volume > _baseVolume || _isPlaying == true) 
        {
            int sign = _isPlaying ? 1 : -1;

            if (_alarm.volume < _targetVolume || _isPlaying == false)
            {
                _runningTime = Mathf.Clamp(_runningTime + Time.deltaTime * sign, _minRunningTime, _duration);
                float normalizedTime = _runningTime / _duration;
                _alarm.volume = Mathf.Lerp(_baseVolume, _targetVolume, normalizedTime);
            }
          
            yield return null;

            if (_activeChangeVolumeNumber > 1)
            {
                break;
            }
        }

        if (_activeChangeVolumeNumber == 1)
        {
            _alarm.Stop();
        }

        _activeChangeVolumeNumber--;
    }
    
    private void OnDestroy()
    {
        _door.Opened -= PlaySound;
        _door.Closed -= StopSound;
    }
}
