using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject _enemyPrefab;

    void Start()
    {
        _enemyPrefab = Resources.Load<GameObject>("Enemy");

        for (int i=0; i<30; i++)
        {
            CreateEnemy();
        }

        StartCoroutine(CreateEnemyCoroutine());
    }

    private void CreateEnemy()
    {
        float x = Utils.GetRandomFloatBetween(0, 140);
        Vector2 pos = new Vector2(x: x, y: 3.05f);
        Instantiate(original: _enemyPrefab, position: pos, rotation: Quaternion.identity);
    }

    private IEnumerator CreateEnemyCoroutine()
    {
        while (true)
        {
            CreateEnemy();
            yield return new WaitForSeconds(0.65f);
        }
        
    }
}
