using System.Collections;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public bool isDestructible;
    public float DragForce;
    public Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.tag == "Player" && isDestructible)
        {
            WaitForAnim(info);
        }
    }

    void WaitForAnim(Collider2D info)
    {
        if (info.gameObject.GetComponent<Grappler>().GrapplePoint.gameObject == this.gameObject)
        {
            info.gameObject.GetComponent<Grappler>().GrappleDeactivate();
        }
        info.gameObject.GetComponent<Rigidbody2D>().linearVelocity *= DragForce;
        anim.enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = false;
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
