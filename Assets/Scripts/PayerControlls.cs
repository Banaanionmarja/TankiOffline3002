using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerControlls : MonoBehaviour
{


    public float movementSpeed;
    public float turningSpeed;
    public float turretTurningSpeed;
    public float fireCooldown;


    public Transform turret;
    public Transform muzzle;
    public GameObject projectile;

    private Camera mainCamera;
    private Rigidbody rb;
    private float maxRayDist = 100;
    private int floorMask;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        floorMask = LayerMask.GetMask("Floor");
    }

    private void Update()
    {
        if (t <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                GameObject proj = Instantiate(projectile, muzzle.position, muzzle.rotation);
                t = fireCooldown;
                proj.GetComponent<Projectile>().shooterTag = tag;
            }
        }
        else t -= Time.deltaTime;
    }

    // Update is called ~50 times per second
    void FixedUpdate()
    {
        Vector3 currentRotation = rb.rotation.eulerAngles;
        rb.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);

        // k‰ytt‰j‰n nappulanpainelut
        float inptuHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");


        // k‰‰ntyminen
        if (inptuHorizontal != 0)
        {
            Vector3 turning = Vector3.up * inptuHorizontal * turningSpeed;
            rb.angularVelocity = turning;
        }

        // liikkuminen
        if (inputVertical != 0)
        {
            Vector3 movement = transform.forward * inputVertical * movementSpeed;
            rb.velocity = movement;
        }


        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDist, floorMask))
        {
            Vector3 targetDirection = hit.point - turret.position;
            targetDirection.y = 0f;
            Vector3 turningDirection = Vector3.RotateTowards(turret.forward, targetDirection, turretTurningSpeed * Time.deltaTime, 0f);
            turret.rotation = Quaternion.LookRotation(turningDirection);
        }
    }
}
