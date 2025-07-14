using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3f; // مقدار جان اولیه
    public Slider healthSlider; // اسلایدر جان

    public AudioClip CrashCloud;


    private AudioSource _audioSource;

    private void Start()
    {
        // تنظیم اسلایدر به مقدار اولیه جان
        healthSlider.maxValue = health;
        healthSlider.value = health;

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // وقتی به ابر برخورد میکنیم
        if (other.CompareTag("cloud")) // فرض میکنیم ابر تگ "Cloud" رو داره
        {
            TakeDamage(3f); // کم کردن یک واحد از جان
        }

        if (other.CompareTag("heart"))
        {
            TakeHealth(2f);
        }
    }

    private void Update()
    {
        if (health == 0)
        {
            GetComponent<Movement>().enabled = false;
            _audioSource.PlayOneShot(CrashCloud);
            Invoke("LoadLevel", 1);
        }
    }

    void LoadLevel()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(activeScene);
    }

    private void TakeHealth(float h)
    {
        health += h;
        healthSlider.value = health;
    }

    private void TakeDamage(float damage)
    {
        health -= damage; // کم کردن جان
        health = Mathf.Max(health, 0); // جلوگیری از منفی شدن جان

        // به روز رسانی اسلایدر جان
        healthSlider.value = health;
    }
}