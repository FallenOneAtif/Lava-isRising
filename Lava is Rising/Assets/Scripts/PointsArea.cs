using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;

public class PointsArea : MonoBehaviour
{
    private PointSpawner spawner;
    [SerializeField] private Bounds bounds;
    [SerializeField] private BoxCollider2D collider;
    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        bounds = collider.bounds;
        spawner = GetComponentInParent<PointSpawner>();
        OnSpawn();
    }
    private void OnEnable()
    {

    }
    public void OnSpawn()
    {
        for (int i = 0; i < transform.childCount; i++ ) 
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
           Vector2 randomPoint = new Vector2(x, y);
            transform.GetChild(i).position = randomPoint;
        }
        transform.position = new Vector3(spawner.PositionMult * spawner.SpawnIndex, 0, 0);
        spawner.SpawnIndex += 1;
    }
    public void ReturnToPool()
    {
        LeanPool.Despawn(this.gameObject);
    }
}
