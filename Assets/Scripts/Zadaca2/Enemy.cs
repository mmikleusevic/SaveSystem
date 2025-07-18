using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace Zadaca2
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        
        [SerializeField] private float maxHealth;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private int scoreAmount;
            
        private float health;
        
        private Player target;
        
        private void Start()
        {
            health = maxHealth;    
            healthImage.fillAmount = health / maxHealth;
        }

        private void Update()
        {
            if (!target) return;
            
            float step = speed * Time.deltaTime;
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.Damage(damage);
                Destroy(gameObject);
            }
        }

        public void Damage(float damage)
        {
            health -= damage;
            healthImage.fillAmount = health / maxHealth;

            if (health <= 0)
            {
                Destroy(gameObject);
                GameManager.Instance.IncreaseScore(scoreAmount);
            }
        }
        
        public void SetTarget(Player player)
        {
            target = player;
        }
    }
}