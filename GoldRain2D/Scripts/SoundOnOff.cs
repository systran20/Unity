using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnOff : MonoBehaviour
{    
    public Sprite soundOn;
    public Sprite soundOff;
    SpriteRenderer sr;
    bool soundState = true;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = soundOn;
    }
    void OnMouseUp()
    {        
        soundState = !soundState;
        if (soundState)
        {
            sr.sprite = soundOn;
        }
        else {
            sr.sprite = soundOff;
        }
        Debug.Log(sr.sprite);
        GameManager.Instance.soundEffectsOn = soundState;
        
    }    
}
