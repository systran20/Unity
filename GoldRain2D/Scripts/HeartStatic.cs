using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartStatic : MonoBehaviour
{
    public bool aktif = false;
    SpriteRenderer sr;
    public RotatingHeart rh;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = aktif;
    }
    void OnMouseDown()
    {
        if (aktif)
        {
            aktif = false;
            sr.color = Color.gray;            
            rh.SettActive(aktif);            
        }
        else
        {
            aktif = true;
            sr.color = Color.white;            
            rh.SettActive(aktif);
        }
    }
}