using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] AnimationCurve spawnPerSec;  // X: 경과 분, Y: 초당 소환 수
    [SerializeField] Enemy enemyPrefab;

    float elapsed = 0;
    float acc;


    private CarManager Player => CarManager.Inst;
    
    void Start()
    {
        StartCoroutine(Spawning());   
    }

    IEnumerator Spawning()
    {
        while (true)
        {
            Spawn();
            elapsed++;
            yield return new WaitForSeconds(2);
        }
    }

    public void Spawn()
    {
        Vector3 pos = Player.gameObject.transform.position;

        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, 
            Mathf.Sin(angle)) * 20;

        Vector3 spawnPos = pos + offset;
        spawnPos.y = 1f;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity).target = Player.gameObject;
    }
}
