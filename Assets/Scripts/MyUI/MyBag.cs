using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBag : MonoBehaviour
{
    public int currentButton;
    public int currentPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useThings()
    {
        if (currentButton - 1 < 0)
        {
            return;
        }
        Func<bool> action = SourceManager.instance.actions[GameManager.instance.player.thingsList[currentButton - 1]];
        if (action())
        {
            destroyThings();
        }
    }

    public void destroyThings()
    {
        if (currentButton - 1 < 0)
        {
            return;
        }
        else
        {
            GameManager.instance.player.thingsList.RemoveAt(currentButton - 1);
            UIManager.instance.showBag();
            UIManager.instance.showBag();
            this.transform.Find("TextImage").Find("UseThings").gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
