using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : PartBase
{
    public float power = 100;
    public List<TargetCatcher> catchers;// = new List<TargetCatcher>();

    public float Power => CarManager.Inst.Stat.atk * power / 100;

    // 모든 catcher의 범위 안 대상을 중복 없이 반환
    public List<Unit> CatchAll()
    {
        var res = new HashSet<Unit>();
        foreach (var c in catchers)
            res.UnionWith(c.Catch(transform));
        return new List<Unit>(res);
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