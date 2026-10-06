using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private readonly List<Enemy> enemies = new();

    public IReadOnlyList<Enemy> Enemies => enemies;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Register(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void Unregister(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public Enemy GetNearestEnemy(Vector3 position)
    {
        Enemy nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (Enemy enemy in enemies)
        {
            if (enemy == null)
                continue;

            float distance = (enemy.transform.position - position).sqrMagnitude;

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }
}