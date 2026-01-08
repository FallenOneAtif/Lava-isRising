using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D Rb;
    public float Speed, WithinDistance;
    public bool ToPlayer;
    private GameObject player;
    Vector2 Direction;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ToPlayer)
        {
            transform.position += transform.up * Speed * Time.deltaTime;
        }
        else
        {
            float Distance = Vector2.Distance(transform.position, player.transform.position);
            if (Distance < WithinDistance)
            {
                Direction = player.transform.position - transform.position;
                Rb.linearVelocity = new Vector2(Direction.x, Direction.y) * Speed;
            }
        }
    }
    
}
