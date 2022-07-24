using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private UnityEvent _opened;
    [SerializeField] private UnityEvent _closed;

    private bool _isOpened;

    public event UnityAction Opened
    {
        add => _opened.AddListener(value);
        remove => _opened.RemoveListener(value);
    }

    public event UnityAction Closed
    {
        add => _closed.AddListener(value);
        remove => _closed.RemoveListener(value);
    }

    private void Start()
    {
        _isOpened = false;
    }

    public void Interact()
    {
        if (_isOpened)
        {
            _isOpened = false;
            _closed.Invoke();
        }
        else
        {
            _isOpened = true;
            _opened.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractIfThief(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractIfThief(collision);
    }

    private void InteractIfThief(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Thief>(out _))
        {
            Interact();
        }
    }

}
