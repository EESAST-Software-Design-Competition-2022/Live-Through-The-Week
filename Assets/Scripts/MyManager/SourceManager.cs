using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceManager : MonoBehaviour
{
    public static SourceManager instance;
    public GameObject classroomNPCs;
    public GameObject dominateNPCs;
    public GameObject NPCs;

    public GameObject BLLEdit;
    public List<NPC> NPCList;
    public List<Thing> thingsList;
    public List<Func<bool>> actions;
    public int currentNPC;
    private int eventNum = 5;

    public GameObject secretDoor;
    //public Sprite newbook;
    // Start is called before the first frame update
    void Start()
    {
        NPCs = GameObject.Find("NPC");
        classroomNPCs = NPCs.transform.GetChild(0).gameObject;
        dominateNPCs = NPCs.transform.GetChild(1).gameObject;
        
        instance = this;
        //Debug.Log(SourceManager.instance.NPC.name);
        NPCList = initNPC();
        thingsList = initThingsList();
        actions = initActionList();
        //Texture2D tex = (Texture2D)Resources.Load("InteractableThingsPic/1");
        //newbook = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        //newbook = getSprite(1);
    }

    

    // Update is called once per frame
    void Update()
    {
    }

    public Sprite getSprite(int index)
    {
        if(index < 0 || index > thingsList.Count)
        {
            return null;
        }
        else
        {
            Texture2D tex = (Texture2D)Resources.Load("InteractableThingsPic/" + index);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }
    }

    public Sprite getEventSprite(int index)
    {
        if (index < 0 || index >= eventNum)
        {
            return null;
        }
        else
        {
            Texture2D tex = (Texture2D)Resources.Load("EventPic/" + index);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }
    }

    public List<Thing> initThingsList()
    {
        List<Thing> thingsList = new List<Thing>();
        thingsList.Add(new Thing("学长的原题", 0, "一沓泛黄的纸张上密密麻麻得写满了字，隐约能看清纸张最前面的几个字：电8x x泽"));
        thingsList.Add(new Thing("强力的咖啡", 1, "一包过期了的咖啡，据说有着很强的效力"));
        thingsList.Add(new Thing("压缩食品", 2, "一包压缩食品，能够略微增加饱腹感"));
        thingsList.Add(new Thing("破旧的钥匙", 3, "一把生锈了的钥匙，不知道有什么奇怪的作用"));
        thingsList.Add(new Thing("电磁场习题辅导", 4, "一本破旧的红色书籍，上面写着“电磁场习题辅导”"));
        thingsList.Add(new Thing("大学物理题库(1)", 5, "一份大学物理题库云盘资料"));
        thingsList.Add(new Thing("康师傅老坛酸菜牛肉面", 6, "一份方便面，泡好以后闻起来似乎还有酸菜以外的其他的味道"));
        thingsList.Add(new Thing("白象老坛酸菜牛肉面", 7, "一份普通的方便面，泡好以后散发着正常方便面的香味"));
        thingsList.Add(new Thing("神秘的眼镜", 8, "隐藏着神性光辉的眼镜，镜片上反射出深邃的光晕"));
        thingsList.Add(new Thing("沙漠神灯", 9, "不知道它能不能召唤出远古的灯神？" ));
        thingsList.Add(new Thing("古老的课本", 10, "或许隐藏着过去的秘密"));
        thingsList.Add(new Thing("考试信息摘要",11,"一张小纸条，上面用潦草的字写着：\n周一下午电磁场\n周三晚上模电\n周四上午高电，晚上复变\n周五上午大雾\n不能忘记时间，提前三十分钟准备去考场！！！(特别加粗)"));
        thingsList.Add(new Thing("士力架", 12, "经典巧克力零食，饱腹感极佳"));
        thingsList.Add(new Thing("可口可乐", 13, "被誉为快乐水的它，真的能让我快乐吗？"));
        thingsList.Add(new Thing("奇怪的钥匙", 14, "一把造型奇怪的钥匙，不知道能打开哪里的门"));
        return thingsList;
    }

    private List<Func<bool>> initActionList()
    {
        List<Func<bool>> actionList = new List<Func<bool>>();
        actionList.Add(() => { UIManager.instance.updateMessage("使用学长的原题");GameManager.instance.player.commit(0, 0, 0, new int[] { 100, 100, 100, 100, 100 });return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("使用强力的咖啡，感觉肚子有一些难受，却感到异常精神"); GameManager.instance.player.commit(10, 8, -3, new int[] { 0}); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("使用压缩食品"); GameManager.instance.player.commit(10, 10, 0, new int[] { 0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("使用生锈的钥匙"); Debug.Log("打开408B"); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("使用电磁场习题辅导"); GameManager.instance.player.commit(0, 0, 0, new int[] { 10,0,0,0,0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("使用大学物理题库(1)"); GameManager.instance.player.commit(0, 0, 0, new int[] { 0, 0, 0, 0, 15 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("使用康师傅老坛酸菜牛肉面"); GameManager.instance.player.commit(5, 4, -2, new int[] { 0, 0, 0, 0, 0 }); return true; });      
        actionList.Add(() => { UIManager.instance.updateMessage("使用白象老坛酸菜牛肉面"); GameManager.instance.player.commit(5, 4, 0, new int[] { 0, 0, 0, 0, 0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("使用神秘的眼镜，你得知了下一次考试的能力要求：85点"); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("召唤远古的灯神，远古灯神开心地捉弄起了你"); GameManager.instance.player.commit(-5, -4, 0, new int[] { 0, 0, 0, 0, 0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage(""); GameManager.instance.player.commit(0, 0, 0, new int[] { 3, 3, 3, 3, 3 }); return true; });
        actionList.Add(() => { return false; });
        //上一行？
        actionList.Add(() => { UIManager.instance.updateMessage("饿货，来一条");  GameManager.instance.player.commit(5, 4, 2, new int[] { 0, 0, 0, 0, 0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("感觉，不如pepsi");  GameManager.instance.player.commit(3, 2, 0, new int[] { 0, 0, 0, 0, 0 }); return true; });
        actionList.Add(() => { 
            try
            {
                GameObject gameObject = transform.Find("/DominateCollection/SpecialDominate/Atrium/Secret").gameObject;
                Destroy(gameObject.GetComponent<BoxCollider>());
            }
            catch (Exception ex)
            {
                UIManager.instance.updateMessage("使用的方式似乎不太对");
                return false;
            }
            UIManager.instance.updateMessage("似乎有什么东西消失了");
            //GameManager.instance.player.commit(0, 0, 0, new int[] { 0, 0, 0, 0, 0 });
            return true; 
        });
        return actionList;
    }

    private List<NPC> initNPC()
    {
        NPCList = new List<NPC>() {
            new NPC("刘飞",0,new List<string>{"马上就要考试了","感觉自己还完全没有准备","太难了","抓紧时间复习吧，加油","诶嘿"}),
            new NPC("李久",1,new List<string>[7]{ 
                new List<string>{"马上就要考试了","感觉自己还完全没有准备",},
                new List<string>{"昨天晚上好像有听到了一些奇怪的声音","我们宿舍的人好像都听到了","我也不知道是什么呢","抓紧时间复习吧，加油"},
                new List<string>{"昨晚又听见了奇怪的声音","这次他们都带了耳塞，只有我听到了","到底是什么呢"},
                new List<string>{"我的电磁场算是完蛋了","今天难得可以休息一天呢","太难了","抓紧时间复习吧，加油","诶嘿"},
                new List<string>{"感觉我最好还是直接摆烂~"},
                new List<string>{"……(看起来不怎么想搭理你)"},
                new List<string>{"……(看起来不怎么想搭理你)"}
            }),
            new NPC("杨冬",2,new List<string>{"什么，你是在叫我吗？(摘下耳机,从床上探出头来)","我要开摆了","别再来找我了","(不再说话)"}),
            new NPC("张明",3,new List<string>[7]{
                new List<string>{"感觉我一边想学习一边又想玩","但是却冲突了……"},
                new List<string>{"昨天晚上好像有听到奇怪的声音","大概是我的错觉吧"},
                new List<string>{"哎，复习真难啊"},
                new List<string>{"今天难得可以休息一天呢","(……)"},
                new List<string>{"(……)"},
                new List<string>{"我觉得我大概是摆了"},
                new List<string>{"……(看起来不怎么想搭理你)"}
            }),
            new NPC("李华",4,new List<string>{"马上就要考试了","谁来帮我写一封信啊，我已经不会英语了……(思维错乱中)"}),
            new NPC("王离",5,new List<string>[7]{
                new List<string>{"你复习的怎么样了？","感觉之前摆的有点太多了，现在都快补不回来了……","(一些听不清楚的自言自语)要是...那个东西就...，可惜不知道..."},
                new List<string>{"(一些听不清楚的自言自语)...还是没...，但明天就要开始了...还是...复习吧..."},
                new List<string>{"……(看起来不怎么想搭理你)"},
                new List<string>{"今天难得可以休息一天呢","(……)"},
                new List<string>{"(一些听不清楚的自言自语)...难得的...休息...我一定...找出来..."},
                new List<string>{"还是没有找到呢..."},
                new List<string>{"我前几天在找什么？","是学长的原题啦。听说以前的某位学长对我们这个学期的考试资料做了翔实的总结，","有了那个就一定能行的","但我并没有找到呢，真是可惜了"}
            }),
            new NPC("奉原",6,new List<string>[7]{
                new List<string>{"为什么今年的考试都没有原题啊！！","感觉我大概是要无了"},
                new List<string>{"还是没有找到","可是我也不想复习诶"},
                new List<string>{"完了完了，要开始考第一门了","我甚至才刚刚开始复习"},
                new List<string>{"……(看起来不怎么想搭理你)"},
                new List<string>{"……(看起来不怎么想搭理你)"},
                new List<string>{"……(看起来不怎么想搭理你)"},
                new List<string>{ "……(看起来不怎么想搭理你)" }
            }),
            new NPC("张雨",7,new List<string>[7]{
                new List<string>{"嗯？是你啊。","你应该复习的很不错吧？"},
                new List<string>{"昨晚似乎听到了一些奇怪的声音","emmm，我也说不清楚，似乎是从隔壁传来的声音"},
                new List<string>{"第一门考试就这样结束了啊，可惜我还是没有找到学长说的东西呢"},
                new List<string>{"……(看起来不怎么想搭理你)"},
                new List<string>{"……(看起来不怎么想搭理你)"},
                new List<string>{"后面大概就轻松起来了"},
                new List<string>{ "终于要结束了吗" }
            })
            //new NPC("hls")
        };
        return NPCList;
    }

    public List<RandomEvent> initEventList()
    {
        List<RandomEvent> eventList = new List<RandomEvent>();
        eventList.Add(new RandomEvent("起床时间过晚\n影响心态\n", -5, -4, 0, new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 1, 9, 5 }, getEventSprite(0)));
        eventList.Add(new RandomEvent("听了一首最喜欢的歌\n心情大好\n", 0, 3, 0, new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 1, 9, 20 }, getEventSprite(1)));
        eventList.Add(new RandomEvent("教室里出现了一对情侣\n你被喂饱了狗粮\n无心学习\n", -2, -4, 0, new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 4, 14, 0 }, getEventSprite(2)));
        return eventList;
    }

    public List<StayRandomEvent> initStayEventList()
    {
        List<StayRandomEvent> stayEventList = new List<StayRandomEvent>();
        //Debug.Log(SourceManager.instance.NPCs.name);
        stayEventList.Add(new StayRandomEvent("偶然听到同学讨论原题\n影响心态\n", 0, -6, 0,
            new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 3, 18, 0 }, new int[3] { 3, 20, 0 },
            new GameObject[1] { dominateNPCs.transform.Find("npc5").gameObject }, getEventSprite(3)));
        stayEventList.Add(new StayRandomEvent("宿舍里出现了很多人吵吵嚷嚷\n你受到了很大影响\n", 0, -6, 0,
            new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 1, 9, 0 }, new int[3] { 1, 12, 0 },
            new GameObject[1] { dominateNPCs.transform.Find("npc1").gameObject }, getEventSprite(4)));
        return stayEventList;
    }


}
public class NPC
{
    public string name;
    public int id;
    public List<string> words;
    public List<string>[] wordsColl; 
    public int index;
    public int max;
    public int isMulti;
    public NPC() { }

    public NPC(string name, int id, List<string> words)
    {
        this.name = name;
        this.id = id;
        this.words = words;
        index = 0;
        max = words.Count;
        isMulti = 0;
    }
    public NPC(string name, int id, List<string>[] wordsColl)
    {
        this.name = name;
        this.id = id;
        this.wordsColl = wordsColl;
        index = 0;
        //max = words.Count;
        isMulti = 1;
    }
}


public class Thing
{
    public string name;
    public int id;
    public string describe;
    public Thing(){    }
    public Thing(string name, int id, string describe)
    {
        this.name=name;
        this.id=id;
        this.describe=describe;
    }
}