using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Rigidbody2D rb;
    float currentGravityScale;
    
    /// <summary>
    /// Start Methodu
    /// </summary>
    void Start()
    {        
        this.transform.position = new Vector2(Random.Range(-7f, 7f), 6f);
    }
    public void ReSpawn()
    {
        //Debug.Log("respawnn");
        this.rb.velocity = new Vector2(0, 0);
        this.transform.position = new Vector2(Random.Range(-7f, 7), 6f);
    }

}
