using System;
using NUnit.Framework;
using UnityEngine;

public class Gas : MonoBehaviour
{
    float damage;
    [SerializeField] float timer = 0;
    float duration = 1;
    float durationTimer;
    bool upgraded = false;
    readonly Collider[] buffer = new Collider[32];
    LayerMask targetLayer;

    public void SetGas(TargetCatcher _catcher, MixerUnit unit)
    {
        targetLayer = _catcher.GetLayer;
        this.damage = unit.Power;
        this.durationTimer = unit.gasDuration;
        this.duration = unit.gasDuration;
        this.upgraded = unit.grade == unit.MaxGrade;
        transform.localScale = Vector3.one * _catcher.GetRadius * 2;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            timer = 0;
            float radius = transform.localScale.x * 0.5f;
            int count = Physics.OverlapSphereNonAlloc(transform.position, radius, buffer, targetLayer);

            for (int i = 0; i < count; i++)
            {
                if (buffer[i].TryGetComponent(out Unit unit))
                    CarManager.Attack(damage / (duration * 2), unit);
            }
        }

        durationTimer -= Time.deltaTime;
        if (durationTimer <= 0)
        {
            Destroy(gameObject);
        }

        transform.localScale += Vector3.one * Time.deltaTime * 0.4f;

        if (upgraded)
        {
            Transform target = EnemyManager.Instance.GetNearestEnemy(transform.position).transform;
            if (target == null) return; 

            Vector3 pos = transform.position;
            Vector3 goal = new Vector3(target.position.x, pos.y, target.position.z);

            if ((goal - pos).sqrMagnitude < 0.5f * 0.5f) return;   // 0.5m 이내면 정지

            float t = 1f - Mathf.Exp(-2 * Time.deltaTime);   // 프레임에 독립적인 보간
            transform.position = Vector3.Lerp(pos, goal, t);
        }
    }
}
