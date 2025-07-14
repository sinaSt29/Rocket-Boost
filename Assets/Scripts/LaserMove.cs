using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public float speed = 10f;        // سرعت حرکت لیزر
    public float lifeTime = 3f;      // چند ثانیه بعد حذف بشه

    void Start()
    {
        Destroy(gameObject, lifeTime);  // حذف خودکار بعد از چند ثانیه
    }

    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime); // حرکت به جلو
    }
}