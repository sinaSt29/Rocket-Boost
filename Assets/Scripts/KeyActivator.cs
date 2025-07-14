using System;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using TMPro;
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
    [Header("UI References")]
    
    public GameObject questionCanvas;
    public RTLTextMeshPro questionText;
    public RTLTextMeshPro[] optionTexts;
    public Button[] optionButtons;
    public TextMeshProUGUI timerText;

    [SerializeField] private AudioClip SucsessAudio;
    [SerializeField] private AudioClip CrashAudio;
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        questionCanvas.SetActive(false); // پنل سؤال رو مخفی می‌کنیم
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
        if (other.CompareTag("Player") && canShowQuestion && currentQuestionIndex < questions.Count)
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

        questionText.text = currentQuestion.questionText;

        // 🔥 تایمر راه‌اندازی
        currentTime = timeLimit;
        timerRunning = true;
        timerText.text = Mathf.Ceil(currentTime).ToString();

        // 🔁 نمایش گزینه‌ها
        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < currentQuestion.options.Length)
            {
                optionTexts[i].text = currentQuestion.options[i];
                optionButtons[i].gameObject.SetActive(true);

                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
            }
            else
            {
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
        }

        questionCanvas.SetActive(false);
        
        Time.timeScale = 1f;
        GetComponent<MeshRenderer>().enabled = false; // ظاهر کلید رو برگردون
        GetComponent<Collider>().enabled = true; // کولایدر رو فعال کن
    }

    private void TimeOut()
    {
        Debug.Log("⛔ زمان تموم شد!");
        correctAnswersCount = 0; // ریست کردن تعداد جواب‌های درست
        questionCanvas.SetActive(false);
        Time.timeScale = 1f;
        GetComponent<MeshRenderer>().enabled = false; // ظاهر کلید رو برگردون
        GetComponent<Collider>().enabled = true; // کولایدر رو فعال کن
        canShowQuestion = true; // اجازه می‌ده سوال بعدی رو نشون بده
    }
}