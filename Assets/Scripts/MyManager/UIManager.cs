using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance { get; private set; }
    public Canvas canvas;
    public Canvas activityCanvas;
    public float gameTimeScale = 30;
    private int day;
    private int hour;
    private int minute;

    private float LoadStartTime;
    public LoadState gameState;
    public string wordScene;
    public int[] trueTime;
    public string[] className = new string[5] { "电磁场", "模电", "电路原理", "复变函数", "大学物理" };

    public enum LoadState { LEARN, SLEEP, WORD, FISH, SPORT };
    public int fishType;
    public int sportType;

    public Material[] skyLight;

    public int currentBagPage = 0;
    public enum HintID { LEARN, DOOR, NPC, SLEEP, AMUSEMENT, SPORT ,CABDOOR,TRANSFORM};
    //private bool isShow;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance = this;
        //updateAllAbility(new int[5]{ 5,5,5,5,5});
        LoadStartTime = -1f;
        trueTime = new int[3];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showHint(HintID ID, int alpha)
    {
        switch (ID)
        {
            case HintID.LEARN:
                canvas.transform.GetChild(0).GetChild(0).GetComponent<CanvasGroup>().alpha = alpha;
                break;
            case HintID.DOOR:
                canvas.transform.GetChild(0).GetChild(1).GetComponent<CanvasGroup>().alpha = alpha;
                break;
            case HintID.NPC:
                canvas.transform.GetChild(0).GetChild(2).GetComponent<CanvasGroup>().alpha = alpha;
                break;
            case HintID.SLEEP:
                canvas.transform.GetChild(0).GetChild(3).GetComponent<CanvasGroup>().alpha = alpha;
                break;
            case HintID.AMUSEMENT:
                canvas.transform.GetChild(0).GetChild(4).GetComponent<CanvasGroup>().alpha = alpha;
                break;
            case HintID.SPORT:
                canvas.transform.GetChild(0).GetChild(5).GetComponent<CanvasGroup>().alpha = alpha;
                break;
            case HintID.CABDOOR:
                canvas.transform.GetChild(0).GetChild(7).GetComponent<CanvasGroup>().alpha = alpha;
                break;
            case HintID.TRANSFORM:
                canvas.transform.GetChild(0).GetChild(8).GetComponent<CanvasGroup>().alpha = alpha;
                break;
        }
    }
    public void showHint(string name, int alpha)
    {
        GameObject gameObject = canvas.transform.GetChild(0).Find("ThingHint").gameObject;
        gameObject.GetComponent<Text>().text = "  [E]	按E键拾取" + name;
        gameObject.GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void updateStatus(String status)
    {
        Text text = canvas.transform.GetChild(2).GetComponent<Text>();
        text.text = status;
    }

    /*
    public void dishowHint(HintID ID)
    {
        switch (ID)
        {
            case HintID.LEARN:
                canvas.transform.GetChild(0).GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                break;
            case HintID.DOOR:
                canvas.transform.GetChild(0).GetChild(1).GetComponent<CanvasGroup>().alpha = 0;
                break;
            case HintID.NPC:
                canvas.transform.GetChild(0).GetChild(2).GetComponent<CanvasGroup>().alpha = 0;
                break;
            case HintID.SLEEP:
                canvas.transform.GetChild(0).GetChild(3).GetComponent<CanvasGroup>().alpha = 0;
                break;
            case HintID.AMUSEMENT:
                canvas.transform.GetChild(0).GetChild(4).GetComponent<CanvasGroup>().alpha = 0;
                break;
        }
    }
    */

    public void showAllAbility(int alpha)
    {
        canvas.transform.GetChild(3).GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void updateAllAbility(int[] ability)
    {
        if (ability.Length != 5)
        {
            return;
        }
        String allAbility = "";
        int i = 0;
        for (; i < GameManager.instance.examIndex; ++i)
        {
            allAbility += GameManager.instance.examList[i].name + ":已结束\n";
        }
        for (; i < 5; ++i)
        {
            allAbility += GameManager.instance.examList[i].getQInfo() + "……" + ability[i] + "\n";
        }
        allAbility.Remove(allAbility.Length - 1);
        /*
            "心电场：" + getLine(ability[0]) +
            "\n模拟电：" + getLine(ability[1]) +
            "\n高电分：" + getLine(ability[2]) +
            "\n复函数：" + getLine(ability[3]) +
            "\n大带物：" + getLine(ability[4]);
        */
        canvas.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = allAbility;
    }

    private String getLine(int i)
    {
        if (i <= 0)
        {
            return null;
        }
        else
        {
            i /= 10;
            String line = "";
            for (int j = 0; j < i; ++j)
            {
                line = line + "▊";
            }
            for (int j = 0; j < 10 - i; ++j)
            {
                line = line + "  ";
            }
            return line;
        }
    }

    /// <summary>
    /// 获取GameManager中的时间，更新时间
    /// </summary>
    public void updateTime()
    {
        hour = GameManager.instance.gameTime[1];
        minute = GameManager.instance.gameTime[2];
        day = GameManager.instance.gameTime[0];
        int time = (int)(gameTimeScale * (Time.time - GameManager.instance.checkTime));//seconds
        //Debug.Log(time);
        time = time / 60;//minutes
        minute += time;
        hour += minute / 60;
        minute %= 60;
        day += hour / 24;
        hour %= 24;
        trueTime[0] = day;
        trueTime[1] = hour;
        trueTime[2] = minute;
        if (minute < 10)
        {
            String timeNow = hour + ":0" + minute;
            //Debug.Log(timeNow);
            canvas.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = timeNow;
            canvas.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "Day" + day;
        }
        else
        {
            String timeNow = hour + ":" + minute;
            //Debug.Log(timeNow);
            canvas.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = timeNow;
            canvas.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "Day" + day;
        }
        GameManager.instance.checkEventTime(new int[3] { day, hour, minute });
        GameManager.instance.checkStayedEvent(new int[3] { day, hour, minute });
        GameManager.instance.checkExam(new int[3] { day, hour, minute });
        //GameManager.instance.checkOver();
        if(hour >=6 && hour < 17 && RenderSettings.skybox != skyLight[0])
        {
            RenderSettings.skybox = skyLight[0];
        }
        else if(hour >= 17 && hour <= 18 && RenderSettings.skybox != skyLight[1])
        {
            RenderSettings.skybox = skyLight[1];
        }
        else if(hour > 18 && hour <= 20 && RenderSettings.skybox != skyLight[2])
        {
            RenderSettings.skybox = skyLight[2];
        }
        else if(hour > 20 || hour < 5 && RenderSettings.skybox != skyLight[3])
        {
            RenderSettings.skybox = skyLight[3];
        }
        else if(hour == 5 && RenderSettings.skybox != skyLight[4])
        {
            RenderSettings.skybox = skyLight[4];
        }

        
    }

    public int[] getTimeNow()
    {
        return new int[3] { day, hour, minute };
    }

    public void updateRandomEvent(RandomEvent randomEvent)
    {
        string text = randomEvent.getUIInfo();
        canvas.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = text;
        canvas.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = randomEvent.eventPic;
    }

    public void showRandomEvent(int alpha)
    {
        canvas.transform.GetChild(5).GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void showDialog(int alpha)
    {
        canvas.transform.GetChild(6).GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void updateDialog(string name, string word)
    {
        canvas.transform.GetChild(6).GetChild(1).GetComponent<Text>().text = name + "\n----------------";
        canvas.transform.GetChild(6).GetChild(2).GetComponent<Text>().text = word;
    }

    public void showLearnUI(int alpha)
    {
        activityCanvas.transform.Find("Learn").GetComponent<CanvasGroup>().alpha = alpha;
        if (alpha == 1)
        {
            activityCanvas.transform.Find("Learn").GetComponent<CanvasGroup>().interactable = true;
            activityCanvas.transform.Find("Learn").SetAsLastSibling();
        }
        else
        {
            activityCanvas.transform.Find("Learn").GetComponent<CanvasGroup>().interactable = false;
            activityCanvas.transform.Find("Learn").SetAsFirstSibling();
        }

    }

    public float getLearnTime()
    {
        return (activityCanvas.transform.Find("Learn").Find("Time").GetComponent<Dropdown>().value + 1) * 0.5f;
    }

    public int getLearnSubject()
    {
        return (activityCanvas.transform.Find("Learn").Find("Subject").GetComponent<Dropdown>().value);
    }

    public void showSleepUI(int alpha)
    {
        activityCanvas.transform.Find("Sleep").GetComponent<CanvasGroup>().alpha = alpha;
        if (alpha == 1)
        {
            activityCanvas.transform.Find("Sleep").GetComponent<CanvasGroup>().interactable = true;
            activityCanvas.transform.Find("Sleep").SetAsLastSibling();
        }
        else
        {
            activityCanvas.transform.Find("Sleep").GetComponent<CanvasGroup>().interactable = false;
            activityCanvas.transform.Find("Sleep").SetAsFirstSibling();
        }
    }

    public int[] getSleepTime()
    {
        int hour;
        int minute;
        try
        {
            hour = int.Parse(activityCanvas.transform.Find("Sleep").Find("hours").GetComponent<InputField>().text);
            minute = int.Parse(activityCanvas.transform.Find("Sleep").Find("minutes").GetComponent<InputField>().text);
        } catch (Exception)
        {
            hour = -1;
            minute = -1;
        }
        return new int[2] { hour, minute };
    }

    public void showFishUI(int alpha)
    {
        activityCanvas.transform.Find("Fish").GetComponent<CanvasGroup>().alpha = alpha;
        if (alpha == 1)
        {
            activityCanvas.transform.Find("Fish").GetComponent<CanvasGroup>().interactable = true;
            activityCanvas.transform.Find("Fish").SetAsLastSibling();
        }
        else
        {
            activityCanvas.transform.Find("Fish").GetComponent<CanvasGroup>().interactable = false;
            activityCanvas.transform.Find("Fish").SetAsFirstSibling();
        }
    }

    public int[] getFishTime()
    {
        int hour;
        int minute;
        try
        {
            hour = int.Parse(activityCanvas.transform.Find("Fish").Find("hours1").GetComponent<InputField>().text);
            minute = int.Parse(activityCanvas.transform.Find("Fish").Find("minutes1").GetComponent<InputField>().text);
        }
        catch (Exception)
        {
            hour = -1;
            minute = -1;
        }
        return new int[2] { hour, minute };
    }

    public int getFishType()
    {
        return (activityCanvas.transform.Find("Fish").Find("Type").GetComponent<Dropdown>().value);
    }

    public void showSportUI(int alpha)
    {
        activityCanvas.transform.Find("Sport").GetComponent<CanvasGroup>().alpha = alpha;
        if (alpha == 1)
        {
            activityCanvas.transform.Find("Sport").GetComponent<CanvasGroup>().interactable = true;
            activityCanvas.transform.Find("Sport").SetAsLastSibling();
        }
        else
        {
            activityCanvas.transform.Find("Sport").GetComponent<CanvasGroup>().interactable = false;
            activityCanvas.transform.Find("Sport").SetAsFirstSibling();
        }
    }

    public int[] getSportTime()
    {
        int hour;
        int minute;
        try
        {
            hour = int.Parse(activityCanvas.transform.Find("Sport").Find("hours1").GetComponent<InputField>().text);
            minute = int.Parse(activityCanvas.transform.Find("Sport").Find("minutes1").GetComponent<InputField>().text);
        }
        catch (Exception)
        {
            hour = -1;
            minute = -1;
        }
        return new int[2] { hour, minute };
    }

    public int getSportType()
    {
        return (activityCanvas.transform.Find("Sport").Find("Type").GetComponent<Dropdown>().value);
    }

    public void showLoadUI()
    {
        if (LoadStartTime < 0)
        {
            LoadStartTime = Time.time;
        }
        GameObject gameObject = null;
        if (UIManager.instance.gameState == UIManager.LoadState.LEARN)
        {
            gameObject = activityCanvas.transform.Find("LearnLoad").gameObject;
        }
        else if (UIManager.instance.gameState == UIManager.LoadState.SLEEP)
        {
            gameObject = activityCanvas.transform.Find("SleepLoad").gameObject;
        }
        else if (UIManager.instance.gameState == UIManager.LoadState.FISH)
        {
            if (UIManager.instance.fishType <= 4 && UIManager.instance.fishType >= 0)
            {
                gameObject = activityCanvas.transform.Find("FishLoad").GetChild(UIManager.instance.fishType).gameObject;
            }
            else
            {
                gameObject = null;
            }

        }
        else if (UIManager.instance.gameState == UIManager.LoadState.SPORT)
        {
            if (UIManager.instance.sportType <= 4 && UIManager.instance.sportType >= 0)
            {
                gameObject = activityCanvas.transform.Find("SportLoad").GetChild(UIManager.instance.sportType).gameObject;
            }
            else
            {
                gameObject = null;
            }

        }
        else if (UIManager.instance.gameState == UIManager.LoadState.WORD)
        {
            gameObject = activityCanvas.transform.Find("WordLoad").gameObject;
            gameObject.transform.Find("Word").GetComponent<Text>().text = UIManager.instance.wordScene;
        }
        if (gameObject == null)
        {
            GameManager.instance.gameState = GAMESTATE.ON;
            LoadStartTime = -1f;
            return;
        }
        if (Time.time - LoadStartTime <= 1)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = Time.time - LoadStartTime;
        }
        if (Time.time - LoadStartTime <= 2)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 2 - Time.time + LoadStartTime;
        }
        if (Time.time - LoadStartTime > 2)
        {
            activityCanvas.transform.Find("LearnLoad").gameObject.GetComponent<CanvasGroup>().alpha = 0;
            activityCanvas.transform.Find("SleepLoad").gameObject.GetComponent<CanvasGroup>().alpha = 0;
            activityCanvas.transform.Find("WordLoad").gameObject.GetComponent<CanvasGroup>().alpha = 0;
            if (UIManager.instance.wordScene.Contains("\r"))
            {
                gameObject = activityCanvas.transform.Find("WordLoad").gameObject;
                gameObject.transform.Find("Word").GetComponent<Text>().text = UIManager.instance.wordScene + "\nThe Game is Over\nThanks for playing";
                gameObject.GetComponent<CanvasGroup>().alpha = 1;
                GameManager.instance.gameState = GAMESTATE.OFF;
                LoadStartTime = -1f;
                Time.timeScale = 0;
                return;
            }
            GameManager.instance.gameState = GAMESTATE.ON;
            LoadStartTime = -1f;
        }
    }

    public void updateExamInfo(string text)
    {
        UIManager.instance.activityCanvas.transform.Find("GoExam").Find("NextExamInfo").gameObject.GetComponent<Text>().text = text;
    }

    public void showGoExamBtn(int alpha)
    {
        if (alpha == 0)
        {
            UIManager.instance.activityCanvas.transform.Find("GoExam").transform.SetAsFirstSibling();
            UIManager.instance.activityCanvas.transform.Find("GoExam").Find("GoExam").gameObject.GetComponent<Button>().interactable = false;
        } else if (alpha == 1)
        {
            UIManager.instance.activityCanvas.transform.Find("GoExam").transform.SetAsLastSibling();
            UIManager.instance.activityCanvas.transform.Find("GoExam").Find("GoExam").gameObject.GetComponent<Button>().interactable = true;
        }
        //UIManager.instance.activityCanvas.transform.Find("GoExam").Find("GoExam").gameObject.GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void showBag()
    {
        GameObject bag = activityCanvas.transform.Find("Bag").gameObject;
        float alphaNow = bag.GetComponent<CanvasGroup>().alpha;
        if (alphaNow == 0)
        {
            bag.transform.SetAsLastSibling();
            updateBagDescribe("");
            updateBagName("");
            bag.GetComponent<CanvasGroup>().alpha = 1;
            for (int i = 0; i < 12; ++i)
            {
                bag.transform.GetChild(0).GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            }
            int j = GameManager.instance.player.thingsList.Count <= 12 ? GameManager.instance.player.thingsList.Count : 12;
            for (int i = 0; i < j; ++i)
            {
                bag.transform.GetChild(0).GetChild(i).gameObject.GetComponent<Image>().sprite = SourceManager.instance.getSprite(GameManager.instance.player.thingsList[i] + 1);
            }
            bag.GetComponent<MyBag>().currentPage = 0;
            currentBagPage = 0;
        }
        else
        {
            bag.transform.SetAsFirstSibling();
            activityCanvas.transform.Find("Bag").GetComponent<CanvasGroup>().alpha = 0;
        }
    }
    public void nextBagPage()
    {
        if (GameManager.instance.player.thingsList.Count > (currentBagPage + 1) * 12)
        {
            currentBagPage += 1;
            showBag(currentBagPage);
        }
    }

    public void FormerBagPage()
    {
        if (currentBagPage >= 1)
        {
            --currentBagPage;
            showBag(currentBagPage);
        }
    }

    public void showBag(int k)
    {
        GameObject bag = activityCanvas.transform.Find("Bag").gameObject;
        float alphaNow = bag.GetComponent<CanvasGroup>().alpha;
        if (true)
        {
            bag.transform.SetAsLastSibling();
            updateBagDescribe("");
            updateBagName("");
            bag.GetComponent<CanvasGroup>().alpha = 1;
            for (int i = 0; i < 12; ++i)
            {
                bag.transform.GetChild(0).GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            }
            int j = GameManager.instance.player.thingsList.Count <= 12 * (k + 1) ? GameManager.instance.player.thingsList.Count : 12 * (k + 1);
            for (int i = 12 * k; i < j; ++i)
            {
                bag.transform.GetChild(0).GetChild(i % 12).gameObject.GetComponent<Image>().sprite = SourceManager.instance.getSprite(GameManager.instance.player.thingsList[i] + 1);
            }
        }
        /*
        else
        {
            bag.transform.SetAsFirstSibling();
            activityCanvas.transform.Find("Bag").GetComponent<CanvasGroup>().alpha = 0;
        }
        */
    }

    public void updateBagDescribe(string describe)
    {
        activityCanvas.transform.Find("Bag").Find("TextImage").Find("Describe").GetComponent<Text>().text = describe;
    }
    public void updateBagName(string describe)
    {
        activityCanvas.transform.Find("Bag").Find("TextImage").Find("Name").GetComponent<Text>().text = describe;
    }

    public void updateMessage(string message)
    {
        GameObject target = this.canvas.transform.Find("MessageBox").gameObject;
        target.transform.Find("Text").GetComponent<Text>().text = "  "+message;
        if(IsInvoking("dishowMessage"))
        {
            CancelInvoke("dishowMessage");
        }
        if (IsInvoking("dishowMessageSlow"))
        {
            CancelInvoke("dishowMessageSlow");
        }
        target.GetComponent<CanvasGroup>().alpha = 1;
        Invoke("dishowMessage", 2f);
    }

    public void dishowMessage()
    {
        //GameObject target = this.canvas.transform.Find("MessageBox").gameObject;
        //target.GetComponent<CanvasGroup>().alpha = 0;
        InvokeRepeating("dishowMessageSlow", 0f, 0.5f);
    }

    public void dishowMessageSlow()
    {

        GameObject target = this.canvas.transform.Find("MessageBox").gameObject;
        if (target.GetComponent<CanvasGroup>().alpha <= 0)
        {
            CancelInvoke("dishowMessageSlow");
            return;
        }
        target.GetComponent<CanvasGroup>().alpha -= 0.2f;
        
    }
}
