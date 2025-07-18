using System;
using UnityEngine;

namespace Zadaca2
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private Rigidbody rb;

        private Vector3 forward;
        private float timer;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        }

        private void Update()
        {
            rb.linearVelocity = forward * speed;

            if (timer >= 3)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.Damage(10);
                Destroy(gameObject);
            }
        }

        public void SetForward(Vector3 forward)
        {
            this.forward = forward;
        }
    }
}