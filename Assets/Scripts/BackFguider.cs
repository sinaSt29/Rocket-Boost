using UnityEngine;
using UnityEngine.UI;
public class BackFguider : MonoBehaviour
{
    [Header("Canvas")] 
    public GameObject guiderCanvas;
    public GameObject menuCanvas;
    public GameObject controllerrCanvas;

    [Header("buttom")] public Button backButtom;
    void Start()
    {
        backButtom.onClick.AddListener(BackFun);
        print("DDDDDDDDDDDD");
    }


    void BackFun()
    {
        guiderCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
        controllerrCanvas.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
