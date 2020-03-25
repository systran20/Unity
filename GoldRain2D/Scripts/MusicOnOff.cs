using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnOff : MonoBehaviour
{  
    public Sprite musicOn;
    public Sprite musicOff;
    SpriteRenderer sr;
    bool musicState = true;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = musicOn;
    }
    void OnMouseUp()
    {
        //Debug.Log(sr.sprite);
        musicState = !musicState;
        if (musicState)
        {            
            sr.sprite = musicOn;
        }
        else {            
            sr.sprite = musicOff;
        }        
        //Debug.Log(sr.sprite);
        AudioManager.Instance.SetMusicLoop(musicState);
        GameManager.Instance.musicOn = musicState;
    }    
}
