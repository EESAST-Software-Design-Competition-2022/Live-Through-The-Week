using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNPC : MonoBehaviour
{
    public bool isTrigger;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("Player"))
        {
            isTrigger = true;
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.name.Equals("Player") && GameManager.instance.gameState == GAMESTATE.ON)
        {
            if(SourceManager.instance.NPCList[id].isMulti == 0)
            {
                UIManager.instance.updateDialog(SourceManager.instance.NPCList[id].name, SourceManager.instance.NPCList[id].words[0]);
            }
            else
            {
                UIManager.instance.updateDialog(SourceManager.instance.NPCList[id].name, SourceManager.instance.NPCList[id].wordsColl[UIManager.instance.trueTime[0]-1][0]);
            }
            
            UIManager.instance.showDialog(1);
            GameManager.instance.gameState = GAMESTATE.DIALOGON;
            SourceManager.instance.currentNPC = id;
            UIManager.instance.showHint(UIManager.HintID.NPC,0);
            Time.timeScale = 0;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }


}
