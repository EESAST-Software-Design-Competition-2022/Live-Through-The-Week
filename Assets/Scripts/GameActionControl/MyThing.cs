using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyThing : MonoBehaviour
{

    public bool inVision;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        inVision = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        inVision = true;
    }

    private void OnBecameInvisible()
    {
        inVision = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if(inVision)
        {
            if (other.tag.Equals("Player"))
            {
                UIManager.instance.showHint(SourceManager.instance.thingsList[id].name,1);
            } 
            if(Input.GetKeyDown(KeyCode.E)&& other.tag.Equals("Player"))
            {
                inVision = false;
                GameManager.instance.player.thingsList.Add(id);
                UIManager.instance.showHint(SourceManager.instance.thingsList[id].name, 0);
                Destroy(this.gameObject);
            }
        }
        else
        {
            UIManager.instance.showHint(SourceManager.instance.thingsList[id].name, 0);
        }   
    }
    public void OnTriggerExit(Collider other)
    {
        UIManager.instance.showHint(SourceManager.instance.thingsList[id].name, 0);
    }
}
