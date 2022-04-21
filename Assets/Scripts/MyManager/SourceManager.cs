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
        thingsList.Add(new Thing("ѧ����ԭ��", 0, "һ���Ƶ�ֽ�������������д�����֣���Լ�ܿ���ֽ����ǰ��ļ����֣���8x x��"));
        thingsList.Add(new Thing("ǿ���Ŀ���", 1, "һ�������˵Ŀ��ȣ���˵���ź�ǿ��Ч��"));
        thingsList.Add(new Thing("ѹ��ʳƷ", 2, "һ��ѹ��ʳƷ���ܹ���΢���ӱ�����"));
        thingsList.Add(new Thing("�ƾɵ�Կ��", 3, "һ�������˵�Կ�ף���֪����ʲô��ֵ�����"));
        thingsList.Add(new Thing("��ų�ϰ�⸨��", 4, "һ���ƾɵĺ�ɫ�鼮������д�š���ų�ϰ�⸨����"));
        thingsList.Add(new Thing("��ѧ�������(1)", 5, "һ�ݴ�ѧ���������������"));
        thingsList.Add(new Thing("��ʦ����̳���ţ����", 6, "һ�ݷ����棬�ݺ��Ժ��������ƺ�������������������ζ��"));
        thingsList.Add(new Thing("������̳���ţ����", 7, "һ����ͨ�ķ����棬�ݺ��Ժ�ɢ�����������������ζ"));
        thingsList.Add(new Thing("���ص��۾�", 8, "���������Թ�Ե��۾�����Ƭ�Ϸ��������Ĺ���"));
        thingsList.Add(new Thing("ɳĮ���", 9, "��֪�����ܲ����ٻ���Զ�ŵĵ���" ));
        thingsList.Add(new Thing("���ϵĿα�", 10, "���������Ź�ȥ������"));
        thingsList.Add(new Thing("������ϢժҪ",11,"һ��Сֽ�����������ʲݵ���д�ţ�\n��һ�����ų�\n��������ģ��\n��������ߵ磬���ϸ���\n�����������\n��������ʱ�䣬��ǰ��ʮ����׼��ȥ����������(�ر�Ӵ�)"));
        thingsList.Add(new Thing("ʿ����", 12, "�����ɿ�����ʳ�������м���"));
        thingsList.Add(new Thing("�ɿڿ���", 13, "����Ϊ����ˮ��������������ҿ�����"));
        thingsList.Add(new Thing("��ֵ�Կ��", 14, "һ��������ֵ�Կ�ף���֪���ܴ��������"));
        return thingsList;
    }

    private List<Func<bool>> initActionList()
    {
        List<Func<bool>> actionList = new List<Func<bool>>();
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ��ѧ����ԭ��");GameManager.instance.player.commit(0, 0, 0, new int[] { 100, 100, 100, 100, 100 });return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ��ǿ���Ŀ��ȣ��о�������һЩ���ܣ�ȴ�е��쳣����"); GameManager.instance.player.commit(10, 8, -3, new int[] { 0}); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ��ѹ��ʳƷ"); GameManager.instance.player.commit(10, 10, 0, new int[] { 0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ�������Կ��"); Debug.Log("��408B"); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ�õ�ų�ϰ�⸨��"); GameManager.instance.player.commit(0, 0, 0, new int[] { 10,0,0,0,0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ�ô�ѧ�������(1)"); GameManager.instance.player.commit(0, 0, 0, new int[] { 0, 0, 0, 0, 15 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ�ÿ�ʦ����̳���ţ����"); GameManager.instance.player.commit(5, 4, -2, new int[] { 0, 0, 0, 0, 0 }); return true; });      
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ�ð�����̳���ţ����"); GameManager.instance.player.commit(5, 4, 0, new int[] { 0, 0, 0, 0, 0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("ʹ�����ص��۾������֪����һ�ο��Ե�����Ҫ��85��"); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("�ٻ�Զ�ŵĵ���Զ�ŵ����ĵ�׽Ū������"); GameManager.instance.player.commit(-5, -4, 0, new int[] { 0, 0, 0, 0, 0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage(""); GameManager.instance.player.commit(0, 0, 0, new int[] { 3, 3, 3, 3, 3 }); return true; });
        actionList.Add(() => { return false; });
        //��һ�У�
        actionList.Add(() => { UIManager.instance.updateMessage("��������һ��");  GameManager.instance.player.commit(5, 4, 2, new int[] { 0, 0, 0, 0, 0 }); return true; });
        actionList.Add(() => { UIManager.instance.updateMessage("�о�������pepsi");  GameManager.instance.player.commit(3, 2, 0, new int[] { 0, 0, 0, 0, 0 }); return true; });
        actionList.Add(() => { 
            try
            {
                GameObject gameObject = transform.Find("/DominateCollection/SpecialDominate/Atrium/Secret").gameObject;
                Destroy(gameObject.GetComponent<BoxCollider>());
            }
            catch (Exception ex)
            {
                UIManager.instance.updateMessage("ʹ�õķ�ʽ�ƺ���̫��");
                return false;
            }
            UIManager.instance.updateMessage("�ƺ���ʲô������ʧ��");
            //GameManager.instance.player.commit(0, 0, 0, new int[] { 0, 0, 0, 0, 0 });
            return true; 
        });
        return actionList;
    }

    private List<NPC> initNPC()
    {
        NPCList = new List<NPC>() {
            new NPC("����",0,new List<string>{"���Ͼ�Ҫ������","�о��Լ�����ȫû��׼��","̫����","ץ��ʱ�临ϰ�ɣ�����","����"}),
            new NPC("���",1,new List<string>[7]{ 
                new List<string>{"���Ͼ�Ҫ������","�о��Լ�����ȫû��׼��",},
                new List<string>{"�������Ϻ�����������һЩ��ֵ�����","����������˺���������","��Ҳ��֪����ʲô��","ץ��ʱ�临ϰ�ɣ�����"},
                new List<string>{"��������������ֵ�����","������Ƕ����˶�����ֻ����������","������ʲô��"},
                new List<string>{"�ҵĵ�ų������군��","�����ѵÿ�����Ϣһ����","̫����","ץ��ʱ�临ϰ�ɣ�����","����"},
                new List<string>{"�о�����û���ֱ�Ӱ���~"},
                new List<string>{"����(����������ô�������)"},
                new List<string>{"����(����������ô�������)"}
            }),
            new NPC("�",2,new List<string>{"ʲô�������ڽ�����(ժ�¶���,�Ӵ���̽��ͷ��)","��Ҫ������","������������","(����˵��)"}),
            new NPC("����",3,new List<string>[7]{
                new List<string>{"�о���һ����ѧϰһ��������","����ȴ��ͻ�ˡ���"},
                new List<string>{"�������Ϻ�����������ֵ�����","������ҵĴ����"},
                new List<string>{"������ϰ���Ѱ�"},
                new List<string>{"�����ѵÿ�����Ϣһ����","(����)"},
                new List<string>{"(����)"},
                new List<string>{"�Ҿ����Ҵ���ǰ���"},
                new List<string>{"����(����������ô�������)"}
            }),
            new NPC("�",4,new List<string>{"���Ͼ�Ҫ������","˭������дһ���Ű������Ѿ�����Ӣ���ˡ���(˼ά������)"}),
            new NPC("����",5,new List<string>[7]{
                new List<string>{"�㸴ϰ����ô���ˣ�","�о�֮ǰ�ڵ��е�̫���ˣ����ڶ��첹�������ˡ���","(һЩ�����������������)Ҫ��...�Ǹ�������...����ϧ��֪��..."},
                new List<string>{"(һЩ�����������������)...����û...���������Ҫ��ʼ��...����...��ϰ��..."},
                new List<string>{"����(����������ô�������)"},
                new List<string>{"�����ѵÿ�����Ϣһ����","(����)"},
                new List<string>{"(һЩ�����������������)...�ѵõ�...��Ϣ...��һ��...�ҳ���..."},
                new List<string>{"����û���ҵ���..."},
                new List<string>{"��ǰ��������ʲô��","��ѧ����ԭ��������˵��ǰ��ĳλѧ�����������ѧ�ڵĿ�������������ʵ���ܽᣬ","�����Ǹ���һ�����е�","���Ҳ�û���ҵ��أ����ǿ�ϧ��"}
            }),
            new NPC("��ԭ",6,new List<string>[7]{
                new List<string>{"Ϊʲô����Ŀ��Զ�û��ԭ�Ⱑ����","�о��Ҵ����Ҫ����"},
                new List<string>{"����û���ҵ�","������Ҳ���븴ϰ��"},
                new List<string>{"�������ˣ�Ҫ��ʼ����һ����","�������Ÿոտ�ʼ��ϰ"},
                new List<string>{"����(����������ô�������)"},
                new List<string>{"����(����������ô�������)"},
                new List<string>{"����(����������ô�������)"},
                new List<string>{ "����(����������ô�������)" }
            }),
            new NPC("����",7,new List<string>[7]{
                new List<string>{"�ţ����㰡��","��Ӧ�ø�ϰ�ĺܲ���ɣ�"},
                new List<string>{"�����ƺ�������һЩ��ֵ�����","emmm����Ҳ˵��������ƺ��ǴӸ��ڴ���������"},
                new List<string>{"��һ�ſ��Ծ����������˰�����ϧ�һ���û���ҵ�ѧ��˵�Ķ�����"},
                new List<string>{"����(����������ô�������)"},
                new List<string>{"����(����������ô�������)"},
                new List<string>{"�����ž�����������"},
                new List<string>{ "����Ҫ��������" }
            })
            //new NPC("hls")
        };
        return NPCList;
    }

    public List<RandomEvent> initEventList()
    {
        List<RandomEvent> eventList = new List<RandomEvent>();
        eventList.Add(new RandomEvent("��ʱ�����\nӰ����̬\n", -5, -4, 0, new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 1, 9, 5 }, getEventSprite(0)));
        eventList.Add(new RandomEvent("����һ����ϲ���ĸ�\n������\n", 0, 3, 0, new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 1, 9, 20 }, getEventSprite(1)));
        eventList.Add(new RandomEvent("�����������һ������\n�㱻ι���˹���\n����ѧϰ\n", -2, -4, 0, new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 4, 14, 0 }, getEventSprite(2)));
        return eventList;
    }

    public List<StayRandomEvent> initStayEventList()
    {
        List<StayRandomEvent> stayEventList = new List<StayRandomEvent>();
        //Debug.Log(SourceManager.instance.NPCs.name);
        stayEventList.Add(new StayRandomEvent("żȻ����ͬѧ����ԭ��\nӰ����̬\n", 0, -6, 0,
            new int[5] { 0, 0, 0, 0, 0 }, new int[3] { 3, 18, 0 }, new int[3] { 3, 20, 0 },
            new GameObject[1] { dominateNPCs.transform.Find("npc5").gameObject }, getEventSprite(3)));
        stayEventList.Add(new StayRandomEvent("����������˺ܶ��˳�������\n���ܵ��˺ܴ�Ӱ��\n", 0, -6, 0,
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