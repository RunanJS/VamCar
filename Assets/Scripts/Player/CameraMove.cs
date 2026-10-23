using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float posLerp = 5f;
    [SerializeField] float rotLerp = 5f;

    Vector3 posOffset;      // 타겟 기준 로컬 오프셋
    float pitch;            // 위아래 각도 (offset에서 가져옴)

    void Start()
    {
        // 시작 위치를 타겟 로컬 좌표로 저장
        posOffset = target.InverseTransformPoint(transform.position);
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 타겟의 y축 회전만 사용
        Quaternion yaw = Quaternion.Euler(0f, target.eulerAngles.y, 0f);

        // 등뒤 목표 위치
        Vector3 desiredPos = target.position + yaw * posOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, posLerp * Time.deltaTime);

        // 타겟 방향(yaw) + 고정 pitch
        Quaternion desiredRot = Quaternion.Euler(pitch, target.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotLerp * Time.deltaTime);
    }
}
