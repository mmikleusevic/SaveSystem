using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 bulletSpawnPosition;
    [SerializeField] private float maxBulletTime = 3;
    
    private Rigidbody rb;

    private float timer;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.localPosition = bulletSpawnPosition;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= maxBulletTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = speed * Vector3.forward;
    }
}
