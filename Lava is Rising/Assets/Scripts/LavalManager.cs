using UnityEngine;

public class LavalManager : MonoBehaviour
{
    public bool isStatic;
    public float SpeedDamp;
    public GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        if (!isStatic)
        {
            MoveLava();
        }
        else
        {
            transform.position = new Vector3(Player.transform.position.x, transform.position.y, 0);
        }
    }
    public void MoveLava()
    {
        transform.Translate(Vector2.right/SpeedDamp * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.tag == "Player")
        {
            UIBehaviour.instance.EndScreen();
        }
    }
}
