using UnityEngine;

public class CloseQuestionPanel : MonoBehaviour
{
    public GameObject panelToClose;

    public void ClosePanel()
    {
        panelToClose.SetActive(false);
        Time.timeScale = 1f;
    }
}