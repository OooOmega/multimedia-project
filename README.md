# 三星堆考古项目


**小组成员：曾焓、杨佳雨、杨智宇、蔡永宁、曾寒阳**

**行业导师：钟雯、唐际根、但丹丹、肖懿洋、刘子彧**

**课程教授：刘江**


## 思维导图

![image](https://user-images.githubusercontent.com/58631573/172048639-0ce07b44-53ff-4369-a032-4f58c3fb3147.png)


## 背景介绍

### 三星堆背景

三星堆古遗址位于四川省广汉市西北的鸭子河南岸，分布面积12平方千米，距今已有3000至5000年历史，是迄今在西南地区发现的范围最大、延续时间最长、文化内涵最丰富的古城、古国、古蜀文化遗址。现有保存最完整的东、西、南城墙和月亮湾内城墙。三星堆遗址被称为20世纪人类最伟大的考古发现之一，昭示了长江流域与黄河流域一样，同属中华文明的母体，被誉为“长江文明之源”。

### 课题背景

南科大唐际根教授课题组与深圳博物馆合作，预计于2022年6月开设三星堆考古工作。希望以多媒体的方式，为大众展示三星堆祭祀全貌。南科大刘江教授的多媒体信息处理课程与唐教授课题组合作，以课程项目的形式帮助工作开展。


## 工具介绍

### Maya

MAYA软件是Autodesk旗下的著名三维建模和动画软件。Autodesk Maya可以大大提高电影、电视、游戏等领域开发、设计、创作的工作流效率。项目中使用MAYA处理模型文件、贴图文件。

### Unity 3D

Unity 3D 也称 Unity，是由 Unity Technologies 公司开发的一个让玩家轻松创建诸如三维视频游戏、建筑可视化、实时三维动画等类型互动内容的多平台的综合型游戏开发工具。

项目中使用Unity搭建祭祀场景，并根据祭祀场景摆放文物的位置,同时给每个文物设置点击动作,使用户在点击文物之后能够看到文物的模型和详细介绍信息.

### TensorFlowTTS

TensorFlowTTS是基于TensorFlow的语音生成架构，特点是借助Tensorflow2可以加快训练和推理的进度，同时TTS模型运行速度快，易于部署，在项目中使用预训练的tacotron模型和multiband-MelGAN声码器将项目中涉及到的文物的文字介绍转换成语音文件,并运用到我们搭建的祭祀场景中。


## 流程介绍

### 模型处理

首先对文物实体进行3d扫描，生成模型文件，贴图文件。再通过.mtl文件将贴图文件与模型文件结合，导入unity3D中获得可互动的3D模型。
![image](https://user-images.githubusercontent.com/58631573/172049097-6635349c-fe58-4643-a9ff-abeb7daf5cfc.png)

### 场景搭建

首先参考祭祀模拟图，确认模型在祭祀场景中的位置和方向。然后在unity3D中搭建真实祭祀环境，设计交互功能，多媒体音画，实现与文物之间的真实互动。

![image](https://user-images.githubusercontent.com/58631573/172049220-a65b9526-85e0-422e-b5d7-36d990cc1e64.png)
![image](https://user-images.githubusercontent.com/58631573/172049336-5cc18a06-09bc-436b-8068-a54c562bcecb.png)


## 代码说明

通过每一帧对鼠标点击的位置发射一条从点击位置垂直屏幕的射线，对场景中的对象进行射线检测。如果对象名称符合命名规范，则判定点中场景中摆放的文物，将场景跳转到单独对象的显示区域。
```
Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
RaycastHit hit;
if (Physics.Raycast(ray,out hit))
{
    string name = hit.collider.gameObject.name;
    if (name.Contains("("))
    {
        name = name.Remove(name.Length-4, 4);
    }
    window.gameObject.SetActive(true);
    window.ShowModel(name);
}
```
通过重写unity的Button方法，实现按钮的长按功能，在长按时调整mainCamera的位置实现全景展示。
```
    public ButtonClickedEvent my_onLongPress;
    public ButtonClickedEvent OnLongPress
    {
        get { return my_onLongPress; }
        set { my_onLongPress = value; }
    }
 
    private bool my_isStartPress = false;
    private float my_curPointDownTime = 0f;
    private float my_longPressTime = 0.6f;
    private bool my_longPressTrigger = false;
  
  private bool isStart = false;
 
    void Update()
    {
        if (isStart)
            my_onLongPress.Invoke();
    }
 
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        // my_curPointDownTime = Time.time;
        // my_isStartPress = true;
        // my_longPressTrigger = false;
        isStart = true;
    }
 
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isStart = false;
    }
 
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);  
    }
```

在点击模型跳转后，根据模型名称加载对应prefab，然后加载其相关描述并播放语音。界面中可以通过拖拽来旋转模型。
```
if (cd > 0)
{
    cd -= Time.deltaTime;
    return;
}
if (Input.GetMouseButtonDown(0))
{
    if (Input.mousePosition.x > 960)
        return;
    lastPos = Input.mousePosition;
}
if (Input.GetMouseButton(0))
{
    if (Input.mousePosition.x > 960)
        return;
    currentPos = Input.mousePosition;
    gameObject.transform.Rotate(Vector3.up, (lastPos.x - currentPos.x) * Time.deltaTime * rotateSpeed);
    gameObject.transform.Rotate(Vector3.left, (lastPos.y - currentPos.y) * Time.deltaTime * rotateSpeed);
    lastPos = Input.mousePosition;
}
```
UI管理
```
public UIViewState ViewState = UIViewState.Visible;
    //用于点击关闭的背景
    private GameObject bgObj;

    //初始化
    public virtual void initial()
    {
        OnHide();
    }
    //被显示时
    public virtual void OnShow()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (ViewState != UIViewState.Visible)
        {
            Vector3 pos = transform.localPosition;
            pos.z = 0;
            transform.localPosition = pos;

            ViewState = UIViewState.Visible;
        }

        UpdateView();
    }

    public virtual void OnShowWithBG()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (ViewState != UIViewState.Visible)
        {
            Vector3 pos = transform.localPosition;
            pos.z = 0;
            transform.localPosition = pos;

            ViewState = UIViewState.Visible;
        }

        UpdateView();
        InitBG();
    }

    //被隐藏
    public virtual void OnHide()
    {
        Vector3 pos = transform.localPosition;
        pos.z = -99999;
        transform.localPosition = pos;

        ViewState = UIViewState.Nonvisible;
        this.gameObject.SetActive(false);
    }

    public virtual void OnHideWithBG()
    {
        Vector3 pos = transform.localPosition;
        pos.z = -99999;
        transform.localPosition = pos;

        ViewState = UIViewState.Nonvisible;
        this.gameObject.SetActive(false);
        Destroy(bgObj);
    }

    //初始化背景(点击背景自动关闭界面)
    //未设置状态转移机，也就是倒回上一步
    protected void InitBG()
    {
        Transform bgTran = transform.Find("BG");
        bgObj = new GameObject("BG", typeof(RectTransform));
        bgTran = bgObj.transform;
        bgTran.SetParent(transform);
        bgTran.SetAsFirstSibling();
        RectTransform rt = bgObj.GetComponent<RectTransform>();
        Image img = bgTran.GetComponent<Image>();
        if (img == null)
        {
            img = bgTran.gameObject.AddComponent<Image>();
            img.color = new Color(0, 0, 0, 0f);
            CanvasRenderer cr = bgTran.GetComponent<CanvasRenderer>();
            cr.cullTransparentMesh = true;
        }
        img.raycastTarget = true;
        EventTrigger eventTrigger = bgTran.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = bgTran.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(e => clossePanel());
        eventTrigger.triggers.Add(entry);

    }

    protected void canntClickBG()
    {
        bgObj = new GameObject("BG", typeof(RectTransform));
        Transform bgTran = bgObj.transform;
        bgTran.SetParent(transform);
        bgTran.SetAsFirstSibling();
        RectTransform rt = bgObj.GetComponent<RectTransform>();
    }

    protected void closeCanntClickBG()
    {
        Destroy(bgObj);
    }

    protected virtual void clossePanel()
    {
        OnHideWithBG();
        Destroy(bgObj);
    }
```

## 收获总结

在做小组项目的过程中,我们广泛运用在南科大刘江教授的多媒体信息处理课程中学到的知识，并结合南科大唐际根教授课题组提供的考古知识，导入和交互3D模型文件，使用Unity搭建场景、完成多媒体互动，成功展示了模拟三星堆祭祀场景。


## 鸣谢

感谢南方科技大学计算机系刘江老师对于本项目提供的帮助

感谢考古课题组老师唐际根、钟雯、但丹丹、肖懿洋、刘子彧对于本项目提供的模型支持

感谢南方科技大学人文社科学院对于本项目提供的各项支持和帮助

感谢南方科技大学图书馆为本次项目展示提供场地

感谢南方科技大学计算机系对本项目提供的设备、技术支持
