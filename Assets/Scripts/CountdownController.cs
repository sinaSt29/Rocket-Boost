using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public GameObject countdownTextObj;       // آبجکت اصلی شمارش (برای فعال/غیرفعال کردن)
    public TextMeshProUGUI countdownText;     // خود نوشته شمارش
    public GameObject questionCanvas;         // پنل سوال
    public KeyActivator thisKey; // کلیدی که شمارش رو صدا زده


    public float delayBeforeStart = 0.5f;     // تأخیر اولیه (قابل تنظیم)
    void Awake()
    {
        countdownTextObj.SetActive(false);
    }

    public void StartCountdown()
    {
        Debug.Log("🎯 کلید فعال: " + thisKey);

        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        countdownTextObj.SetActive(true);

        string[] numbers = { "3", "2", "1" };

        foreach (string num in numbers)
        {
            countdownText.text = num;
            yield return new WaitForSeconds(1f);
        }

        countdownTextObj.SetActive(false);
        questionCanvas.SetActive(true);
        Time.timeScale = 0f;
        
        countdownTextObj.SetActive(false);
        
        

// اجرای سوال مربوط به کلید فعلی
        
    }
}
