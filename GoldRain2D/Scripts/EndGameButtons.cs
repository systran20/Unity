using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// GAME OVER SCREEN BUTTONS
/// </summary>

public class EndGameButtons : MonoBehaviour
{    
    public Button btnYeter;
    public Button btnOyna;
    

    void Start()
    {
        //bu ekran açıldıysa game over ekranındasın demektir.        
        AudioManager.Instance.StopAll();
        AudioManager.Instance.PlayGameOverMusic();       
        Button btn = btnYeter;
        btn.onClick.AddListener(Yeter);
        btn = btnOyna;
        btn.onClick.AddListener(Oyna);
    }

    void Yeter()
    {
        //Debug.Log("Yeter");
        Application.Quit();
    }
    void Oyna()
    {
        SceneManager.LoadScene(0);
    }
}