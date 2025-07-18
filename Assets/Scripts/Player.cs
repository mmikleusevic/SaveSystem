using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 30;
    [SerializeField] private float maxHealth;
    [SerializeField] private TextMeshProUGUI keyText;
    
    private Rigidbody rb;

    private Vector3 direction;
    private float health;
    private bool hasKey;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        //GameManager.Instance.HealthUI(health, maxHealth);
    }

    private void Update()
    {
        direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector3.right;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = speed * direction;
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Time.timeScale = 0;
            GameManager.Instance.ShowResetPanel();
        }

        GameManager.Instance.HealthUI(health, maxHealth);
    }

    public void OpenDoor(Vrata vrata)
    {
        if (!hasKey) return;
        
        vrata.OpenDoor();
    }

    public void PickupKey()
    {
        hasKey = true;
        keyText.gameObject.SetActive(false);
    }
}
