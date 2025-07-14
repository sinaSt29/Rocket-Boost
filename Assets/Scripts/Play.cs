using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    
    public GameObject menuCanvas; // Canvas منوی 
    public Button pauseButton;

    void Start()
    {
        pauseButton.onClick.AddListener(pauseGame);
    }

    private void pauseGame()
    {
        print("pause");
        Time.timeScale = 0;
        menuCanvas.SetActive(true);
    }
}