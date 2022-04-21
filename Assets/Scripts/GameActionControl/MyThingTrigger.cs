using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyThingTrigger : MonoBehaviour
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
        if (inVision && (GameManager.instance.playerObject.transform.position - this.transform.position).magnitude <= (this.transform.position - this.transform.GetChild(0).position).magnitude)
        {
            UIManager.instance.showHint(SourceManager.instance.thingsList[id].name, 1);
            if (Input.GetKeyDown(KeyCode.E))
            {
                inVision = false;
                GameManager.instance.player.thingsList.Add(id);
                UIManager.instance.showHint(SourceManager.instance.thingsList[id].name, 0);
                Destroy(this.gameObject);
            }
            else
            {
                UIManager.instance.showHint(SourceManager.instance.thingsList[id].name, 0);
            }
        }
    }

    private void OnBecameVisible()
    {
        inVision = true;
    }

    private void OnBecameInvisible()
    {
        inVision = false;
    }

    
}
