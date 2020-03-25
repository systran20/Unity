using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// GAME OVER SCENE İÇİN 
/// </summary>
public class GetScore : MonoBehaviour
{
    public Text txtScore_P1;
    public Text txtScore_P2;
    public Text txtCoop_Score;

    void Awake()
    {
        txtScore_P1.enabled = false;
        txtScore_P2.enabled = false;
        txtCoop_Score.enabled = false;
    }
    void Start()
    {        
        string  gm =PlayerPrefs.GetString("GM");        
        if (gm==GameModes.OnePlayer.ToString())
        {
            txtScore_P1.enabled = true;
            txtScore_P1.text = "Player 1 : " + PlayerPrefs.GetInt("P1_Score").ToString();
        }
        else if (gm == GameModes.TwoPlayer.ToString())
        {
            txtScore_P1.enabled = true;
            txtScore_P1.text = "Player 1 : " + PlayerPrefs.GetInt("P1_Score").ToString();
            txtScore_P2.enabled = true;
            txtScore_P2.text = "Player 2 : " + PlayerPrefs.GetInt("P2_Score").ToString();
        }
        else if(gm == GameModes.Coop.ToString())
        {
            txtCoop_Score.enabled = true;
            txtCoop_Score.text = "Ortak : " + PlayerPrefs.GetInt("Coop_Score").ToString();
        }        
    }   
    
}
