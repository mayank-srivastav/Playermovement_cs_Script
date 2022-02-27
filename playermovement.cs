using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private Rigidbody rb;
    bool isgrounded;
    public float grounddistance;
    public LayerMask groundmask;
    public GameObject grouncheck;
    public float jumpheight;
    public float speed;
    Vector3 move;
    float x;
    float z;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        rb.drag = 10f;
        speed = 0f;
        isgrounded = Physics.CheckSphere(grouncheck.transform.position, grounddistance, groundmask);
        if (isgrounded == true)
        {
            Jump();
        }

        if (isgrounded == false)
        {
            rb.drag = 0f;
        }

        moveme();

        rb.AddForce(move.normalized * speed,ForceMode.Acceleration);
    }

    void moveme()
    {
        speed = 0f;
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            rb.drag = 0f;
            if (isgrounded == true)
            {
                speed = 40f;
            }

            if (isgrounded == false)
            {
                speed = 20f;
            }

            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
        }

        move = transform.right * x + transform.forward * z;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.drag = 0f;
            //rb.velocity = (Vector3.up * jumpheight);
            rb.AddForce(Vector3.up * jumpheight, ForceMode.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(grouncheck.transform.position, grounddistance);
    }
}
