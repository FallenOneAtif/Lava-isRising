using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

using static UnityEditor.PlayerSettings;
using System.Reflection.Emit;

public class PointSpawner : MonoBehaviour
{
    [SerializeField] private Camera MainCam;
    [SerializeField] private GameObject GrapplePoint, EnemyMulti, EnemySingle;
    [SerializeField] private Vector2 PositionSpawn;
    [SerializeField] private Grappler grappler;
    [SerializeField] private Transform Player;
    private float RandomX,RandomY;
    public float RayRadius;
    int Height, Width, BaseReq;  
    Vector2 Position;
    public List<GameObject> PooledPoints;
    private bool allowEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {

    }
    void Start()
    {
        SpawnObjectAtStart(12);
        BaseReq = 1;
        Height = Screen.height;
        Width = Screen.width;
        Position = MainCam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Debug.Log(Position.x);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Player.position.x > Position.x * BaseReq)
        {
            SpawnObjects(0, 2, 7);
        }
        else if (Player.position.y > Position.y * BaseReq)
        {
            SpawnObjects(2, 0, 7);
        }
    }
 
    public void SpawnObjectAtStart(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            RandomX = Random.Range(0, Screen.width * 2);
            RandomY = Random.Range(0, Screen.height * 2);
            PositionSpawn = new Vector2(RandomX, RandomY);
            Vector2 Pos = MainCam.ScreenToWorldPoint(PositionSpawn);
            Collider2D[] RayData = Physics2D.OverlapCircleAll(Pos, RayRadius);
            if (RayData.Length <= 0 )
            {
                Debug.Log(RayData.Length);
                GameObject var = Instantiate(GrapplePoint, Pos, Quaternion.identity, transform);
                grappler.PoolObj(var);
            }
            else
            {
                amount += 1;
            }
        }
        
    }
    public void SpawnObjects(int Height, int Width,int amount)
    {
        BaseReq += 1;

        for (int i = 0; i < amount; i++)
        {
            RandomX = Random.Range(Screen.width, Screen.width * Width);
            RandomY = Random.Range(Screen.height, Screen.height * Height);
            PositionSpawn = new Vector2(RandomX, RandomY);
            Vector2 Pos = MainCam.ScreenToWorldPoint(PositionSpawn);
            Collider2D[] RayData = Physics2D.OverlapCircleAll(Pos, RayRadius);
            if (Random.value < 0.3f && allowEnemy)
            {
                GameObject var = Instantiate(EnemySingle, Pos, Quaternion.identity, transform);
                allowEnemy = false;
            }
            if (RayData.Length <= 0)
            {
                GameObject var = Instantiate(GrapplePoint, Pos, Quaternion.identity, transform);
                grappler.PoolObj(var);

            }
        }
       
        Debug.Log("Called");
        allowEnemy = true;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RayRadius);

    }

}
