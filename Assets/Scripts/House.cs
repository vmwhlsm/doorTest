using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]

public class House : MonoBehaviour
{
    private Door _door;
    private Collider2D _houseCollider;

    private void Start()
    {
        _houseCollider = GetComponent<Collider2D>();
        _door = GetComponentInChildren<Door>();
        _door.Opened += EnableCollider;
        _door.Closed += DisableCollider;
    }

    private void EnableCollider()
    {
        _houseCollider.enabled = true;
    }

    private void DisableCollider()
    {
        _houseCollider.enabled = false;
    }

    private void OnDestroy()
    {
        _door.Opened -= EnableCollider;
        _door.Closed -= DisableCollider;
    }
}
