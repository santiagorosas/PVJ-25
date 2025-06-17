using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    private GameObject _explosionPrefab;

    static public ExplosionManager Instance;

    public void Start()
    {
        Instance = this;

        _explosionPrefab = Resources.Load<GameObject>("Explosion");
    }

    public void CreateExplosion(Vector2 position)
    {
        GameObject explosionGameObject = Instantiate(original: _explosionPrefab, position: position, rotation: Quaternion.identity);
        StartCoroutine(WaitAndDestroyExplosion(explosionGameObject: explosionGameObject));
    }

    private IEnumerator WaitAndDestroyExplosion(GameObject explosionGameObject)
    {
        yield return new WaitForSeconds(4);
        Destroy(explosionGameObject);
    }
}
