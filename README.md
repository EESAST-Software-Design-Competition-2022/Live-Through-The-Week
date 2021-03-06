# Live-Through-The-Week

---

作者：刘彦宏、白立来、许鑫

---

## Live Through the Week 说明书

-----------------------

#### 功能文档

- 该软件为一款3D游戏。其游戏背景如下：

  > 你是某平行的方块世界的一所华清大学的一名普通大二学生。伴随着周五的最后一节课的下课铃声的响起，你即将迎来新的一周。在这一周，你将有5门考试，它们分别有不同的难度，有不同的学分，以及不同的考试时间，你需要合理安排你接下来的一周，尽可能地取得最好的成绩。游戏中以每一个小时为一个单位。

- 游戏功能

  1. 包括紫#公寓某号楼某层，教学楼的一间教室的3D地图，玩家可以在其中自由行走，与其中的NPC进行交互，与物品进行互动。并选择自己接下来的活动。

  2. 玩家拥有精力值，效率值，健康值，掌握度等多个属性。可以通过学习，休息，锻炼，睡觉等一系列活动来修改自己的属性。

  3. 游戏中会有一系列的随机事件。一些随机事件是随时间固定触发的，而另一些随机事件则是当玩家在特定时间经过某个NPC或者某个场景时触发（例如，考试结束后来到某个宿舍可能会听到同学在讨论刚才的考试题目的答案，这会影响玩家的心态）。

  4. 玩家可以收集一些关键物品，并在背包中查看这些物品。这些物品会影响最后的考试结果。

#### 软件使用说明

##### 按键说明

- W A S D分别控制玩家的前进，左转，后退，右转等。使用鼠标键来控制视角的移动
- E键为游戏的通用交互键，可以触发的交互事件有对话，学习，睡觉，拾取物品，锻炼等
- Q键为信息键，长按可以显示各个科目的掌握情况
- N键为下一句，在与NPC对话时可以使用
- Esc键为返回键，在进行对话，选择学习时间，打开背包等界面按该键可以退出
- B键为背包键，点击B后展开背包，可以查看当前拥有的物品。并使用物品或查看下一页。

##### 地图说明

- 现有的地图为某教室的地图与紫#公寓某号楼某层

- 通过宿舍楼尽头的楼梯或教室的门可以实现两个场景的互相切换

- 宿舍楼中的各个宿舍内有不同的NPC与物品，可以与之交互

- 游戏初始地点为主角所在宿舍的床位附近

  > 在主角的书桌附近与主角宿舍所在的中厅，主角可以进行学习
  >
  > 在主角的床的楼梯附近，主角可以进行睡觉或休息
  >
  > 在主角的另一台电脑附近，可以选择摸鱼
  >
  > 在宿舍楼的一个尽头的门口可以选择出门锻炼

#### 游戏开发代码介绍

- 本游戏使用了Unity游戏引擎开发，并使用C# 语言作为主要的开发语言。

  游戏中的模型少部分来自于Unity官方的Assert Store，大部分来自于Blender建模。

  游戏中的一些平面设计则使用到了Ps软件等

- 本软件利用了Unity官方提供的Plastic SCM进行版本控制。Plastic SCM为Unity官方支持的云端开发工具，其功能类似于Git，使用其能方便高效地进行游戏的版本控制与协同开发。

- 代码的建构信息如下

  1. 工具版本：Unity Editor 2020.3.25f1c1，Unity Hub 3.0.1-c3，Plastic SCM 11.0.16.6825
  2. 操作系统版本：Windows11

- 代码中最重要的三个类分别为 `GameManager`，`SourceManager`，`UIManager`。三个类均使用了全局单例模式来构建唯一的对象，这使得三者之间可以方便的互相调用。

  > `GameManager`为游戏的总控类。这个类处理游戏的主要逻辑，例如随机事件的发生的检查，时间控制系统的实现，学习，休息，锻炼，睡觉等一系列活动的数据处理。

  > `SourceManager`则是游戏资源的管理类。这个类下有游戏的几乎所有资源，例如NPC列表，随机事件列表，考试信息列表等等

  > `UIManager`负责接收`GameManager`的数据输出，并将其更新到游戏UI之中。这样实现了游戏的前后端分离。

  > 在`Player`类中，处理与其他NPC，电脑，床，门等一系列互动事件并修改UI。但具体的事件监控代码放在各自的类之中，例如`MyNPC`类中监控玩家是否正与NPC进行对话等。

- 游戏的物品系统使用了Unity的碰撞系统，列表与树的数据结构，距离检测系统等构建。

- 游戏的时间系统基于Unity的时间流逝系统构建。在`UIManager`中的`updateTime`方法会逐帧更新时间，并检查事件的发生。

- 游戏的随机事件系统与定点触发的随机事件系统依赖于时间系统，并利用列表数据结构构造
