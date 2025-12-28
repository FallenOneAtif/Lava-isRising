using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Drawing;


public class Grappler : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction GrappleAction;

    [SerializeField] private Rope GrapplingRope;
    [SerializeField] private float launchSpeed;
    private Rigidbody2D Rb;
    private SpringJoint2D lineDistance;
    [SerializeField] private Camera MainCam;
    public Transform GunPoint;

    [Header("Physics Calc:")]

    [SerializeField][Range(0, 10)] private float GravityScale, BaseGravity, DampDistance;
    [SerializeField][Range(0, 2)] private float DampScale, BaseDamp, FrequencyDamp;
    public List<GameObject> points, PooledPoints;
    [HideInInspector] public Transform GrapplePoint;
    [HideInInspector] public Vector2 DistanceToPoint;
    public float Torque;
    private void Awake()
    {
        GrappleAction = inputActions.actionMaps[0].actions[0];
        GrappleAction.started += ctx => GrappleActivate();
        GrappleAction.canceled += ctx => GrappleDeactivate();
    }
    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        lineDistance = GetComponent<SpringJoint2D>();
        GrapplingRope.enabled = false;
        lineDistance.enabled = false;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 Mouse1 = Mouse.current.position.ReadValue();
        Vector2 MousePos = MainCam.ScreenToWorldPoint(Mouse1);
        Vector2 Direction = MousePos - Rb.position;

        float rot = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        Rb.rotation = rot;
        Rb.AddTorque(10f);
        if (lineDistance.frequency < 1)
        {
            lineDistance.frequency += FrequencyDamp * Time.deltaTime;
        }
        else
            lineDistance.frequency = 1;
    }

    public void PoolObj(GameObject Obj)
    {
        points.Add(Obj);
    }
    
    public void GrappleActivate()
    {
        
        Vector2 MousePos = Mouse.current.position.ReadValue();
        GameObject Point = NearestObject(MainCam.ScreenToWorldPoint(MousePos));
        if (Point != null)
        {
            GrapplePoint = Point.transform;
            DistanceToPoint = Point.transform.position - transform.position;
            float TotalDistance = DistanceToPoint.magnitude;
            if (TotalDistance > DampDistance)
            {
                DampScale = 1f;
            }
            else
            {
                DampScale = 0.6f;
            }
            GrapplingRope.enabled = true;
        }
        
    }
    public void Grapple()
    {
        if (GrapplePoint.gameObject.activeInHierarchy)
        {
            lineDistance.connectedAnchor = GrapplePoint.transform.position;
            Vector2 Distance = GunPoint.transform.position - transform.position;
            lineDistance.distance = Distance.magnitude;
            Debug.Log(launchSpeed / DistanceToPoint.magnitude);
            lineDistance.frequency = launchSpeed / DistanceToPoint.magnitude;
            Rb.gravityScale = GravityScale;
            Rb.linearDamping = DampScale;
            lineDistance.enabled = true;
        }
        
    }

    public GameObject NearestObject(Vector2 Position)
    {
        GameObject NearestObj = null;
        float MinDistance = Mathf.Infinity;
       
        foreach (var item in points)
        {
            if (item != null)
            {
                float Distance = Vector2.Distance(item.transform.position, Position);
                if (Distance < MinDistance && item.tag == "GrapplePoint")
                {
                    NearestObj = item;
                    MinDistance = Distance;

                }
            }
        }
        if (NearestObj == null)
        {
            return null;
        }
        Debug.Log(NearestObj);

        return NearestObj;
    }
    public void GrappleDeactivate()
    {
        lineDistance.enabled = false;
        Rb.gravityScale = BaseGravity;
        GrapplingRope.enabled = false;
        Rb.linearDamping = BaseDamp;
    }
    

}
