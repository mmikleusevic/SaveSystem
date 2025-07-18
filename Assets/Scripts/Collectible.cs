using System;
using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int scoreAmount;
    [SerializeField] private float maximum = 3;
    
    private float minimum;
    private float timer = 0;
    private void Start()
    {
        minimum = transform.position.y;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        transform.position =
            new Vector3(transform.position.x, Mathf.Lerp(minimum, maximum, timer), transform.position.z);
        
        timer += 0.5f * Time.deltaTime;
        Debug.Log(timer);
        if (timer < 1.0f) return;
        
        (maximum, minimum) = (minimum, maximum);
        timer = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GameManager.Instance.IncreaseScore(scoreAmount);
            Destroy(gameObject);
        }   
    }
}
