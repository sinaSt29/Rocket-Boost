using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject menuCanvas; // Canvas منوی 
    public Button pauseButton;

    void Start()
    {
        menuCanvas.SetActive(false);
        pauseButton.onClick.AddListener(pauseGame);
    }

    private void pauseGame()
    {
        Time.timeScale = 1;
        menuCanvas.SetActive(false);
    }
}