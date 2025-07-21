using System;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class QuestionData
{
    public string questionText;
    public string[] options;
    public int correctAnswerIndex;
}

public class KeyActivator : MonoBehaviour
{
    [Header("Player Reference")]
    public GameObject player; // رفرنس به گیم‌ابجکت پلیر
    private CollisionObjects CollisionObjectsSctipt;
    
    [Header("UI References")]
    public GameObject questionCanvas;
    public RTLTextMeshPro questionText;
    public RTLTextMeshPro[] optionTexts;
    public Button[] optionButtons;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countdownText; // متن برای شمارش معکوس

    [SerializeField] private AudioClip SucsessAudio;
    [SerializeField] private AudioClip CrashAudio2;
    [SerializeField] private ParticleSystem successParticle;
    [SerializeField] private ParticleSystem crashParticle;

    [Header("Question Data")]
    public List<QuestionData> questions = new List<QuestionData>();
    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0;
    private bool canShowQuestion = true;
    private AudioSource audioSource;

    [Header("Timer")]
    public float timeLimit = 20f;
    private float currentTime;
    private bool timerRunning = false;

    [Header("Countdown")]
    public float countdownDuration = 3f; // مدت زمان شمارش معکوس

    private void Start()
    {
        CollisionObjectsSctipt = player.GetComponent<CollisionObjects>();
        audioSource = GetComponent<AudioSource>();
        questionCanvas.SetActive(false); // پنل سوال رو مخفی می‌کنیم
        if (countdownText != null)
            countdownText.gameObject.SetActive(false); // مخفی کردن متن شمارش معکوس
    }

    private void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.unscaledDeltaTime;
            timerText.text = Mathf.Ceil(currentTime).ToString();

            if (currentTime <= 0)
            {
                timerRunning = false;
                TimeOut();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canShowQuestion 
        && currentQuestionIndex < questions.Count&& !CollisionObjectsSctipt.isDead)
        {
            ShowQuestion();
            GetComponent<MeshRenderer>().enabled = false; // ظاهر کلید رو مخفی کن
            GetComponent<Collider>().enabled = false; // کولایدر رو غیرفعال کن
            canShowQuestion = false;
        }
    }

    private void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Count) return;

        QuestionData currentQuestion = questions[currentQuestionIndex];
        
        questionCanvas.SetActive(true);
        Time.timeScale = 0f; // بازی رو متوقف می‌کنیم

        // اطمینان از فعال بودن questionText
        if (questionText != null)
        {
            questionText.gameObject.SetActive(true);
            questionText.text = currentQuestion.questionText;
            Debug.Log($"نمایش سوال: {currentQuestion.questionText}"); // برای دیباگ
        }
        else
        {
            Debug.LogError("questionText لینک نشده!");
        }

        // 🔥 تایمر راه‌اندازی
        currentTime = timeLimit;
        timerRunning = true;
        timerText.text = Mathf.Ceil(currentTime).ToString();

        // مخفی کردن متن شمارش معکوس
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        // 🔁 نمایش گزینه‌ها
        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < currentQuestion.options.Length)
            {
                optionTexts[i].gameObject.SetActive(true); // اطمینان از فعال بودن
                optionTexts[i].text = currentQuestion.options[i];
                optionButtons[i].gameObject.SetActive(true);

                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
            }
            else
            {
                optionTexts[i].gameObject.SetActive(false);
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnOptionSelected(int selectedIndex)
    {
        timerRunning = false;

        QuestionData currentQuestion = questions[currentQuestionIndex];

        if (selectedIndex == currentQuestion.correctAnswerIndex)
        {
            successParticle.Play();
            audioSource.PlayOneShot(SucsessAudio);
            correctAnswersCount++;
        }
        else
        {
            crashParticle.Play();
            audioSource.PlayOneShot(CrashAudio2);
            correctAnswersCount = 0; // ریست کردن تعداد جواب‌های درست
        }

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            canShowQuestion = true;
            questionCanvas.SetActive(false);
            Time.timeScale = 1f;
            GetComponent<MeshRenderer>().enabled = true; // ظاهر کلید رو برگردون
            GetComponent<Collider>().enabled = true; // کولایدر رو فعال کن
        }
        else
        {
            if (correctAnswersCount == questions.Count)
            {
                KeyManager.Instance.ActivateNextKeyImage(); // نمایش کلید کوچیک توی کنواس
                Debug.Log("همه سوالات درست جواب داده شد! کلید ظاهر شد!");
            }
            else
            {
                Debug.Log("همه سوالات نمایش داده شد، اما همه درست نبودن!");
            }

            // شروع شمارش معکوس
            StartCoroutine(CountdownAndResume());
        }
    }

    private void TimeOut()
    {
        Debug.Log("⛔ زمان تموم شد!");
        correctAnswersCount = 0; // ریست کردن تعداد جواب‌های درست
        questionCanvas.SetActive(false);
        Time.timeScale = 1f;
        GetComponent<MeshRenderer>().enabled = true; // ظاهر کلید رو برگردون
        GetComponent<Collider>().enabled = true; // کولایدر رو فعال کن
        canShowQuestion = true; // اجازه می‌ده سوال بعدی رو نشون بده
    }

    private IEnumerator CountdownAndResume()
    {
        // مخفی کردن سوال و گزینه‌ها
        if (questionText != null)
            questionText.gameObject.SetActive(false);
        foreach (var button in optionButtons)
            button.gameObject.SetActive(false);
        foreach (var optionText in optionTexts)
            optionText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        // نمایش متن شمارش معکوس
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            float countdown = countdownDuration;
            while (countdown > 0)
            {
                countdownText.text = Mathf.Ceil(countdown).ToString();
                countdown -= Time.unscaledDeltaTime;
                yield return null;
            }
            countdownText.text = "0";
            countdownText.gameObject.SetActive(false);
        }

        // غیرفعال کردن کنواس و ادامه بازی
        questionCanvas.SetActive(false);
        Time.timeScale = 1f;
        GetComponent<MeshRenderer>().enabled = false; // ظاهر کلید رو برگردون
        GetComponent<Collider>().enabled = false; // کولایدر رو فعال کن
    }
}