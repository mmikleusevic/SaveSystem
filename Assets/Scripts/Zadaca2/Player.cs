using UnityEngine;
using UnityEngine.UI;

namespace Zadaca2
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        
        [SerializeField] private float speed = 10;
        [SerializeField] private float maxHealth = 100;
        
        private float health;
        private Vector3 direction;

        private void Start()
        {
            health = maxHealth;
        }

        private void Update()
        {
            direction = Vector3.zero;
            
            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.down;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.up;
            }

            transform.Rotate(direction * (Time.deltaTime * speed));
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Bullet bullet = Instantiate(bulletPrefab);
                bullet.SetForward(transform.forward);
            }
        }

        public void Damage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                GameManager.Instance.PlayerDied();
                Destroy(gameObject);
            }
        }
    }
}