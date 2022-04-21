using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraControl : MonoBehaviour
{
    public GameObject target;
    private float Rotatespeed = 5f;
    private Vector3 distance;

    public static NewCameraControl instance = null;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.localPosition - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //float X = Input.GetAxis("Mouse X") * Rotatespeed;
        //transform.localRotation = transform.localRotation * Quaternion.Euler(0, X, 0);
        float Y = Input.GetAxis("Mouse Y") * Rotatespeed;
        //Debug.Log(Y / Rotatespeed);
        //Debug.Log(transform.localEulerAngles.x);
        if(transform.localEulerAngles.x > 40 && transform.localEulerAngles.x < 90)
        {
            if(Y > 0)
            {
                transform.localRotation = transform.localRotation * Quaternion.Euler(-Y, 0, 0);
            }
        }
        else if (transform.localEulerAngles.x < 350 && transform.localEulerAngles.x > 90)
        {
            if(Y < 0)
            {
                transform.localRotation = transform.localRotation * Quaternion.Euler(-Y, 0, 0);
            }
        }
        else
        {
            transform.localRotation = transform.localRotation * Quaternion.Euler(-Y, 0, 0);
        }
        
        //Debug.Log("Quaternion.Euler:"+Quaternion.Euler(-Y, 0, 0));
        //Debug.Log("transform.localRotation:" + transform.localRotation);
        Vector3 angle = transform.localEulerAngles;
        angle.y = target.transform.localEulerAngles.y;
        angle.z = 0;
        transform.localEulerAngles = angle;
        updateDistance();
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
}
