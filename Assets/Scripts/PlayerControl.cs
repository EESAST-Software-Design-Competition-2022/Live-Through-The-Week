using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Rotatespeed = 5f;
    private Rigidbody rb;
    public float speed;
    public Vector3 forceY = new Vector3(0, 0, 32);
    public Vector3 forceX = new Vector3(28, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1, 0);
    }

    private void FixedUpdate()
    {
        float X = Input.GetAxis("Mouse X") * Rotatespeed;
        //Debug.Log(X / Rotatespeed);
        //float Y = Input.GetAxis("Mouse Y") * Rotatespeed;
        //transform.localRotation = transform.localRotation * Quaternion.Euler(-Y, 0, 0);
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, X, 0);

        float axisY = Input.GetAxis("Vertical");
        float axisX = Input.GetAxis("Horizontal");
        float cos = Mathf.Cos(-transform.eulerAngles.y * 3.14f / 180);
        float sin = Mathf.Sin(-transform.eulerAngles.y * 3.14f / 180);
        //Debug.Log(rb.velocity);
        if (Mathf.Abs(axisY) >= 0.02)
        {

            if (Mathf.Abs(rb.velocity.x * sin - rb.velocity.z * cos) < 2)
            //if(Mathf.Abs(rb.velocity.x) < 2 && Mathf.Abs(rb.velocity.z) < 2)
            {
                //rb.AddRelativeForce(22*transform.forward * axisY, ForceMode.Force);
                rb.AddRelativeForce(forceY * axisY, ForceMode.Force);
            }
        }
        if (Mathf.Abs(axisX) >= 0.02)
        {
            
            if (Mathf.Abs(rb.velocity.x*cos + rb.velocity.z *sin) < 1.5)
            //if (Mathf.Abs(rb.velocity.x) < 2 && Mathf.Abs(rb.velocity.z) < 2)
            {
                //rb.AddRelativeForce(22*transform.right* axisX, ForceMode.Force);
                rb.AddRelativeForce(forceX * axisX, ForceMode.Force);
            }
        }
        //Debug.Log(rb.velocity);
    }
}
