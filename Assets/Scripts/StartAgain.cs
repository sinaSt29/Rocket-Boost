using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartAgain : MonoBehaviour
{
    public GameObject pauseCanvas;
    // public GameObject Canvas;
    public Button StartAgainBtn;
    
    void Start()
    {
        StartAgainBtn.onClick.AddListener(StartFun);
    }

    void StartFun()
    {
        pauseCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        
    }
}
