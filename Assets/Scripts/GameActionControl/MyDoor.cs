using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).GetComponent<Rigidbody>().centerOfMass = transform.GetChild(2).localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.name.Equals("Player"))
        {
            open();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.name.Equals("Player"))
        {
            Debug.Log("open");
            open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Invoke("close", 2f);
    }

    public void open()
    {
        //float force = 10000f;
        //Rigidbody rigidbody = transform.GetChild(1).GetComponent<Rigidbody>();
        //rigidbody.AddRelativeTorque(0, +force, 0,ForceMode.VelocityChange);
        HingeJoint joint = transform.GetChild (1).GetComponent<HingeJoint>();
        var motor = joint.motor;
        motor.force = 100;
        motor.targetVelocity = 90;
        motor.freeSpin = false;
        joint.motor = motor;
        joint.useMotor = true;
        //transform.localEulerAngles = new Vector3(0,-90f,0);
    }

    public void close()
    {
        //float force = 10000f;
        //Rigidbody rigidbody = transform.GetChild(1).GetComponent<Rigidbody>();
        //rigidbody.AddRelativeTorque(0, force, 0);
        HingeJoint joint = transform.GetChild(1).GetComponent<HingeJoint>();
        var motor = joint.motor;
        motor.force = 100;
        motor.targetVelocity = -90;
        motor.freeSpin = false;
        joint.motor = motor;
        joint.useMotor = true;
        //transform.localEulerAngles = new Vector3(0, +90f, 0);
    }
}
