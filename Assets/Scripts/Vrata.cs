using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Vrata : MonoBehaviour
{
    [SerializeField] private GameObject doorText;
    
    private bool isOpen;
    private bool hasKey;

    private void Start()
    {
        doorText.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player player)) return;

        if (!isOpen && player.HasKey)
        {
            hasKey = true;
            doorText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        doorText.SetActive(false);
    }

    private void OpenDoor()
    {
        if (!hasKey) return;
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        while (!isOpen)
        {
            transform.Rotate(new Vector3(0, -90 * Time.deltaTime, 0));
            yield return null;

            if (transform.rotation.eulerAngles.y > 90) break;
        }
        
        isOpen = true;
    }
}
