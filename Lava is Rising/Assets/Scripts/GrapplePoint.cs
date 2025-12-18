using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public bool isDestructible;
    public float DragForce;
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.tag == "Player" && isDestructible)
        {
            if (info.gameObject.GetComponent<Grappler>().GrapplePoint.gameObject == this.gameObject)
            {
                info.gameObject.GetComponent<Grappler>().GrappleDeactivate();
            }
            info.gameObject.GetComponent<Rigidbody2D>().linearVelocity *= DragForce;
            Destroy(this.gameObject);
        }
    }

}
