using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject target;
    private Vector3 distance;
    public float Rotatespeed = 5f;
    private float angleY;
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.localPosition - target.transform.position;
        angleY = transform.localEulerAngles.y - target.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    { 
        updateDistance();
        updateAngle();
    }

    public void FixedUpdate()
    {
        //updateAngle();
    }

    private void updateDistance()
    {
        Vector3 distanceNow = transform.localPosition - target.transform.position;
        if ((distance - distanceNow).magnitude > 0.01)
        {
            distanceNow.y = distance.y;
            float dis = Mathf.Sqrt((distance.x * distance.x) + (distance.z * distance.z));
            distanceNow.x = -dis * Mathf.Sin(target.transform.eulerAngles.y * 3.14f / 180);
            distanceNow.z = -dis * Mathf.Cos(target.transform.eulerAngles.y * 3.14f / 180);
            transform.localPosition = distanceNow + target.transform.position;
        }
    }

    private void updateAngle()
    {
        float angleYNow = transform.localEulerAngles.y - target.transform.eulerAngles.y;
        if (Mathf.Abs(angleY - angleYNow) > 0.1)
        {
            Vector3 angle = transform.localEulerAngles;
            angle.y = angleY + target.transform.eulerAngles.y;
            transform.localEulerAngles = angle;
            //Debug.Log(player.transform.eulerAngles.y);
        }
        //float X = Input.GetAxis("Mouse X") * Rotatespeed;
        //float Y = Input.GetAxis("Mouse Y") * Rotatespeed;
        //transform.localRotation = transform.localRotation * Quaternion.Euler(-Y, 0, 0);
        //transform.localRotation = transform.localRotation * Quaternion.Euler(0, X, 0);
        //Vector3 angle = transform.localEulerAngles;
        //angle.z = 0;
        //transform.localEulerAngles = angle;
    }
}
