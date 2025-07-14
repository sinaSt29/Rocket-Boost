using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject menuCanvas; // Canvas منوی شروع
    public Button startButton; // دکمه شروع بازی
    
    void Start()
    {
        Time.timeScale = 0;
        startButton.onClick.AddListener(StartGame);
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
        menuCanvas.SetActive(true);
    }
    private void StartGame()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f; // بازی رو از حالت متوقف خارج می‌کنه

    }
}

        