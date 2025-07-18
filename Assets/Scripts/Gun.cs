using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private TextMeshProUGUI roundsText;
    [SerializeField] private int magazineRounds;
    
    private int maxMagazineRounds;
    private bool isShooting;
    private bool isReloading;

    private void Start()
    {
        maxMagazineRounds = magazineRounds;
        ReloadText("Rounds: " + magazineRounds + " / " + maxMagazineRounds);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isShooting && !isReloading)
        {
            StartCoroutine(SpawnBullet());
        }
        
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator SpawnBullet()
    {
        if (magazineRounds <= 0) yield break;

        isShooting = true;

        Instantiate(bulletPrefab, transform);

        magazineRounds--;

        ReloadText("Rounds: " + magazineRounds + " / " + maxMagazineRounds);
        
        yield return new WaitForSeconds(0.15f);
        
        isShooting = false;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        
        ReloadText("Reloading...");
        
        yield return new WaitForSeconds(2f);
        
        isShooting = false;
        isReloading = false;
        
        magazineRounds = maxMagazineRounds;
        ReloadText("Rounds: " + magazineRounds + " / " + maxMagazineRounds);
    }

    private void ReloadText(string text)
    {
        roundsText.text = text;
    }
}
