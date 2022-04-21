using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewControl : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public Vector3 forceY;
    public Vector3 forceX;
    public Text textDoor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float axisY = Input.GetAxis("Vertical");
        float axisX = Input.GetAxis("Horizontal");
        if (axisY != 0)
        {
            if (rb.velocity.magnitude < 2)
            {
                rb.AddRelativeForce(forceY * axisY, ForceMode.Force);
            }
        }
        if (Mathf.Abs(axisX) > 0.01)
        {
            //if (rb.velocity.magnitude < 2)
            {
                //rb.AddRelativeTorque(forceX * axisX * Time.fixedDeltaTime, ForceMode.Force);
                rb.AddRelativeForce(forceX * axisX,ForceMode.Force);

            }
        }
        updateAngle();
    }
    private void updateAngle()
    {
        float Rotatespeed = 5f;
        //float angleYNow = transform.localEulerAngles.y - target.transform.eulerAngles.y;
        //if (Mathf.Abs(angleY - angleYNow) > 0.1)
        //{
        //    Vector3 angle = transform.localEulerAngles;
        //    angle.y = angleY + target.transform.eulerAngles.y;
        //    transform.localEulerAngles = angle;
        //    //Debug.Log(player.transform.eulerAngles.y);
        //}
        float X = Input.GetAxis("Mouse X") * Rotatespeed;
        float Y = Input.GetAxis("Mouse Y") * Rotatespeed;
        transform.localRotation = transform.localRotation * Quaternion.Euler(-Y, 0, 0);
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, X, 0);
        Vector3 angle = transform.localEulerAngles;
        angle.z = 0;
        angle.x = 0;
        transform.localEulerAngles = angle;
    }
}
