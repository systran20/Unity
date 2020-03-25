using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject speakBallon;
    void Start()
    {
        ReSpawn();
    }
    
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speakBallon.SetActive(true);
        }
        
    }    
    public void Patladim(string x)
    {
        //Debug.Log("Patladim " + x);        
        animator.SetInteger("State", 0);
        speakBallon.SetActive(false);
        ReSpawn();
    }
    void ReSpawn()
    {
        //Debug.Log("respawn olmam lazım");
        rb.velocity = new Vector2(0, 0);
        rb.transform.position = new Vector2(Random.Range(-7f, 7f), 6f);
        
    }
    public void Activate()
    {
        FindObjectOfType<AudioManager>().PlaySoundEffect("Bomb");
        animator.SetInteger("State", 1);
        StopCoroutine(Bekle());
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        speakBallon.SetActive(true);
        StartCoroutine(Bekle());
        //Debug.Log("Bomba çarptı ---->>> " + collision.gameObject.tag);
        
    }
    IEnumerator Bekle()
    {        
        yield return new WaitForSeconds(5);
        //Debug.Log("Beklemekten patladım");
        Activate();
    }
    
    
}
