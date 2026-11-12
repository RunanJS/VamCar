using System;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] float life = 0.7f;
    [SerializeField] float riseSpeed = 2f;

    static Camera cam;
    float timer;
    Color baseColor;
    Action<DamagePopup> release;

    public void Init(Action<DamagePopup> release) => this.release = release;

    public void Show(Vector3 pos, string str, Color color, float scale)
    {
        transform.position = pos;
        transform.localScale = Vector3.one * scale;
        text = GetComponent<TextMeshPro>();
        text.text = str;
        baseColor = color;
        text.color = color;
        timer = 0f;
    }

    void Update()
    {
        if (cam == null) cam = Camera.main;

        timer += Time.deltaTime;
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        if (cam != null) transform.forward = cam.transform.forward;   // 항상 카메라를 향함

        float t = timer / life;
        Color c = baseColor;
        c.a = 1f - t * t;                                             // 점점 투명해짐
        text.color = c;

        if (timer >= life) release(this);
    }
}