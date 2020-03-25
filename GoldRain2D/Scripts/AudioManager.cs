using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour    
{
    private static AudioManager _instance;      //SINGLETON İÇİN
    public static AudioManager Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new AudioManager();
            }
            return _instance;
        }
    }

    [Header("Oyun içi ses efektleri")]
    public Sound[] sounds;

    [Header("Oyun içi Müzikler")]
    public Music[] musics;

    private List<string> randomLoops = new List<string>();
    private string nowPlaying=null;
    private bool isMusicOn = true;
    private Music currentMusic;
    void OnDestroy()
    {
        Debug.Log("OnDestroy AudioManager");
    }
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            //s.source.loop = s.loop;   GEREK KALMADI
            //Debug.Log(s.name + " -->POA--> " + s.pOnScene);            
        }
        foreach (Music m in musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.loop = m.loop;
            if (m.name=="Intro")
            {
                currentMusic = m;
                m.source.Play();
            }            
            if (m.name.Substring(0,4)=="Loop")
            {
                Debug.Log(m.name + " randomLoopa eklendi");
                randomLoops.Add(m.name);
            }
        }
    } 
    void Update()
    {
        if (!currentMusic.source.isPlaying && isMusicOn)
        {
            PlayMusicLoop();
        }

    }

    public void PlaySoundEffect(string name)
    {
        if (GameManager.Instance.soundEffectsOn)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Scene: " + SceneManager.GetActiveScene().buildIndex + ",  Sound " + name + " not found!");
                return;
            }
            s.source.Play();
        }
    }
    
    public void PlayMusicLoop()
    {
        StopAll();
        isMusicOn = true;
        int rndVal= UnityEngine.Random.Range(1, randomLoops.Count+1);
        Debug.Log("RndVal:" + rndVal);                
        foreach (Music m in musics)
        {
            if (m.name.Substring(4,1)==rndVal.ToString())
            {
                m.source.Play();
                nowPlaying = m.name;
                currentMusic = m;
                return;
            }
        }
    }

    public void PlayGameOverMusic()
    {
        Debug.Log("PlayGameOverMusic");
        foreach (Music m in musics)
        {          
            if (m.name == "GameOver")
            {
                m.source.Play();
                return;
            }
        }
    }
    public void StopAll()
    {   //Debug.Log("StopAll");
        foreach(Sound s in sounds)
        {
            s.source.Stop();            
        }
        foreach (Music m in musics)
        {
            m.source.Stop();
        }
        isMusicOn = false;
        nowPlaying = null;
    }

    public void SetMusicLoop(bool val)
    {
        Debug.Log("GELEN VAL: " + val);
        if (val)
        {
            isMusicOn = true;
            PlayMusicLoop();
        }
        else
        {
            isMusicOn = false;
            StopAll();
        }
        
    }

    IEnumerator PlayNext()
    {
        /*
        audio.clip = engineStartClip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = engineLoopClip;
        audio.Play();
        */
        yield return new WaitForSeconds(0.1f);
    }
    
}
