using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player
{
    private float energy { get; set; }//精力值
    private float efficiency { get; set; }//效率值
    private int[] ability { get; set; }//能力值
    private float health { get; set; }//健康值
    private float mentality;//心态值

    public List<int> thingsList;
    public Player()
    {
        energy = 80;
        efficiency = 80;
        ability = new int[5] { 20, 20, 20, 20, 20 };
        health = 80;
        //mentality = 100;
        thingsList = new List<int>() { 11};
    }

    public int[] getAbility()
    {
        return ability;
    }

    public void learn(float hour, int subject)
    {
        float[] effect = new float[4] { 1.5f, 1.2f, 1, 1 };
        if (UIManager.instance.trueTime[1] >= 23 || UIManager.instance.trueTime[1] < 5)
        {
            learnTired(hour, subject, effect);
        }
        else
        {
            learnNormal(hour, subject, effect);
        }
    }

    private void learnTired(float hour, int subject, float[] effect)
    {
        float[] subjectcoeff = new float[5] {3, 4, 4, 5, 5 };
        float loss = 1f;
        float add = 1f;
        float learnTime = hour;
        float tiredk = 1.3f;
        if (energy - learnTime * tiredk * add * effect[0] < 0)
        {
            energy = 0;
        }
        else
        {
            energy -= learnTime * tiredk * add * effect[0];
        }
        if (efficiency - learnTime * tiredk * add * effect[1] < 0)
        {
            efficiency = 0;
        }
        else
        {
            efficiency -= learnTime * tiredk * add * effect[1];
        }
        if (health - learnTime * tiredk * add * effect[2] < 0)
        {
            health = 0;
        }
        else
        {
            health -= learnTime * tiredk * add * effect[2];
        }
        int addition = (int)(effect[3] * loss * learnTime * (efficiency / 80) * subjectcoeff[subject]) ;
        if (ability[subject] + addition > 100)
        {
            ability[subject] = 100;
        }
        else
        {
            ability[subject] += addition;
        }
        this.updateAll();
        checkcoeff(energy, efficiency, health);
    }

    private void learnNormal(float hour, int subject, float[] effect)
    {
        float[] subjectcoeff = new float[5] { 3, 4, 4, 5, 5 };
        float loss = 1f;
        float add = 1f;
        float learnTime = hour;
        if (energy - learnTime * add * effect[0] < 0)
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
        int addition = (int)(effect[3] * loss * learnTime * (efficiency / 80) * subjectcoeff[subject]);
        if (ability[subject] + addition > 100)
        {
            ability[subject] = 100;
        }
        else
        {
            ability[subject] += addition;
        }
        this.updateAll();
        checkcoeff(energy, efficiency, health);
    }


    public void play(float hour,int type)
    {
        switch (type)
        {
            case 0:
                //Debug.Log("逛B站" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                //if(hour > 10)
                //{                   
                //}
                commit(-(int)(1.3 * hour), -(int)(1.2 * 1.3 * hour), -(int)hour);
                break;
            case 1:
                //Debug.Log("游戏" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                commit(-(int)(1.3 * hour), -(int)(1.2 * 1.3 * hour), -(int)hour);
                break;
            case 2:
                //Debug.Log("发呆" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                commit(-(int)(1.3 * hour), -(int)(1.2 * 1.3 * hour), -(int)hour);
                break;
            case 3:
                //Debug.Log("短视频" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                commit(-(int)(1.3 * hour), -(int)(1.2 * 1.3 * hour), -(int)hour);
                break;
            case 4:
                //Debug.Log("购物" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                commit(-(int)(1.3 * hour), -(int)(1.2 * 1.3 * hour), -(int)hour);
                break;
        }
        this.energy -= hour;
        this.efficiency -= hour;
        this.health -= hour;
    }


    public void exercise(float hour,int type)
    {
        switch (type)
        {
            case 0:
                //Debug.Log("跑步" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                break;
            case 1:
                //Debug.Log("羽毛球" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                break;
            case 2:
                //Debug.Log("篮球" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                break;
            case 3:
                //Debug.Log("手球" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                break;
            case 4:
                //Debug.Log("游泳" + hoursAndMinutes[0] + "h:" + hoursAndMinutes[1] + "min");
                break;
        }
        this.energy -= 2.5f * hour;
        this.efficiency -= 0.8f * 2.5f * hour;
        this.health -= 2.5f * hour;
        checkcoeff(energy, efficiency, health);
    }

    //public void rest(float hour)
    //{
    //    this.energy += 3 * hour;
    //    this.health += 1.5f * hour;
    //    this.efficiency += 2.5f * hour;
    //}

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
        else
        {
            restNormal(minutes);
        }

        energy = energy >= 100 ? 100 : energy;
        efficiency = efficiency >= 100 ? 100 : efficiency;
        health = health >= 100 ? 100 : health;
        energy = (int)energy;
        efficiency = (int)efficiency;
        health = (int)health;
        updateAll();
        checkcoeff(energy, efficiency, health);
    }

    private void restTired(int minutes)
    {
        float sleepcoeff = 0.8f;
        if (minutes >= 7 * 60)
        {
            this.energy += 3 * minutes / 60;
            this.efficiency += 2.5f * minutes / 60;
            this.health = this.health > 60 ? this.health + 1.5f * minutes / 60 : this.health + 1.5f * minutes / 60 + 3;
        }
        else
        {
            this.energy += 3 * sleepcoeff * minutes / 60;
            this.efficiency += 2.5f * sleepcoeff * minutes / 60;
            this.health = this.health > 60 ? this.health + 1.5f * sleepcoeff * minutes / 60 : this.health + 1.5f * sleepcoeff * minutes / 60 + 3;
        }
        checkcoeff(energy, efficiency, health);
    }

    private void restNormal(int minutes)
    {
        float sleepcoeff = 0.9f;
        if (minutes >= 7 * 60)
        {
            this.energy += 3 * minutes/60;
            this.efficiency += 2.5f * minutes / 60;
            this.health = this.health > 60 ? this.health + 1.5f * minutes / 60 : this.health + 1.5f * minutes / 60 +3;
        }
        else
        {
            this.energy += 3 * sleepcoeff * minutes / 60;
            this.efficiency += 2.5f * sleepcoeff * minutes / 60;
            this.health = this.health > 60 ? this.health + 1.5f * sleepcoeff * minutes / 60 : this.health + 1.5f * sleepcoeff * minutes / 60 + 3;
        }
        checkcoeff(energy, efficiency, health);
    }

    private void restNoon(int minutes)
    {
        float sleepcoeff = 2;
        this.energy += 3 * sleepcoeff * minutes / 60;
        this.efficiency += 2.5f * sleepcoeff * minutes / 60;
        this.health = this.health > 60 ? this.health + 1.5f * sleepcoeff * minutes / 60 : this.health + 1.5f * sleepcoeff * minutes / 60 + 3;

        //this.energy = this.energy > 95 ? 100 : 95;
        //this.efficiency = this.efficiency > 95 ? 100 : 95;
        //this.health = this.health > 70 ? (this.health < 90 ? this.health + 10 : 100) : (this.health + 20);
    }

    public string getStatusExceptAblility()
    {
        return "精力值：" + (int)energy
            + "\n效率值：" + (int)efficiency
            + "\n健康值：" + (int)health;
    }

    public void commit(int energyChange, int efficiencyChange, int healthChange)
    {
        this.energy += energyChange;
        this.efficiency += efficiencyChange;
        this.health += healthChange;
        if (energy < 0)
        {
            energy = 0;
        }
        else if (energy > 100)
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
        checkcoeff(energy, efficiency, health);
    }

    public void commit(int energyChange, int efficiencyChange, int healthChange, int[] abilityChange)
    {
        this.energy += energyChange;
        this.efficiency += efficiencyChange;
        this.health += healthChange;
        if (energy < 0)
        {
            energy = 0;
        }
        else if (energy > 100)
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
        if (abilityChange.Length != 5)
        {
            return;
        }
        else
        {
            for (int i = 0; i < abilityChange.Length; i++)
            {
                ability[i] += abilityChange[i];
                if (ability[i] < 0)
                {
                    ability[i] = 0;
                }
                else if (ability[i] > 100)
                {
                    ability[i] = 100;
                }
            }
        }
        UIManager.instance.updateAllAbility(this.getAbility());
        checkcoeff(energy,efficiency,health);
    }

    public void checkcoeff(float energy, float effciency, float health)
    {
        string tiredover = "长时间的疲劳使你的腰杆不再挺直\n使你的身板不再健壮\n恭喜你，小伙\n你喜提医院床位一张\n并由此从魔鬼期末周中幸存";
        if (energy <= 5|| health <= 5)
            GameManager.instance.checkOver(tiredover);
    }
    
    private void updateAll()
    {
        UIManager.instance.updateAllAbility(this.getAbility());
        UIManager.instance.updateStatus(this.getStatusExceptAblility());
    }

    public void commitHealth(int change)
    {
        health += change;
        if(health < 0)
        {
            health = 0;
        }
        else if(health > 100)
        {
            health = 100;
        }
    }
}
