using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CollisionObjects : MonoBehaviour
{
    [SerializeField] private AudioClip SucsessAudio;
    [SerializeField] private AudioClip ClapingAudio;
    [SerializeField] private AudioClip CrashAudio;
    [SerializeField] private ParticleSystem successParticle;
    [SerializeField] private ParticleSystem crashParticle;

    public bool isDead;
    
    private AudioSource audioSource;

    private void Start()
    {
        isDead = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "StartLaunch":
                print("u start");
                break;
            case "Finish":
                InvokeLoadNextLevel();
                break;

            default:
                InvokeDeadPlayer();
                break;
        }
    }

    private void InToTarget()
    {
        // successParticle.Play();
        // audioSource.PlayOneShot(SucsessAudio);
    }


    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "kidRoom":
                InvokeLoadKidRoomLevel();
                break;
            case "artRoom":
                InvokeLoadArtRoomLevel();
                break;
            case "catRoom":
                InvokeLoadCatRoomLevel();
                break;
            case "Key":
                InToTarget();

                break;
        }
    }


    private void InvokeLoadCatRoomLevel()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Invoke("loadCatRoomLevel", 0.5f);
        }
    }

    void loadCatRoomLevel()
    {
        SceneManager.LoadScene(3);
    }

    private void InvokeLoadArtRoomLevel()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Invoke("loadArtRoomLevel", 0.5f);
        }
    }

    private void loadArtRoomLevel()
    {
        SceneManager.LoadScene(2);
    }

    void InvokeLoadKidRoomLevel()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Invoke("loadKidRoomLevel", 0.5f);
        }
    }

    private void loadKidRoomLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void InvokeLoadNextLevel()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(ClapingAudio);
        }

        successParticle.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadAgain", 6);
    }

    void LoadAgain()
    {
        SceneManager.LoadScene(0);
    }

    private void InvokeDeadPlayer()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(CrashAudio);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadLevel", 1);

        isDead = true;
    }

    void LoadNextLevel()
    {
        int NextScene;
        int activeScene = SceneManager.GetActiveScene().buildIndex;

        if (activeScene == SceneManager.sceneCountInBuildSettings - 1)
        {
            NextScene = 0;
        }
        else
        {
            NextScene = activeScene + 1;
        }

        SceneManager.LoadScene(NextScene);
    }

    void LoadLevel()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(activeScene);
    }
}