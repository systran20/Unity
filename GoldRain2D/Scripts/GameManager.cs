using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;      //SINGLETON İÇİN
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }
    private static int _coop_score = 0;    
    public GameModes gameMode;
    public HealtBarScript ortakHp;
    public RotatingHeart heal;

    public GameObject Player1, Player2, P1_Hud, P2_Hud, Ortak_Hud;
    public GameObject staticCoin, staticHeart, Coin, Bomb, Heart;
    public Text txtOrtakScore;

    public GameObject Sprite_1P, Sprite_2P, Sprite_Coop, Sprite_Start;
    public bool musicOn { get; set; }
    public bool soundEffectsOn { get; set; }

    private IEnumerator koRutin;
    public float offset = 1.2f;
    public int _CoopCurrentHp;
    public int _CoopMaxHp = 100;
    public bool gameStarted;   
    void Awake()
    {
        musicOn = true;
        soundEffectsOn = true;
    }

    void Start()
    {        
        koRutin = Bekle(2.0f);
        PlayerPrefs.SetInt("P1_Score", 0);
        PlayerPrefs.SetInt("P2_Score", 0);
        PlayerPrefs.SetInt("Coop_Score", 0);
        _CoopCurrentHp = _CoopMaxHp;
        ortakHp.SetMaxHealth(_CoopMaxHp);
        
        Sprite_1P.SetActive(true);
        Sprite_2P.SetActive(true);
        Sprite_Coop.SetActive(true);
        Sprite_Start.SetActive(true);        
        Coin.SetActive(false);
        Bomb.SetActive(false);
        //Heart.SetActive(false);        
        Player1.SetActive(false);
        Player2.SetActive(false);
        AudioManager.Instance.PlaySoundEffect("Selection");
        SetOnePlayer();
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))         {
            PauseScene();
        }        
    }    

    private void SetOnePlayer()
    {
        //Debug.Log("Set One Player");
        gameMode = GameModes.OnePlayer;
        P1_Hud.SetActive(true);
        P2_Hud.SetActive(false);
        Ortak_Hud.SetActive(false);

        AudioManager.Instance.PlaySoundEffect("Selection");
        staticCoin.transform.position = new Vector2(Sprite_1P.transform.position.x, Sprite_1P.transform.position.y + offset);
    }

    private void SetTwoPlayer()
    {
        //Debug.Log("Set Two Player");
        gameMode = GameModes.TwoPlayer;
        P1_Hud.SetActive(true);
        P2_Hud.SetActive(true);
        Ortak_Hud.SetActive(false);
        staticCoin.transform.position = new Vector2(Sprite_2P.transform.position.x, Sprite_2P.transform.position.y + offset);
        
    }

    private void SetCoop()
    {
        //Debug.Log("Set Coop");
        gameMode = GameModes.Coop;
        P1_Hud.SetActive(false);
        P2_Hud.SetActive(false);
        Ortak_Hud.SetActive(true);
        staticCoin.transform.position = new Vector2(Sprite_Coop.transform.position.x, Sprite_Coop.transform.position.y + offset);
    }

    private void HideGui()
    {
        FindObjectOfType<AudioManager>().PlaySoundEffect("Start");
        GameObject.Find("GameModeSprites").SetActive(false);
        staticCoin.SetActive(false);
        Coin.SetActive(true);
        Bomb.SetActive(true);
    }

    public void StartGame()
    {
        gameStarted = true;
        AudioManager.Instance.PlaySoundEffect("Start");
        StartCoroutine(koRutin);

        

    }
    public void PauseScene()
    {
        Debug.Log("Sprice Click Pause Scene");
    }
    
    public void SetGameMode(GameModes val)
    {
        gameStarted = false;
        gameMode = val;
        AudioManager.Instance.PlaySoundEffect("Selection");
        switch(val)
        {
            case GameModes.OnePlayer:
                SetOnePlayer();
                break;
            case GameModes.TwoPlayer:
                SetTwoPlayer();
                break;
            case GameModes.Coop:
                SetCoop();
                break;
            case GameModes.Start:
                StartGame();
                break;
        }
        //Debug.Log("Set Game Mode" + gameMode);
    }    

    public GameModes GetGameMode()
    {
        return gameMode;
    }

    public void SetCoopScore()
    {
        
        _coop_score++;
        PlayerPrefs.SetInt("Coop_Score", _coop_score);
        
        txtOrtakScore.text = _coop_score.ToString();
    }

    public void SetCoopDamage(int damage)
    {
        _CoopCurrentHp -= damage;
        ortakHp.SetHealth(_CoopCurrentHp);                
        if (_CoopCurrentHp <= 0f)
        {
            SceneManager.LoadScene(1);
            //TODO:audioManager.PlayGameOver();
        }
    }

    public void SetCoopHealth(int val)
    {
        _CoopCurrentHp += val;
        
        if (_CoopCurrentHp > _CoopMaxHp)
        {
            _CoopCurrentHp = _CoopMaxHp;            
        }
        ortakHp.SetHealth(_CoopCurrentHp);
        heal.ReLoc();
    }

    public bool isGameStarted()
    {
        //Debug.Log("isGameStarted ->" + gameStarted);
        return gameStarted;
    }
    
    IEnumerator Bekle(float sec)
    {

        yield return new WaitForSeconds(sec);
        Debug.Log(sec.ToString() + " Bekledim");
        //Debug.Log("StartGame()");
        PlayerPrefs.SetString("GM", gameMode.ToString());
        PlayerPrefs.SetInt("Coop_Score", 0);
        AudioManager.Instance.StopAll();
        Sprite_1P.SetActive(false);
        Sprite_2P.SetActive(false);
        staticCoin.SetActive(false);
        staticHeart.SetActive(false);

        Sprite_Coop.SetActive(false);
        Sprite_Start.SetActive(false);
        //Debug.Log("<<" + gameMode + ">>");
        switch (gameMode)
        {
            case GameModes.OnePlayer:
                Player1.SetActive(true);
                Coin.SetActive(true);
                Bomb.SetActive(true);
                break;
            case GameModes.TwoPlayer:
                Player1.SetActive(true);
                Player2.SetActive(true);
                Coin.SetActive(true);
                Bomb.SetActive(true);
                break;
            case GameModes.Coop:
                Player1.SetActive(true);
                Player2.SetActive(true);
                Coin.SetActive(true);
                Bomb.SetActive(true);
                break;
        }
        AudioManager.Instance.PlayMusicLoop();
    }

    /*
    public void SetHeartActive(bool val)
    {
        heartActive = val;
    }

    public bool GetHeartActive()
    {
        return heartActive;
    }
    */

}
