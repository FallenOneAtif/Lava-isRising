using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappler : MonoBehaviour
{
    public InputActionAsset inputActions;
    public InputAction GrappleAction;
    public LineRenderer GrappleLine;
    public Rigidbody2D Rb;
    public DistanceJoint2D lineDistance;
    public Camera MainCam;
    private void Awake()
    {
        GrappleAction = inputActions.actionMaps[0].actions[0];
        GrappleAction.started += ctx => GrappleActivate();
        GrappleAction.canceled += ctx => GrappleDeactivate();
    }
    private void Start()
    {
        GrappleLine = GetComponent<LineRenderer>();
        Rb = GetComponent<Rigidbody2D>();
        lineDistance = GetComponent<DistanceJoint2D>();

        lineDistance.enabled = false;
        GrappleLine.enabled = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (lineDistance.enabled)
        {
            GrappleLine.SetPosition(1, transform.position);
        }
    }
    public void GrappleActivate()
    {
        Vector2 MousePos = Mouse.current.position.ReadValue();
        RaycastHit2D RayData = Physics2D.Raycast(MainCam.ScreenToWorldPoint(MousePos), Vector2.zero);
        if (RayData.collider == null)
        {
            return;
        }
        if (RayData.collider.CompareTag("GrapplePoint"))
        {

            lineDistance.connectedAnchor = (Vector2)RayData.transform.position;
            GrappleLine.SetPosition(0, (Vector2)RayData.transform.position);
            GrappleLine.SetPosition(1, transform.position);
            GrappleLine.enabled = true;
            lineDistance.enabled = true;
            Debug.Log(Rb.angularVelocity);
        }
        
    }
    public void GrappleDeactivate()
    {
        GrappleLine.enabled = false;
        lineDistance.enabled = false;
    }
    public void OvertimeForce(Transform ForcePoint)
    {
        
    }
}
