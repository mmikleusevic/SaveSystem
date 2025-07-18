using System.Collections;
using UnityEngine;

public class Vrata : MonoBehaviour
{
    private bool isOpen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) )
        {
            player.OpenDoor(this);
        }
    }

    public void OpenDoor()
    {
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
