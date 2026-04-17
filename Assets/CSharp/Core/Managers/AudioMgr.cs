using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioMgr : BaseMgr<AudioMgr>
{
    private float BackMusicVolume = 0.1f;
    private float soundVolume = 0.1f;
    private bool SoundIsPlay = true;
    private AudioSource BackMusic = null;
    private GameObject SoundObj = null;
    private List<AudioSource> soundlist = new List<AudioSource>();
    private AudioMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }
    private void Update()
    {
        if (!SoundIsPlay) return;
        for (int i = soundlist.Count - 1; i >= 0; i--)
        {
            if (!soundlist[i].isPlaying)
            {
                GameObject.Destroy(soundlist[i]);
                soundlist.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayerBackMusic(string name)
    {
        if (BackMusic == null)
        {
            GameObject obj = new GameObject("BackMusic");
            GameObject.DontDestroyOnLoad(obj);
            BackMusic = obj.AddComponent<AudioSource>();
        }
        BackMusic.clip = Resources.Load<AudioClip>("Music/" + name);
        BackMusic.Play();
        BackMusic.loop = true;
        BackMusic.volume = BackMusicVolume;
    }
    /// <summary>
    /// 关闭背景音乐
    /// </summary>
    public void StopBackMusic()
    {
        if (BackMusic != null)
        {
            BackMusic.Stop();
        }
    }
    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBackMusic()
    {
        if (BackMusic != null)
        {
            BackMusic.Pause();
        }
    }
    /// <summary>
    /// 改变背景音乐音量
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeBackMusicVolume(float volume)
    {
        BackMusicVolume = volume;
        if (BackMusic != null)
        {
            BackMusic.volume = volume;
        }
    }
    /// <summary>
    /// 清空背景音乐
    /// </summary>
    public void ClearBackMusic()
    {
        if (BackMusic != null)
        {
            BackMusic.Stop();
            GameObject.Destroy(BackMusic);
            BackMusic = null;
        }
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isLoop"></param>
    /// <param name="callback"></param>
    public void PlaySound(string name, bool isLoop = false, UnityAction<AudioSource> callback = null)
    {
        if (SoundObj == null)
        {
            SoundObj = new GameObject("SoundObj");
        }
        AudioSource audio = SoundObj.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Music/" + name);
        audio.loop = isLoop;
        audio.volume = soundVolume;
        audio.Play();
        soundlist.Add(audio);
        if (callback != null)
        {
            callback?.Invoke(audio);
        }
    }
    /// <summary>
    /// 关闭指定音效
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        if (soundlist.Contains(source))
        {
            source.Stop();
            soundlist.Remove(source);
            GameObject.Destroy(source);
        }
    }
    /// <summary>
    /// 改变音效音量
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeSoundVolume(float volume)
    {
        soundVolume = volume;
        for (int i = 0; i < soundlist.Count; i++)
        {
            soundlist[i].volume = volume;
        }
    }
    /// <summary>
    /// 暂停音效
    /// </summary>
    /// <param name="isPlay"></param>
    public void PauseSound(bool isPlay)
    {
        if (isPlay)
        {
            SoundIsPlay = true;
            for (int i = 0; i < soundlist.Count; i++)
            {
                soundlist[i].Play();
            }
        }
        else
        {
            SoundIsPlay = false;
            for (int i = 0; i < soundlist.Count; i++)
            {
                soundlist[i].Pause();
            }
        }
    }
    /// <summary>
    /// 清空音效
    /// </summary>
    public void ClearSound()
    {
        for (int i = 0; i < soundlist.Count; i++)
        {
            soundlist[i].Stop();
            soundlist[i].clip = null;
            GameObject.Destroy(soundlist[i]);
        }
        soundlist.Clear();
    }
}
