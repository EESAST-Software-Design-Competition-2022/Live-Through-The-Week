using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayer : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public Vector3 forceY = new Vector3(0,0,32);
    public Vector3 forceX = new Vector3(0,4,0);
    public static MyPlayer instance;
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
    //public Text textDoor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,-1,0);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.W))
        {
            rb.AddRelativeForce(0,0,speed,ForceMode.Force);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            rb.AddRelativeForce(0, 0, speed, ForceMode.Force);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            rb.AddRelativeTorque(0, speed, 0);
        }*/
    }

    /*
    private void FixedUpdate()
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
                rb.angularVelocity = forceX * axisX;

            }
        }
        //updateAngle();
    }
    */

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

    private void OnTriggerEnter(Collider other)
    {
        //textDoor.GetComponent<CanvasGroup>().alpha = 1;
        if(other.tag.Equals("Door"))
        {
            UIManager.instance.showHint(UIManager.HintID.DOOR,1);
        }
        else if(other.tag.Equals("Learn"))
        {
            UIManager.instance.showHint(UIManager.HintID.LEARN,1);
        }
        else if (other.tag.Equals("NPC"))
        {
            UIManager.instance.showHint(UIManager.HintID.NPC,1);
        }
        else if (other.tag.Equals("Sleep"))
        {
            UIManager.instance.showHint(UIManager.HintID.SLEEP,1);
        }
        else if (other.tag.Equals("Amusement"))
        {
            UIManager.instance.showHint(UIManager.HintID.AMUSEMENT,1);
        }
        else if (other.tag.Equals("Sport"))
        {
            UIManager.instance.showHint(UIManager.HintID.SPORT, 1);
        }
        else if (other.tag.Equals("CabDoor"))
        {
            UIManager.instance.showHint(UIManager.HintID.CABDOOR, 1);
        }
        else if (other.tag.Equals("Transform"))
        {
            UIManager.instance.showHint(UIManager.HintID.TRANSFORM, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Door"))
        {
            UIManager.instance.showHint(UIManager.HintID.DOOR,0);
        }
        else if (other.tag.Equals("Learn"))
        {
            UIManager.instance.showHint(UIManager.HintID.LEARN,0);
        }
        else if (other.tag.Equals("NPC"))
        {
            UIManager.instance.showHint(UIManager.HintID.NPC,0);
        }
        else if (other.tag.Equals("Sleep"))
        {
            UIManager.instance.showHint(UIManager.HintID.SLEEP,0);
        }
        else if (other.tag.Equals("Amusement"))
        {
            UIManager.instance.showHint(UIManager.HintID.AMUSEMENT,0);
        }
        else if (other.tag.Equals("Sport"))
        {
            UIManager.instance.showHint(UIManager.HintID.SPORT, 0);
        }
        else if (other.tag.Equals("CabDoor"))
        {
            UIManager.instance.showHint(UIManager.HintID.CABDOOR, 0);
        }
        else if (other.tag.Equals("Transform"))
        {
            UIManager.instance.showHint(UIManager.HintID.TRANSFORM, 0);
        }
    }


}
