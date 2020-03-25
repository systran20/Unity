using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthBeat : MonoBehaviour
{
    public SpriteRenderer sr;    
    void Start()
    {
        //Debug.Log("HB size -> " + sr.size);
    }

    // Update is called once per frame
    void Update()
    {
        sr.size += new Vector2(5f, 5f);
    }
}
