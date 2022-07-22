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
    public float _runningTime = 0;

    private void Update()
    {
        if (_isPlaying)
        {
            _runningTime = Mathf.Clamp(_runningTime + Time.deltaTime, 0, _duration);
            float normalizedTime = _runningTime / _duration;
            _alarm.volume = Mathf.Lerp(_baseVolume, _targetVolume, normalizedTime);
        }
        else
        {
            _runningTime = Mathf.Clamp(_runningTime - Time.deltaTime, 0, _duration);
            float normalizedTime = _runningTime / _duration;
            _alarm.volume = Mathf.Lerp(_baseVolume, _targetVolume, normalizedTime);
        }
    }

    private void Start()
    {
        _isPlaying = false;
        _alarm = GetComponent<AudioSource>();
        _door.OnOpen += PlaySound;
        _door.OnClose += StopSound;
    }

    private void PlaySound() 
    {
        _alarm.volume = _baseVolume;
        _isPlaying = true;
        _alarm.Play();       
    }

    private void StopSound()
    {
        _isPlaying = false;
    }
}
