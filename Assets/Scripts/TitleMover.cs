using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMover : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] CanvasGroup fade;
    [SerializeField] float fadeTime = 0.6f;        // 질주 끝 부분에서 어두워지는 시간

    [Header("회전 (타이틀 대기)")]
    [SerializeField] float radius = 6f;
    [SerializeField] float height = 2.5f;
    [SerializeField] float orbitSpeed = 15f;        // 초당 회전 각도
    [SerializeField] float lookHeight = 0.5f;       // 타겟 중심에서 얼마나 위를 바라볼지

    [Header("시작 연출")]
    [SerializeField] Vector3 startOffset = new Vector3(-3f, 1.5f, 5f);   // 타겟 로컬 기준, 음수 x = 왼쪽, 양수 z = 전방
    [SerializeField] float moveTime = 1.5f;         // 좌전방으로 이동하는 시간
    [SerializeField] float launchDelay = 0.4f;      // 도착 후 출발 전 대기
    [SerializeField] float launchAccel = 25f;       // 가속도
    [SerializeField] float launchTime = 2.5f;       // 질주 시간

    public event Action OnLaunched;                 // 연출이 끝났을 때 (씬 전환 등에 사용)

    bool started;
    float angle;

    Vector3 LookPoint => target.position + Vector3.up * lookHeight;

    void Start()
    {
        fade.alpha = 0f;
        fade.blocksRaycasts = false;   // 투명한데 버튼 클릭을 막지 않도록
        fade.interactable = false;

        // 현재 카메라 위치에서 시작하도록 각도 초기화 (시작할 때 튀지 않게)
        Vector3 d = transform.position - target.position;
        angle = Mathf.Atan2(d.x, d.z) * Mathf.Rad2Deg;
    }

    void Update()
    {
        if (started || target == null) return;

        angle += orbitSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;

        transform.position = target.position + new Vector3(Mathf.Sin(rad) * radius, height, Mathf.Cos(rad) * radius);
        transform.rotation = Quaternion.LookRotation(LookPoint - transform.position);
    }

    // 시작 버튼에서 호출
    public void GameStart()
    {
        if (started || target == null) return;
        started = true;
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        // 1. 좌전방으로 이동하며 타겟을 계속 바라봄
        Vector3 fromPos = transform.position;
        Quaternion fromRot = transform.rotation;
        Vector3 goalPos = target.TransformPoint(startOffset);

        for (float t = 0f; t < 1f; t += Time.deltaTime / moveTime)
        {
            float s = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(fromPos, goalPos, s);

            Quaternion look = Quaternion.LookRotation(LookPoint - transform.position);
            transform.rotation = Quaternion.Slerp(fromRot, look, s);
            yield return null;
        }
        transform.position = goalPos;

        // 2. 잠깐 대기
        yield return new WaitForSeconds(launchDelay);

        // 3. 타겟만 앞으로 가속하며 질주 (카메라는 제자리에서 지켜봄)
        float speed = 0f;
        float fadeStart = Mathf.Max(0f, launchTime - fadeTime);

        for (float t = 0f; t < launchTime; t += Time.deltaTime)
        {
            speed += launchAccel * Time.deltaTime;
            target.position += target.forward * speed * Time.deltaTime;

            transform.rotation = Quaternion.LookRotation(LookPoint - transform.position);

            // 마지막 fadeTime 동안 서서히 어두워짐
            if (fade != null && t > fadeStart)
                fade.alpha = Mathf.Clamp01((t - fadeStart) / fadeTime);

            yield return null;
        }

        if (fade != null) fade.alpha = 1f;             // 완전히 어둡게 고정

        OnLaunched?.Invoke();                 // 필요하면 다른 곳에서도 감지 가능
        SceneManager.LoadScene("InGame");
    }
}