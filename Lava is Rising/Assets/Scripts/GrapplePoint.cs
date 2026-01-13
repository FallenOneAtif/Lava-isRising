using System.Collections;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public bool isDestructible;
    public float DragForce;
    public Animator anim;
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Player.GetComponent<Grappler>().PoolObj(this.gameObject);
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.tag == "Player" && isDestructible)
        {
            WaitForAnim(info);
        }
        if (info.tag == "Death")
        {
            anim.enabled = true;
            transform.tag = "Serial";
        }
    }

    void WaitForAnim(Collider2D info)
    {
        if (info.gameObject.GetComponent<Grappler>().GrapplePoint.gameObject == this.gameObject)
        {
            info.gameObject.GetComponent<Grappler>().GrappleDeactivate();
        }
        anim.enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = false;
        transform.tag = "Serial";
    }
    public void Destroy()
    {
        this.gameObject.SetActive(false);
    }
}
