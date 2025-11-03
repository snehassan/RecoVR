using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField]private GameObject[] enemies;
    [SerializeField] private Collider spawnRange1Collider;
    [SerializeField] private Collider spawnRange2Collider;
    [SerializeField] private Collider spawnRange3Collider;

    private int spawnRange;

    // Start is called before the first frame update
    void Start()
    {
        spawnRange = PlayerPrefs.GetInt("SpawnRange", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool spaceTakenCondition;
    private bool spawnRangeCondition;
    private Vector3 positionToSpawn;
    public void SpawnEnemyAtRandomPosition(int times)
    {
        
        for (int i = 0; i < times; i++)
        {
            spawnRangeCondition = false;
            spaceTakenCondition = false;
            while (!spaceTakenCondition || !spawnRangeCondition)
            {
                FindAptPos();
            }
            SpawnAtPos(positionToSpawn);
        }
    }
    void FindAptPos()
    {
        float x = Random.Range(-20, 20);
        float y = Random.Range(1, 2);
        float z = Random.Range(-20, 20);

        positionToSpawn = new Vector3(x, y, z);
        spaceTakenCondition = Physics.OverlapSphere(positionToSpawn, 0.7f).Length > 0;

        switch (spawnRange)
        {
            case 1:
                spawnRangeCondition = ColliderContainsPoint(spawnRange1Collider.transform, positionToSpawn);
                break;
            case 2:
                spawnRangeCondition = ColliderContainsPoint(spawnRange2Collider.transform, positionToSpawn);
                break;
            case 3:
                spawnRangeCondition = ColliderContainsPoint(spawnRange3Collider.transform, positionToSpawn);
                break;

        }
    }

    void SpawnAtPos(Vector3 pos)
    {
        GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];

        Instantiate(randomEnemy, pos, Quaternion.identity);
    }

    bool ColliderContainsPoint(Transform ColliderTransform, Vector3 Point, bool Enabled = true)
    {
        Vector3 localPos = ColliderTransform.InverseTransformPoint(Point);
        if (Enabled && Mathf.Abs(localPos.x) < 0.5f && Mathf.Abs(localPos.y) < 0.5f && Mathf.Abs(localPos.z) < 0.5f)
            return true;
        else
            return false;
    }
}
