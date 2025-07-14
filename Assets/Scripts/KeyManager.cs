using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    public Image[] keyImages;
    private int currentKeyIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        foreach (Image img in keyImages)
            img.enabled = false;
    }

    public void ActivateNextKeyImage()
    {
        if (currentKeyIndex < keyImages.Length)
        {
            keyImages[currentKeyIndex].enabled = true;
            currentKeyIndex++;
        }
    }
}