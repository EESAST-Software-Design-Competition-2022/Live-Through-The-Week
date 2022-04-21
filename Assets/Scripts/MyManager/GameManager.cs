using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public Player player;
    public float checkTime;
    public int[] gameTime;
    public string[] className;
    public List<RandomEvent> eventList;
    public List<StayRandomEvent> stayEventList;
    public int eventIndex = 0;
    public GameObject playerObject;

    public int examIndex = 0;
    public GAMESTATE gameState;
    public List<Exam> examList;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);return;
        }
        else
        {
            instance = this;
        }
    }

    //public UIManager UIManager = new UIManager();
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        player = new Player();
        Application.targetFrameRate = 60;
        
        gameTime = new int[3] {1,9,0};
        className = new string[5] { "心电场","模电","高电","复变","大物" };
        checkTime = Time.time;
        eventList = SourceManager.instance.initEventList();
        stayEventList = SourceManager.instance.initStayEventList();
        //Debug.Log(stayEventList[0]);
        examList = Exam.initExam();
        UIManager.instance.updateExamInfo(examList[0].getInfo());
        UIManager.instance.updateAllAbility(player.getAbility());
        UIManager.instance.gameState = UIManager.LoadState.WORD;
        UIManager.instance.wordScene = "周六早上九点\n伴随着闹钟的鸣响\n你醒了过来\n看着熟悉的场景\n准备着即将来到的考试";
        GameManager.instance.gameState = GAMESTATE.LOAD;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.instance.showAllAbility(1);
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            UIManager.instance.showAllAbility(0);
        }
        if (gameState == GAMESTATE.ON)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                UIManager.instance.showBag();
                Time.timeScale = 0;
                gameState = GAMESTATE.STOP;
            }
        }
        else if(gameState == GAMESTATE.STOP)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                UIManager.instance.showBag();
                gameState = GAMESTATE.ON;
            }
        }
        else if (gameState == GAMESTATE.EVENTON)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                UIManager.instance.showRandomEvent(0);
                gameState = GAMESTATE.ON;

            }
        }
        else if (gameState == GAMESTATE.DIALOGON)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                UIManager.instance.showDialog(0);
                UIManager.instance.showHint(UIManager.HintID.NPC,1);
                gameState = GAMESTATE.ON;
                SourceManager.instance.NPCList[SourceManager.instance.currentNPC].index = 0;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                NPC now = SourceManager.instance.NPCList[SourceManager.instance.currentNPC];
                ++now.index;
                if (now.isMulti == 0)
                {
                    if (now.index >= now.max)
                    {
                        Time.timeScale = 1;
                        UIManager.instance.showDialog(0);
                        UIManager.instance.showHint(UIManager.HintID.NPC, 1);
                        gameState = GAMESTATE.ON;
                        now.index = 0;
                    }
                    UIManager.instance.updateDialog(now.name, now.words[now.index]);
                }
                else if(now.isMulti == 1)
                {
                    if (now.index >= now.wordsColl[UIManager.instance.trueTime[0]-1].Count)
                    {
                        Time.timeScale = 1;
                        UIManager.instance.showDialog(0);
                        UIManager.instance.showHint(UIManager.HintID.NPC, 1);
                        gameState = GAMESTATE.ON;
                        now.index = 0;
                    }
                    UIManager.instance.updateDialog(now.name, now.wordsColl[UIManager.instance.trueTime[0]-1][now.index]);
                }
                
            }
        }
        else if (gameState == GAMESTATE.LEARN)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                UIManager.instance.showLearnUI(0);
                GameManager.instance.gameState = GAMESTATE.ON;
            }
        }
        else if (gameState == GAMESTATE.SLEEP)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                UIManager.instance.showSleepUI(0);
                GameManager.instance.gameState = GAMESTATE.ON;
            }
        }
        else if(gameState == GAMESTATE.FISH)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                UIManager.instance.showFishUI(0);
                GameManager.instance.gameState = GAMESTATE.ON;
            }
        }
        else if (gameState == GAMESTATE.SPORT)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                UIManager.instance.showSportUI(0);
                GameManager.instance.gameState = GAMESTATE.ON;
            }
        }
        
        if(gameState == GAMESTATE.LOAD)
        {
            UIManager.instance.showLoadUI();
        }
        else
        {
            UIManager.instance.updateTime();
        }
        
    }


    public void learnUI()
    {
        //Debug.Log("xuexi  " + UIManager.instance.getLearnTime());
        player.learn(UIManager.instance.getLearnTime(),UIManager.instance.getLearnSubject());
        UIManager.instance.updateStatus(player.getStatusExceptAblility());
        UIManager.instance.updateAllAbility(player.getAbility());
        GameManager.instance.gameState = GAMESTATE.ON;
        UIManager.instance.showLearnUI(0);
        int time = (int)(UIManager.instance.gameTimeScale * (Time.time - GameManager.instance.checkTime));
        //Debug.Log(time);
        time = time / 60 + (int)(UIManager.instance.getLearnTime()*60);
        GameManager.instance.gameTime[0] += time / (24 * 60);
        time = time % (24 * 60);
        GameManager.instance.gameTime[1] += time / 60;
        GameManager.instance.gameTime[2] += time % 60;

        GameManager.instance.gameTime[1] += GameManager.instance.gameTime[2] / 60;
        GameManager.instance.gameTime[2] = GameManager.instance.gameTime[2] % 60;

        GameManager.instance.gameTime[0] += GameManager.instance.gameTime[1] / 24;
        GameManager.instance.gameTime[1] = GameManager.instance.gameTime[1] % 24;

        GameManager.instance.checkTime = Time.time;
        UIManager.instance.gameState = UIManager.LoadState.LEARN;
        GameManager.instance.gameState = GAMESTATE.LOAD;
        Time.timeScale = 1;
    }

    public void sleepUI()
    {
        //player.rest();
        int[] hoursAndMinutes = UIManager.instance.getSleepTime();
        if(hoursAndMinutes[0] < 24 && hoursAndMinutes[0] > 0 && hoursAndMinutes[1] >=0 && hoursAndMinutes[1] < 60)
        {
            int[] timeNow = UIManager.instance.getTimeNow();
            if(hoursAndMinutes[0] * 60 + hoursAndMinutes[1] < timeNow[1] * 60 + timeNow[2])
            {
                GameManager.instance.gameTime[0] += 1;
                player.rest(hoursAndMinutes, 24 * 60 + hoursAndMinutes[0] * 60 + hoursAndMinutes[1] - timeNow[1] * 60 - timeNow[2]);
            }
            else
            {
                player.rest(hoursAndMinutes, hoursAndMinutes[0] * 60 + hoursAndMinutes[1] - timeNow[1] * 60 - timeNow[2]);
            }
            GameManager.instance.gameTime[1] = hoursAndMinutes[0];
            GameManager.instance.gameTime[2] = hoursAndMinutes[1];
            GameManager.instance.checkTime = Time.time;
            UIManager.instance.showSleepUI(0);
            UIManager.instance.gameState = UIManager.LoadState.SLEEP;
            //UIManager.instance.showLoadUI();
            //UIManager.instance.showWaitUI();
            GameManager.instance.gameState = GAMESTATE.LOAD;
        }
        else
        {
            UIManager.instance.showSleepUI(0);
            Debug.Log("Error");
        }
        
        Time.timeScale = 1;
    }

    public void goFishUI()
    {
        //player.rest();
        int[] hoursAndMinutes = UIManager.instance.getFishTime();
        upadteTimeViaHoursAndMinutes(hoursAndMinutes);
        int type = UIManager.instance.getFishType();//0,B站，1,游戏，2,发呆，3,短视频，4,购物
       
        player.play(hoursAndMinutes[0] + 1.0f/60 * hoursAndMinutes[1], type);

        UIManager.instance.fishType = type;
        UIManager.instance.gameState = UIManager.LoadState.FISH;
        UIManager.instance.showFishUI(0);
        GameManager.instance.gameState = GAMESTATE.LOAD;
        Time.timeScale = 1;
    }

    public void goSportUI()
    {
        int[] hoursAndMinutes = UIManager.instance.getSportTime();
        if(upadteTimeViaHoursAndMinutes(hoursAndMinutes) == false)
        {
            return;
        }
        int type = UIManager.instance.getSportType();//0,跑步，1,羽毛球，2,篮球，3,手球，4,游泳
        GameManager.instance.player.exercise(hoursAndMinutes[0] + 1.0f / 60 * hoursAndMinutes[1], type);
        
        UIManager.instance.sportType = type;
        UIManager.instance.gameState = UIManager.LoadState.SPORT;
        UIManager.instance.showSportUI(0);
        GameManager.instance.gameState = GAMESTATE.LOAD;

        Time.timeScale = 1;
    }

    private bool upadteTimeViaHoursAndMinutes(int[] hoursAndMinutes)
    {
        if (hoursAndMinutes.Length!= 2 || hoursAndMinutes[0] < 0 || hoursAndMinutes[1] < 0 || hoursAndMinutes[0] > 12 || hoursAndMinutes[1] >= 60)
        {
            return false;
        }
        GameManager.instance.gameTime[1] += hoursAndMinutes[0];
        GameManager.instance.gameTime[2] += hoursAndMinutes[1];
        GameManager.instance.gameTime[1] += GameManager.instance.gameTime[2] / 60;
        GameManager.instance.gameTime[2] %= 60;
        GameManager.instance.gameTime[0] += GameManager.instance.gameTime[1] / 24;
        GameManager.instance.gameTime[1] %= 24;
        GameManager.instance.checkTime = Time.time;
        return true;
    }
    public void checkEventTime(int[] timeNow)
    {
        if (eventIndex >= eventList.Count)
        {
            return;
        }
        else
        {
            if(eventList[eventIndex].checkTime(timeNow))
            {
                Time.timeScale = 0;
                UIManager.instance.updateRandomEvent(eventList[eventIndex]); 
                UIManager.instance.showRandomEvent(1);
                eventList[eventIndex].commit();
                ++eventIndex;
                gameState = GAMESTATE.EVENTON;
                return;
            }
            else if(getTime(eventList[eventIndex].startTime)< getTime(timeNow) )
            {
                ++eventIndex;
            }
        }
    }

    public void checkStayedEvent(int[] timeNow)
    {
        int i = 0;
        bool isOn = false;
        while (i < stayEventList.Count)
        {
            if(getTime(timeNow) > getTime(stayEventList[i].endTime))
            {
                stayEventList.RemoveAt(i);
                continue;
            }
            foreach(GameObject gameObject in stayEventList[i].triggers)
            {
                MyNPC npc = gameObject.GetComponentInParent<MyNPC>();
                /*
                if(npc.isTrigger)
                {
                    Time.timeScale = 0;
                    UIManager.instance.updateRandomEvent(stayEventList[i]);
                    UIManager.instance.showRandomEvent(1);
                    stayEventList[i].commit();
                    gameState = GAMESTATE.EVENTON;
                    stayEventList.RemoveAt(i);
                    isOn = true;
                    break;
                }
                */
                if(npc == null || npc.gameObject.activeSelf == false)
                {
                    continue;
                }
                if ((npc.transform.GetChild(0).position - playerObject.transform.position).magnitude <= (npc.transform.GetChild(0).position - npc.transform.GetChild(0).GetChild(0).position).magnitude)
                {
                    Time.timeScale = 0;
                    UIManager.instance.updateRandomEvent(stayEventList[i]);
                    UIManager.instance.showRandomEvent(1);
                    stayEventList[i].commit();
                    gameState = GAMESTATE.EVENTON;
                    stayEventList.RemoveAt(i);
                    isOn = true;
                    break;
                }
            }
            if (isOn)
            {
                isOn = false;
                continue;
            }
            ++i;
        }
    }

    private int getTime(int[] timeNow)
    {
        if(timeNow.Length != 3)
        {
            throw new Exception();
        }
        return timeNow[0]*24*60 + timeNow[1]*60 + timeNow[2];
    }

    public void checkOver(string overWord)
    {
        if (GameManager.instance.gameState == GAMESTATE.OFF) return;
        UIManager.instance.gameState = UIManager.LoadState.WORD;
        UIManager.instance.wordScene = overWord + "\r";
        GameManager.instance.gameState = GAMESTATE.LOAD;
        Debug.Log(overWord);
    }

    private void gameOver()
    {
        if (GameManager.instance.gameState == GAMESTATE.OFF) return;
        int notPass = 0;
        for (int i = 0; i < examList.Count; ++i)
        {
            if (examList[i].passed == false)
            {
                ++notPass;
            }
        }
        if(notPass != 0)
        {
            UIManager.instance.gameState = UIManager.LoadState.WORD;
            UIManager.instance.wordScene = "你最终有" + notPass + "门科目挂科了\n" + "这是一个沉重的打击\r";
            GameManager.instance.gameState = GAMESTATE.LOAD;
            Debug.Log("You have failed " + notPass + " courses!");
        }
        else
        {
            UIManager.instance.gameState = UIManager.LoadState.WORD;
            UIManager.instance.wordScene = "你通过了所有的考试\n恭喜你顺利地度过了这一周\r";
            GameManager.instance.gameState = GAMESTATE.LOAD;
            Debug.Log("You have made it through the week!");
        }
        //Time.timeScale = 0;
        //GameManager.instance.gameState = GAMESTATE.OFF;
        
    }

    public void checkExam(int[] timeNow)
    {
        if(examIndex == examList.Count)
        {
            gameOver();
            return;
        }
        int examMinutes = getTime(examList[examIndex].startTime);
        int nowMinutes = getTime(timeNow);
        if ( examMinutes- nowMinutes < 30 && examMinutes - nowMinutes > 0)
        {
            examList[examIndex].prepare();
            return;
        }
        else if (examMinutes <= nowMinutes)
        {
            examList[examIndex].isMissed();
            examIndex += 1;
            if(eventIndex >= examList.Count || examIndex < 0)
            {
                return;
            }
            UIManager.instance.updateExamInfo(examList[examIndex].getInfo());
        }
    }

    public void goExam()
    {
        if(examIndex >= examList.Count)
        {
            UIManager.instance.showGoExamBtn(0);
            return ;
        }
        examList[examIndex].isPass(player);
        examList[examIndex].commit(player);
        GameManager.instance.gameTime = examList[examIndex].endTIme;
        GameManager.instance.checkTime = Time.time;
        examIndex += 1;
        UIManager.instance.updateExamInfo(examList[examIndex].getInfo());
        UIManager.instance.showGoExamBtn(0);
    }

    public void transformScene(int id)
    {
        if(id == 0)
        {
            SceneManager.LoadScene("Floor1");
            playerObject.transform.position = Vector3.zero;
            RenderSettings.skybox = UIManager.instance.skyLight[0];
            SourceManager.instance.classroomNPCs.SetActive(false);
            SourceManager.instance.dominateNPCs.SetActive(true);
        }
        else if(id == 1)
        {
            SceneManager.LoadScene("SampleScene");
            playerObject.transform.position = Vector3.zero;
            RenderSettings.skybox = UIManager.instance.skyLight[0];
            SourceManager.instance.classroomNPCs.SetActive(true);
            SourceManager.instance.dominateNPCs.SetActive(false);
        }
    }
}

//原Player
/*
public class Player
{
    private float energy { get; set; }//精力值
    private float efficiency { get; set; }//效率值
    private int[] ability { get; set; }//能力值
    private float health { get; set; }//健康值
    private float mentality;
    public List<int> thingsList;
    public Player()
    {
        energy = 100;
        efficiency = 100;
        ability = new int[5] {50,50,50,50,50};
        health = 100;
        mentality = 100;
        thingsList = new List<int>();
    }

    public int[] getAbility()
    {
        return ability;
    }

    public void learn(float hour)
    {
        if (energy - hour < 0)
        {
            energy = 0;
        }
        else
        {
            this.energy -= hour;
        }
        if (efficiency - hour < 0)
        {
            efficiency = 0;
        }
        else
        {
            this.efficiency -= hour;
        }
        if (ability[1] >= 10)
        {
            ability[1] = 10;
        }
        else
        {
            ++ability[1];
        }
        if (health - hour < 0)
        {
            health = 0;
        }
        else
        {
            this.health -= hour;
        }
    }

    public void learn(float hour ,int subject)
    {
        int[] effect = new int[4] { 3, 5, 2, 2 };
        if(UIManager.instance.trueTime[1] >= 23 || UIManager.instance.trueTime[1]<5)
        {
            learnTired(hour, subject,effect);
        }
        else
        {
            learnNormal(hour, subject,effect);
        }    
    }

    private void learnTired(float hour, int subject,int[] effect) 
    {
        float loss = 0.9f;
        float add = 1.1f;
        float learnTime = hour * 2;
        if(energy - learnTime * add * effect[0] < 0 )
        {
            energy = 0;
        }
        else
        {
            energy -= learnTime * add * effect[0];
        }
        if (efficiency - learnTime * add * effect[1] < 0)
        {
            efficiency = 0;
        }
        else
        {
            efficiency -= learnTime * add * effect[1];
        }
        if (health - learnTime * add * effect[2] < 0)
        {
            health = 0;
        }
        else
        {
            health -= learnTime * add * effect[2];
        }
        int addition = (int) (effect[3] * loss * learnTime * (efficiency / 100) * (energy / 100));
        if(ability[subject] + addition > 100)
        {
            ability[subject] = 100;
        }
        else
        {
            ability[subject] += addition;
        }
        this.updateAll();
    }

    private void learnNormal(float hour, int subject,int[] effect)
    {
        float learnTime = hour * 2;
        if (energy - learnTime * effect[0] < 0)
        {
            energy = 0;
        }
        else
        {
            energy -= learnTime * effect[0];
        }
        if (efficiency - learnTime * effect[1] < 0)
        {
            efficiency = 0;
        }
        else
        {
            efficiency -= learnTime * effect[1];
        }
        int addition = (int)(effect[3] *  learnTime * (efficiency / 80) * (energy / 80));
        if (ability[subject] + addition > 100)
        {
            ability[subject] = 100;
        }
        else
        {
            ability[subject] += addition;
        }
        this.updateAll();
    }

    public void play(float hour)
    {
        this.energy -= hour;
        this.efficiency -= hour;
        this.health -= hour;
    }

    public void exercise(float hour)
    {
        this.energy -= hour;
        this.efficiency -= hour;
        this.health -= hour;
    }

    public void rest(float hour)
    {
        this.energy += hour;
        this.health += hour;
        this.efficiency += hour;
    }

    public void rest(int[] startTime, int minutes)
    {
        if (startTime[0] <= 5 && startTime[0] >= 1)
        {
            restTired(minutes);
        }
        else if (startTime[0] <= 14 && startTime[0] >= 12 && minutes >= 30 && minutes <= 60)
        {
            restNoon(minutes);
        }
        else if(startTime[0] >= 22)
        {
            restNormal(minutes);
        }
        else 
        {
            restNormal(minutes / 2);
        }
        energy = energy >= 100 ? 100 : energy;
        efficiency = efficiency >= 100 ? 100 : efficiency;
        health = health >= 100 ? 100 : health;
        energy = (int)energy;
        efficiency = (int)efficiency;
        health = (int)health;
        updateAll();
    }

    private void restTired(int minutes)
    {
        if(energy <= 90)
        {
            this.energy += (95 - this.energy) * (energy / (2 * 6 * 60));
        }
        if(efficiency <= 90)
        {
            this.efficiency += (95 - this.energy) * (energy / (2 * 6 * 60));
        }
        
        this.health += 5;
    }

    private void restNormal(int minutes)
    {
        if(minutes >= 6 * 60)
        {
            this.energy = 100;
            this.efficiency = 100;
            this.health = this.health > 90 ? this.health : this.health + 10; 
        }
        else
        {
            this.energy += (100 - this.energy) * (minutes/(2 * 6*60));
            this.efficiency += (100 - this.energy) * (minutes / (2 * 6 * 60));
            this.health += 5;
        }
    }

    private void restNoon(int minutes)
    {
        this.energy = this.energy > 95?100:95;
        this.efficiency = this.efficiency > 95 ? 100 : 95;
        this.health = this.health > 70 ? (this.health < 90 ? this.health + 10 : 100) : (this.health + 20);
    }


    public string getStatusExceptAblility()
    {
        return "精力值：" + energy 
            +"\n效率值：" + efficiency
            +"\n健康值：" + health;
    }

    public void commit(int energyChange, int efficiencyChange, int healthChange, int[] abilityChange)
    {
        this.energy += energyChange;
        this.efficiency += efficiencyChange;
        this.health += healthChange;
        if(energy < 0)
        {
            energy = 0;
        }
        else if(energy > 100)
        {
            energy = 100;
        }
        if (efficiency < 0)
        {
            efficiency = 0;
        }
        else if (efficiency > 100)
        {
            efficiency = 100;
        }
        if (health < 0)
        {
            health = 0;
        }
        else if (health > 100)
        {
            health = 100;
        }
        UIManager.instance.updateStatus(this.getStatusExceptAblility());
        if(abilityChange.Length != 5)
        {
            return;
        }
        else
        {
            for (int i = 0; i < abilityChange.Length; i++)
            {
                ability[i] += abilityChange[i];
                if(ability[i] < 0)
                {
                    ability[i] = 0;
                }
                else if(ability[i] > 100)
                {
                    ability[i] = 100;
                }
            }
        }
        UIManager.instance.updateAllAbility(this.getAbility());
    }

    private void updateAll()
    {
        UIManager.instance.updateAllAbility(this.getAbility());
        UIManager.instance.updateStatus(this.getStatusExceptAblility());
    }
}
*/


public class RandomEvent
{
    public int[] startTime;
    public int[] endTime;
    protected string textDescribe;
    protected int energyChange;
    protected int efficiencyChange;
    protected int healthChange;
    protected int[] abilityChange = new int[5] { 0, 0, 0, 0, 0 };
    public Sprite eventPic;
    public RandomEvent(){}
    public RandomEvent(string textDescribe, int energyChange, int efficiencyChange , int healthChange, int[] abilityChange, int[] startTime)
    {
        this.textDescribe = textDescribe;
        this.energyChange = energyChange;
        this.efficiencyChange = efficiencyChange;
        this.healthChange = healthChange;
        this.abilityChange = abilityChange;
        this.startTime = startTime;
        this.endTime = startTime;
    }

    public RandomEvent(string textDescribe, int energyChange, int efficiencyChange, int healthChange, int[] abilityChange, int[] startTime, Sprite eventPic)
    {
        this.textDescribe = textDescribe;
        this.energyChange = energyChange;
        this.efficiencyChange = efficiencyChange;
        this.healthChange = healthChange;
        this.abilityChange = abilityChange;
        this.startTime = startTime;
        this.endTime = startTime;
        this.eventPic = eventPic;
    }

    public bool checkTime(int[] timeNow) 
    { 
        for(int i = 0;i<timeNow.Length;++i)
        {
            if(timeNow[i] != startTime[i])
            {
                return false;
            }
        }
        return true;
    }

    public void commit()
    {
        Player player = GameManager.instance.player;
        player.commit(energyChange, efficiencyChange, healthChange, abilityChange);

    }

    public string getUIInfo()
    {
        string result = "";
        if(energyChange != 0)
        {
            result += "精力值" + energyChange + "  ";
        }
        if(efficiencyChange != 0)
        {
            result += "效率值" + efficiencyChange + "  ";
        }
        if(healthChange != 0)
        {
            result += "健康值" + healthChange + "  ";
        }
        result += "\n";
        for(int i = 0; i < abilityChange.Length; i++)
        {
            if(abilityChange[i] != 0)
            {
                result += GameManager.instance.className[i] + "掌握程度" + abilityChange[i];
            }
        }
        return textDescribe + result;
    }
}

public class StayRandomEvent:RandomEvent
{
    public GameObject[] triggers;

    public StayRandomEvent(string textDescribe, int energyChange, int efficiencyChange, int healthChange, int[] abilityChange, int[] startTime, int[] endTime, GameObject[] triggers)
    {
        this.textDescribe = textDescribe;
        this.energyChange = energyChange;
        this.efficiencyChange = efficiencyChange;
        this.healthChange = healthChange;
        this.abilityChange = abilityChange;
        this.startTime = startTime;
        this.endTime = endTime;
        this.triggers = triggers;
    }

    public StayRandomEvent(string textDescribe, int energyChange, int efficiencyChange, int healthChange, int[] abilityChange, int[] startTime, int[] endTime, GameObject[] triggers, Sprite eventPic)
    {
        this.textDescribe = textDescribe;
        this.energyChange = energyChange;
        this.efficiencyChange = efficiencyChange;
        this.healthChange = healthChange;
        this.abilityChange = abilityChange;
        this.startTime = startTime;
        this.endTime = endTime;
        this.triggers = triggers;
        this.eventPic = eventPic;
    }

}

public class Exam
{
    public string name;
    public int[] startTime;
    public int[] endTIme;
    private int effect { get; }
    private int abilityNeeded;
    private string[] describe { get; }
    public bool passed { get; set; }
    private STATE state;
    private int id;
    enum STATE {PREPARED , FAR, OVER};

    public Exam() { }
    public Exam(string name, int[] startTime, int[] endTIme, int effect,string[] describe, int id,int abilityNeeded) 
    {
        this.name = name;
        this.startTime = startTime;
        this.endTIme = endTIme;
        this.effect = effect;
        this.describe = describe;
        this.abilityNeeded = abilityNeeded;
        passed = false;
        state = STATE.FAR;
    }

    public void commit(Player player)
    {
        if(passed)
        {

        }
        else
        {

        }
    }

    public bool isPass(Player player)
    {
        state = STATE.OVER;
        if(player.getAbility()[id] > this.abilityNeeded)
        {
            UIManager.instance.wordScene = this.describe[0];
            passed = true;
        }
        else if (player.getAbility()[id] > 80)
        {
            UIManager.instance.wordScene = this.describe[1];
            passed = true;
        }
        else if (player.getAbility()[id] > 70)
        {
            UIManager.instance.wordScene = this.describe[2];
            passed = true;
        }
        else 
        {
            UIManager.instance.wordScene = this.describe[2];
            passed = false;
        }
        GameManager.instance.gameState = GAMESTATE.LOAD;
        UIManager.instance.gameState = UIManager.LoadState.WORD;
        return passed;
    }

    public static List<Exam> initExam()
    {
        List<Exam> list = new List<Exam>();
        list.Add(new Exam("电磁场",new int[3] { 3, 15 ,0}, new int[3] { 3, 16, 30 },50, 
            new string[] { "由于及时的复习\n你顺利地通过了考试\n并感到信心倍增",
            "你感觉自己考得一般\n似乎有点对不上自己过去几天的复习\n失落的情绪开始聚集",
            "你感觉很非常糟糕\n因为试卷上的题目你几乎全部都不会写\n你陷入了深深的自我怀疑之中" },0,85));
        list.Add(new Exam("模拟电子技术基础", new int[3] { 5,19 , 30 }, new int[3] { 5, 21, 00 }, 50,
            new string[] { "你觉得试卷上的题目很简单\n并顺利地通过了考试",
            "你感觉自己考得一般\n并没有达到理想的效果\n似乎有点对不上自己过去几天的复习",
            "你感觉很糟糕\n因为试卷上的题目你几乎全部都不会写" },1,85));
        list.Add(new Exam("高等电路分析", new int[3] { 6, 9, 0 }, new int[3] { 6, 10, 30 }, 50,
            new string[] { "由于及时的复习\n你顺利地通过了考试！",
            "你感觉自己发挥失常\n似乎有点对不上自己过去几天的复习",
            "你感觉很糟糕\n因为试卷上的题目你几乎全部都不会写" },2,85));
        list.Add(new Exam("复变函数", new int[3] { 6, 19, 30 }, new int[3] { 6, 21, 00 }, 50,
            new string[] { "由于及时的复习\n你顺利地通过了考试\n并庆幸考试即将结束",
            "你感觉自己考得一般\n似乎有点对不上自己过去几天的复习\n但考试也快要结束了",
            "你感觉很糟糕\n因为试卷上的题目你几乎全部都不会写" },3,85));
        list.Add(new Exam("大学物理", new int[3] { 7, 9, 0 }, new int[3] { 7, 10, 30 }, 50,
            new string[] { "由于及时的复习\n你顺利地通过了考试！",
            "你感觉自己考得一般\n似乎有点对不上自己过去几天的复习",
            "你感觉很糟糕\n因为试卷上的题目你几乎全部都不会写" },4,85));
        return list;

    }

    public void prepare()
    {
        if(state == STATE.FAR)
        {
            UIManager.instance.showGoExamBtn(1);
            state = STATE.PREPARED;
            UIManager.instance.updateMessage("准备考试："+name);
        }
    }

    public void isMissed()
    {
        if(state != STATE.OVER)
        {
            passed = false;
            UIManager.instance.showGoExamBtn(0);
            //Debug.Log("你错过了考试！");
            UIManager.instance.wordScene = "你错过了考试！\n" + name + "\n悬在你头顶上的利剑似乎越来越近了";
            UIManager.instance.gameState = UIManager.LoadState.WORD;
            GameManager.instance.gameState = GAMESTATE.LOAD;
        }
    }

    public string getInfo()
    {
        if(startTime[2] == 0)
        {
            return name + "\nDay" + startTime[0] + " " + startTime[1] + ":00";
        }
        else
        {
            return name + "\nDay" + startTime[0] + " " + startTime[1] + ":" + startTime[2];
        }
    }

    public string getQInfo()
    {
        if (startTime[2] == 0)
        {
            return name + "(Day" + startTime[0] + " " + startTime[1] + ":00)";
        }
        else
        {
            return name + "(Day" + startTime[0] + " " + startTime[1] + ":" + startTime[2]+")";
        }
    }

    public static bool getRandom()
    {
        return true;
    }
}
/// <summary>
/// ON 游戏进行中
/// OFF 游戏未进行
/// EVENTON 随机事件显示
/// DIALOGON 对话框显示
/// LEARN 学习
/// SLEEP 睡觉
/// LOAD 加载界面
/// WORD 供UIManager使用，修改显示的话
/// FISH 摸鱼
/// </summary>
public enum GAMESTATE { ON, OFF ,EVENTON,STOP, DIALOGON, LEARN, SLEEP, LOAD, WORD,FISH,SPORT}