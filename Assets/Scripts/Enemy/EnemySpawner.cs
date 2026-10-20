using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;

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
            yield return new WaitForSeconds(5);
        }
    }

    public void Spawn()
    {
        Vector3 pos = Player.gameObject.transform.position;

        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, 
            Mathf.Sin(angle)) * 20;

        Vector3 spawnPos = pos + offset;
        spawnPos.y = 1f;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
