using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBagButton : MonoBehaviour
{
    public Button btn;
    void OnClickBtn(GameObject g)
    {
        //Debug.Log(g.name);
        for(int i = 12; i > 0; --i)
        {
            //Debug.Log(i.ToString());
            if (g.name.Contains((i-1).ToString()))
            {
                int j = UIManager.instance.currentBagPage * 12 + i;
                if (GameManager.instance.player.thingsList.Count >= j)
                {
                    UIManager.instance.updateBagDescribe(SourceManager.instance.thingsList[GameManager.instance.player.thingsList[j - 1]].describe);
                    UIManager.instance.updateBagName(SourceManager.instance.thingsList[GameManager.instance.player.thingsList[j - 1]].name);
                    GameObject bag = UIManager.instance.activityCanvas.transform.Find("Bag").gameObject;
                    bag.GetComponent<MyBag>().currentButton = j;
                    bag.transform.Find("TextImage").Find("UseThings").gameObject.GetComponent<Button>().interactable = true;
                }
                else
                {
                    UIManager.instance.updateBagDescribe("");
                    UIManager.instance.updateBagName("");
                    GameObject bag = UIManager.instance.activityCanvas.transform.Find("Bag").gameObject;
                    bag.GetComponent<MyBag>().currentButton = -1;
                    bag.transform.Find("TextImage").Find("UseThings").gameObject.GetComponent<Button>().interactable = false;
                }
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = this.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => { OnClickBtn(btn.gameObject); });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
