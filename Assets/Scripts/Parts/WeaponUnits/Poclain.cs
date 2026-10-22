using UnityEngine;

public class Poclain : WeaponBase
{
    [SerializeField] private GameObject arm;

    public override float GetTime => 2;

    [Header("판정")]
    [SerializeField] Transform hitPoint;          // 타격 위치 (비워두면 자기 위치)
    [SerializeField] Vector3 offset = new Vector3(0f, 0f, 1.5f); // hitPoint 기준 로컬 오프셋
    [SerializeField] float radius = 1f;
    [SerializeField] LayerMask targetLayer;

    [Header("미리보기")]
    [SerializeField] Color gizmoColor = new Color(1f, 0f, 0f, 0.4f);

    // 버퍼를 재사용해 GC 발생을 막습니다.
    readonly Collider[] buffer = new Collider[32];

    Transform Origin => hitPoint != null ? hitPoint : transform;
    Vector3 HitPosition => Origin.TransformPoint(offset);

    public void DoAttack()
    {
        int count = Physics.OverlapSphereNonAlloc(HitPosition, radius, buffer, targetLayer);

        for (int i = 0; i < count; i++)
        {
            if (buffer[i].TryGetComponent(out Unit unit))
            {
                unit.OnDamaged(Power);
            }
        }
    }

    // 씬 뷰 미리보기 (선택하지 않아도 항상 표시)
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(HitPosition, radius);
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1f);
        Gizmos.DrawWireSphere(HitPosition, radius);
    }

    public override void SkillEffect()
    {
        DoAttack();
    }
}
