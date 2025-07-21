using UnityEngine;
using UnityEngine.UI;

public class GuiderScript : MonoBehaviour
{

    [Header("UI References")] 
    public GameObject controllerCanvas;
    public GameObject guidermenuCanvas; //  منوی راهنما
    public GameObject thismenuCanvas; //  منوی فعلی
    public Button guidertButton; // دکمه شروع بازی
    void Start()
    {
        guidermenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 0;
        guidertButton.onClick.AddListener(ShowGuiderCanvas);
    }


    void ShowGuiderCanvas()
    {
        controllerCanvas.gameObject.SetActive(false);
        guidermenuCanvas.gameObject.SetActive(true);
        thismenuCanvas.gameObject.SetActive(false);
    }
    
    void Update()
    {
        
    }
}
