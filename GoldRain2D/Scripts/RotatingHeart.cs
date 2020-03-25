using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingHeart : MonoBehaviour
{    
    public Rigidbody2D rb;
    float lastSpawntime;
    float coolDown=5f;
    float coolDownOffset;
    float minRnd = 1f;
    float maxRnd = 5f;
    
    void Start()
    {
        if (GameManager.Instance.isGameStarted())
        {
            //Debug.Log("Oyun başlamış görünüyor");
            ReLoc();
            //Debug.Log(">>" + (coolDown + coolDownOffset));
        }

    }
    void Update()
    {
        if (GameManager.Instance.isGameStarted()) {
            if (lastSpawntime + coolDown + coolDownOffset < Time.time)
            {
                ReSpawn();
                lastSpawntime = Time.time;
            }
        }        
    }
    
    public void ReLoc()
    {
        //Debug.Log("Re Loc Girdi");
        if (GameManager.Instance.isGameStarted())
        {
            //Debug.Log("Re Loc Çalıştı");
            rb.gravityScale = 0;
            this.transform.position = new Vector2(Random.Range(-7f, 7f), 6f);
            lastSpawntime = Time.time;
            coolDownOffset = Random.Range(minRnd, maxRnd);
            //Debug.Log(">>" + (coolDown + coolDownOffset));
        }
    }
    public void ReSpawn()
    {
        //Debug.Log("ReSpwan Girdi");
        if (GameManager.Instance.isGameStarted())
        {
            //Debug.Log("ReSpwan Çalıştı");
            rb.gravityScale = 3;
            coolDownOffset = Random.Range(minRnd, maxRnd);
            //Debug.Log(">>" + (coolDown + coolDownOffset));
            this.rb.velocity = new Vector2(0, 0);
            this.transform.position = new Vector2(Random.Range(-7f, 7), 6f);
        }
    }   
    public void SettActive(bool val)
    {     
        
        this.gameObject.SetActive(val);
        rb.gravityScale = GameManager.Instance.isGameStarted() ? 1 : 0;

    }
}
