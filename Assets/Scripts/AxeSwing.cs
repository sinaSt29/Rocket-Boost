using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    public float swingAngle = 120f;    // حداکثر زاویه انحراف
    public float swingSpeed = 3f;     // سرعت نوسان

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }
}
