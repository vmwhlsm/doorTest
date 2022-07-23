using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private List<Transform> _pathTargets;
    [SerializeField] private float _speed;

    private Queue<Transform> _queuePathTargets;

    private void Start()
    {
        _queuePathTargets = new Queue<Transform>(_pathTargets);    
    }

    private void Update()
    {
        if (_queuePathTargets.Count > 0)
        {
            var target = _queuePathTargets.Peek();

            if (Mathf.Abs(transform.position.x - target.transform.position.x) > 0.5)
            {
                MoveTo(target.transform);
            }
            else
            {
                _queuePathTargets.Dequeue();
            }
        }
    }

    private void MoveTo(Transform transform)
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, transform.position, _speed);
    }
} 
