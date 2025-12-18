using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(0, Player.transform.position.y, -10);
    }
}
