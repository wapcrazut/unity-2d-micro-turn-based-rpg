using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private int _damageAmount;
    private float _speed;
    private Character _target;
    private UnityAction _hitCallback;

    public void Init(int damageAmount, int speed, Character target, UnityAction onHitCallback)
    {
        _damageAmount = damageAmount;
        _speed = speed;
        _target = target;
        _hitCallback = onHitCallback;
    }

    private void Start()
    {
        if (_target == null)
        {
            Debug.LogError("Projectile target is null.");
        }
    }

    private void Update()
    {
        if (transform.position != _target.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        } 
        else 
        {
            _target.TakeDamage(_damageAmount);
            _hitCallback?.Invoke();
            Destroy(gameObject);
        }
    }
}
