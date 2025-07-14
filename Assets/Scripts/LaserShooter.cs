using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public Transform eyeLeft;
    public Transform eyeRight;
    public GameObject laserPrefab;

    private bool playerInRange = false;
    private bool hasFired = false;

    void Update()
    {
        if (playerInRange && !hasFired)
        {
            ShootLaser(eyeLeft);
            ShootLaser(eyeRight);
            hasFired = true;

            // اگر بخوای بعد از چند ثانیه دوباره اجازه شلیک بده
            Invoke(nameof(ResetFire), 2f);
        }
    }

    private void ShootLaser(Transform eye)
    {
        // Instantiate در موقعیت چشم، با همان چرخش
        GameObject laser = Instantiate(laserPrefab, eye.position, eye.rotation);
        // اسکریپت LaserMove خودش باعث حرکت لیزر رو به جلو می‌شه
    }

    private void ResetFire()
    {
        hasFired = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}