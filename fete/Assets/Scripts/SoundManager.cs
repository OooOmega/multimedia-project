using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour

{
    public static SoundManager instance;

    private readonly string m_path = "AudioAssets";
    //播放器
    private AudioSource effectSource;
    //音效片段
    private AudioClip[] EffectClips = null;
    //通过 K V 存储片段 方便管理
    private Dictionary<string, AudioClip> EffectDir;

    private string nowBGM = null;

    private void Start() {
        instance = this;
        init();
    }

    public void init()
    {
        //根节点
        GameObject go = new GameObject("SoundManager");
        go.transform.position = Vector3.zero;
        GameObject.DontDestroyOnLoad(go);

        //两个子节点，分别是音效和BGM

        //音效节点
        GameObject go2 = new GameObject("Effect");
        go2.transform.parent = go.transform;

        effectSource = go2.AddComponent<AudioSource>();
        //设置属性
        effectSource.loop = false;
        effectSource.spatialBlend = 0.0f;//2d声音

        EffectDir = new Dictionary<string, AudioClip>();
        //加载所有到音效
        EffectClips = Resources.LoadAll<AudioClip>("mp3");

        for (int i = 0; i < EffectClips.Length; i++)
        {
            //处理对应名字
            var str = EffectClips[i].ToString();
            str = str.Substring(0, str.Length - 24);
            EffectDir.Add(str, EffectClips[i]);
        }

        //测试
        //PlayBGM("SoulFlower");
    }

    /// <summary>
    /// 播放一次音效
    /// </summary>
    /// <param name="_audioName">音效名</param>
    public void PlayerSoundEffect(string _audioName)
    {
        AudioClip clip = null;
        EffectDir.TryGetValue(_audioName, out clip);
        if (clip == null)
        {
            Debug.Log("名称为：" + _audioName + "的音频文件不存在！");
            return;
        }

        effectSource.PlayOneShot(clip);
    }


    public void CloseEffectSound()
    {
        this.effectSource.Stop();
    }
}