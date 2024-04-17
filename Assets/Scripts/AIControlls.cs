using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControlls : MonoBehaviour
{

    public float movementSpeed;
    public float turningSpeed;
    public float turretTurningSpeed;
    public float fireCooldown;
    public float detectRange;
    public float stoppingRange;
    public float switchTargetDistance;
    public float switchDistance;

    public float AIDelay;


    public Transform turret;
    public Transform muzzle;
    public GameObject projectile;

    public string stringState;

    private Rigidbody rb;
    private float t;
    private float AIt;

    private GameObject targetObject;
    private Vector3 target;

    private int obstacleMask;

    private enum State { forward, left, right, back, stop };
    private State state;
    private State nextState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = 0f;
        AIt = 0f;
        obstacleMask = LayerMask.GetMask("Obstacle");
        state = State.forward;
        nextState = State.forward;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentRotation = rb.rotation.eulerAngles;
        rb.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);



        // etsit‰‰n pelaaja
        if (targetObject != null)
        {
            if(Vector3.Distance(transform.position, targetObject.transform.position) < detectRange)
            {
                if (!Physics.Linecast(transform.position, targetObject.transform.position))
                {
                    target = targetObject.transform.position;

                    if (Vector3.Distance(target, transform.position) < stoppingRange)
                    {
                        nextState = State.stop;
                    }
                }
                
            }
            target = targetObject.transform.position;

        }
        else
        {
            targetObject = GameObject.FindGameObjectWithTag("Player");
        }


        float angle = Vector3.SignedAngle(transform.forward, target - transform.position, Vector3.up);

        if(AIt < 0)
        {
            state = nextState;
            AIt = AIDelay;
        }
        else
        {
            AIt -= Time.deltaTime;
        }



        // Tilakone
        if (state == State.forward)
        {
            stringState = "Forward";

            if (angle < 0) Turning(-1f);
            else if (angle > 0) Turning(1f);

            if (Mathf.Abs(angle) < 90)
            {
                Move(1f);
            }
        }
        if (state == State.left)
        {
            stringState = "Left";
            Turning(-1f);
            Move(1f);

        }
        if (state == State.right)
        {
            stringState = "Right";
            Turning(1f);
            Move(1f);

        }
        if (state == State.back)
        {
            stringState = "Back";
            Move(-1f);
            nextState = State.forward;
        }
        if (state == State.stop)
        {
            stringState = "Stop";
            Move(0f);
            nextState = State.forward;
        }

    }


    // liikkuminen
    private void Move(float input)
    {
        if (input != 0)
        {
            Vector3 movement = transform.forward * input * movementSpeed;
            rb.velocity = movement;
        }
    }

    private void Turning(float input)
    {
        // k‰‰ntyminen
        if (input != 0)
        {
            Vector3 turning = Vector3.up * input * turningSpeed;
            rb.angularVelocity = turning;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Obstacle") && !other.gameObject.CompareTag("Wall"))
        {
            return;
        }

        RaycastHit leftHit;
        RaycastHit rightHit;

        float leftLength = 0f;
        float rightLength = 0f;

        if (Physics.Raycast(transform.position, transform.forward + transform.right * -1, out leftHit, Mathf.Infinity, obstacleMask))
        {
            leftLength = leftHit.distance;
        }
        if (Physics.Raycast(transform.position, transform.forward + transform.right, out rightHit, Mathf.Infinity, obstacleMask))
        {
            rightLength = rightHit.distance;
        }

        if(leftLength > rightLength)
        {
            state = State.left;
            target = leftHit.point;
        }
        else
        {
            state = State.right;
            target = rightHit.point;

        }
    }


    private void OnTriggerExit(Collider other)
    {
        nextState = State.forward;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Obstacle") && !collision.gameObject.CompareTag("Obstacle"))
        {
            return;
        }
        state = State.back;
    }
}
