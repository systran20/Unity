using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteClickPrefab : MonoBehaviour
{    
    public GameModes gm;   
    
    void OnMouseDown()
    {
        if (this.name=="Start")
        {            
            FindObjectOfType<GameManager>().StartGame();            
        }
        else
        {
            FindObjectOfType<GameManager>().SetGameMode(gm);
        }
        
        
    }
}
