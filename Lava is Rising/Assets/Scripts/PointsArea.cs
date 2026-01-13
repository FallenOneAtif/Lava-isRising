using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PointsArea : MonoBehaviour
{
    public PointSpawner spawner;
    [SerializeField] private Bounds bounds;
    [SerializeField] private BoxCollider2D collider;
    public float RayRadius;
    public float EnemyChance;
    [SerializeField] private GameObject EnemySingle, EnemyMult;
    bool AllowReturn;
    private void Awake()
    {
    }
    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        bounds = collider.bounds;

        OnSpawn();
    }
    private void OnEnable()
    {

    }
    public void OnSpawn()
    {
        for (int i = 0; i < transform.childCount - 2;) 
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 randomPoint = new Vector2(x, y);
            if (true)
            {
                transform.GetChild(i).position = randomPoint;
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<CircleCollider2D>().enabled = true;
                i++;
            }
        }
        if (Random.value * 100 < EnemyChance)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 randomPoint = new Vector2(x, y);
            if (true)
            {
                if (Random.value < 0.5f)
                {
                    EnemySingle.SetActive(true);
                    EnemySingle.transform.position = randomPoint;
                }
                else
                {
                    EnemyMult.SetActive(true);
                    EnemyMult.transform.position = randomPoint;
                }
            }
        }
        transform.position = new Vector3(spawner.HorizontalPos * spawner.HorizontalIndex, spawner.VerticalPos * spawner.VerticalIndex, 0);

        if (spawner.VerticalIndex < 2)
        {
            spawner.VerticalIndex += 1;
        }
        else
        {
            spawner.VerticalIndex = 0;
            spawner.HorizontalIndex += 1;
        }

    }
    public void ReturnToPool()
    {
        LeanPool.Despawn(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            AllowReturn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && AllowReturn)
        {
            ReturnToPool();
            AllowReturn = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RayRadius);
    }
}
