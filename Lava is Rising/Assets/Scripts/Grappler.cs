using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
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
    public Transform GunPoint;
    Vector2 DirectionToMove;
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
        Vector2 Mouse1 = Mouse.current.position.ReadValue();
        Vector2 MousePos = MainCam.ScreenToWorldPoint(Mouse1);
        Vector2 Direction = MousePos - Rb.position;

        float rot = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        Rb.rotation = rot;

        if (lineDistance.enabled)
        {
            GrappleLine.SetPosition(1, GunPoint.position);
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
            GrappleLine.SetPosition(1, GunPoint.position);
            GrappleLine.enabled = true;
            lineDistance.enabled = true;
            Debug.Log(Rb.linearVelocity);
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
