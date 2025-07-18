using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.TryGetComponent(out Player player)) return;
        
        player.Damage(damage);
        Destroy(gameObject);
    }
}
