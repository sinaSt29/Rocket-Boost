using UnityEngine;

public class RingFloat : MonoBehaviour
{
    public float floatHeight = 0.5f;   // 🔼 ارتفاع بالا و پایین شدن
    public float floatSpeed = 1f;      // 🕐 سرعت حرکت

    private Vector3 startPos;
    private float timer;

    void Start()
    {
        startPos = transform.position; // ذخیره موقعیت اولیه
    }

    void Update()
    {
        timer += Time.deltaTime * floatSpeed;
        float offsetY = Mathf.Sin(timer) * floatHeight;

        transform.position = new Vector3(
            startPos.x,
            startPos.y + offsetY,
            startPos.z
        );
    }
}