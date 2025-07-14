using UnityEngine;
using System.Collections;

public class SimpleTransparency : MonoBehaviour
{
    private Renderer[] renderers;
    public float blinkDuration = 3f;      // چند ثانیه چشمک بزنه
    public float blinkInterval = 0.2f;    // فاصله بین هر چشمک

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cloud"))
        {
            StartCoroutine(BlinkEffect());
        }
    }

    IEnumerator BlinkEffect()
    {
        float timer = 0f;
        bool isGray = false;

        while (timer < blinkDuration)
        {
            foreach (Renderer rend in renderers)
            {
                rend.material.color = isGray ? Color.white : Color.gray;
            }

            isGray = !isGray;
            timer += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        // بعد از تموم شدن افکت، رنگ رو به حالت عادی برگردون
        foreach (Renderer rend in renderers)
        {
            rend.material.color = Color.white;
        }
    }
}