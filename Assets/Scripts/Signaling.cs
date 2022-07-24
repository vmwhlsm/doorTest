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
        _activeChangeVolumeNumber++;
        _alarm.Play();
        StartCoroutine(ChangeVolume());
    }

    private void StopSound()
    {
        _isPlaying = false;
        _activeChangeVolumeNumber++;
        StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        while ((_alarm.volume < _targetVolume && _isPlaying == true) || (_alarm.volume > _baseVolume && _isPlaying == false)) 
        {
            int sign = _isPlaying ? 1 : -1;
            _runningTime = Mathf.Clamp(_runningTime + Time.deltaTime * sign, _minRunningTime, _duration);
            float normalizedTime = _runningTime / _duration;
            _alarm.volume = Mathf.Lerp(_baseVolume, _targetVolume, normalizedTime);
          
            yield return null;

            if (_activeChangeVolumeNumber > 1)
                break;
        }

        _activeChangeVolumeNumber--;
    }
    
    private void OnDestroy()
    {
        _door.Opened -= PlaySound;
        _door.Closed -= StopSound;
    }
}
