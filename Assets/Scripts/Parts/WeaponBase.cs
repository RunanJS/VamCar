using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : PartBase
{
    public float power = 100;
    public int maxCatchCount;
    public List<TargetCatcher> catchers;// = new List<TargetCatcher>();

    public float Power => CarManager.Inst.Stat.atk * power / 100;

    // 모든 catcher의 범위 안 대상을 중복 없이 반환 (maxCount: 최대 인원, 0 이하면 제한 없음)
    public List<Unit> CatchAll(int maxCount = 0)
    {
        var res = new HashSet<Unit>();
        foreach (var c in catchers)
            res.UnionWith(c.Catch(transform));

        var list = new List<Unit>(res);
        if (maxCount > 0 && list.Count > maxCount)
        {
            // 무기에서 가까운 순으로 정렬 후 n명만 남김
            Vector3 pos = transform.position;
            list.Sort((a, b) =>
                (a.transform.position - pos).sqrMagnitude
                .CompareTo((b.transform.position - pos).sqrMagnitude));
            list.RemoveRange(maxCount, list.Count - maxCount);
        }
        return list;
    }

    // 씬 뷰 미리보기 (재생 전에도 표시)
    protected virtual void OnDrawGizmos()
    {
        if (catchers == null) return;
        foreach (var c in catchers)
            c.DrawGizmos(transform);
    }
}

[System.Serializable]
public class TargetCatcher
{
    [Header("판정")]
    [SerializeField] Transform hitPoint;          // 타격 위치 (비워두면 자기 위치)
    [SerializeField] Vector3 offset = new Vector3(0f, 0f, 1.5f); // hitPoint 기준 로컬 오프셋
    [SerializeField] float radius = 1f;
    [SerializeField] LayerMask targetLayer;

    [Header("미리보기")]
    [SerializeField] Color gizmoColor = new Color(1f, 0f, 0f, 0.4f);

    // 버퍼를 재사용해 GC 발생을 막습니다.
    readonly Collider[] buffer = new Collider[32];

    Vector3 GetPos(Transform self)
    {
        Transform origin = hitPoint != null ? hitPoint : self;
        return origin.TransformPoint(offset);
    }

    public List<Unit> Catch(Transform self)
    {
        int count = Physics.OverlapSphereNonAlloc(GetPos(self), radius, buffer, targetLayer);

        var res = new List<Unit>();
        for (int i = 0; i < count; i++)
            if (buffer[i].TryGetComponent(out Unit unit))
                res.Add(unit);
        return res;
    }

    public void DrawGizmos(Transform self)
    {
        Vector3 pos = GetPos(self);
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(pos, radius);
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1f);
        Gizmos.DrawWireSphere(pos, radius);
    }
}