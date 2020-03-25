using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Player 1 Script
/// </summary>


public class PlayerController : MonoBehaviour
{
    public enum PlayerTypes
    {
        Player1,
        Player2
    }

    [Header("Player1 WASD / Player2 ARROWS")]
    public PlayerTypes playerType;   
    public float moveSpeed = 5f;

    //Object Reference not set to an instance hatası VERMEMESİ İÇİN editör ekranındayken
    //Hiyerarşi penceresinden Player nesnesini seç
    //ilgili oyun nesnesini tutup İnspector penceresindeki uygun yere bağlamak lazım
    
    
    public Bomb bomb;
    public Coin coin;
    public HealtBarScript hp;
    public RotatingHeart heal;
    public Text txtScore;
    

    //zaten bu nesnenin içinde oldukları için public yapıpi elle editörden bağlamaya gerek yok
    //private yap, Start methodunda GetComponent ile al
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;

    Vector2 movement;
    int _score = 0;
    int _coopScore = 0;
    int _MaxHealth = 100;
    int _currentHealth;
    int _damage = 20;
    int _heal = 10;

    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _score = 0;
        _currentHealth = _MaxHealth;
        hp.SetMaxHealth(_MaxHealth);
        //Debug.Log(FindObjectOfType<GameManager>().GetGameMode());
    }

    void Update()
    {
        if(playerType==PlayerTypes.Player1)
        {
            movement.x = Input.GetAxis("P1_Horizontal");
            if (rb.position.x < -7)
            {
                rb.position = new Vector2(-7f, rb.position.y);
            }
            if (rb.position.x > 7)
            {
                rb.position = new Vector2(7f, rb.position.y);
            }
            //txtScore.text = _score.ToString();
            animator.SetFloat("P_Speed", Mathf.Abs(movement.x));
        }
        else if (playerType==PlayerTypes.Player2)
        {
            movement.x = Input.GetAxis("P2_Horizontal");
            if (rb.position.x < -7)
            {
                rb.position = new Vector2(-7f, rb.position.y);
            }
            if (rb.position.x > 7)
            {
                rb.position = new Vector2(7f, rb.position.y);
            }
            //skor text nesnesine burada erişiyor            
            animator.SetFloat("P_Speed", Mathf.Abs(movement.x));
        }       
        
    }

    //MOVEMENT
    void FixedUpdate()
{
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "coin":
                AddScoreOne();
                break;
            case "bomb":
                GetDamage(_damage);
                FindObjectOfType<AudioManager>().PlaySoundEffect("Boing");
                break;
            case "health":
                AddHealth(_heal);
                FindObjectOfType<AudioManager>().PlaySoundEffect("Heal");
                break;
            default:
                break;
        }
    }

    void OnEnable()
    {
        //Debug.Log(this.name + "-> On Enable");
        spriteRenderer = GetComponent<SpriteRenderer>();
        //EĞER PLAYER2 isem Sprite Rengimi KIRMIZ TON YAP 
        if (playerType == PlayerTypes.Player2)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            //player 1 isem
            spriteRenderer.color = Color.white;
        }
    }
    void OnValidate()
    {
        //Debug.Log(this.name + "-> On Validate");
        spriteRenderer = GetComponent<SpriteRenderer>();
        //EĞER PLAYER2 isem Sprite Rengimi KIRMIZ TON YAP 
        if (playerType == PlayerTypes.Player2)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            //player 1 isem
            spriteRenderer.color = Color.white;
        }

    }

    public void AddScoreOne()
    {
        if (GameManager.Instance.GetGameMode()==GameModes.Coop)   //COOP MODE
        {

            _coopScore++;
            GameManager.Instance.SetCoopScore();
        }
        else
        {
            _score++;
            txtScore.text = _score.ToString();
            if (playerType == PlayerTypes.Player1)
            {                
                PlayerPrefs.SetInt("P1_Score", _score);
            }
            else
            {
                PlayerPrefs.SetInt("P2_Score", _score);
            }
        }        
        FindObjectOfType<AudioManager>().PlaySoundEffect("Coin");
        coin.ReSpawn();
    }
    public void GetDamage(int damage)
    {
        if (GameManager.Instance.GetGameMode() == GameModes.Coop)   //COOP MODE
        {
            //Ortak HASAR AL
            GameManager.Instance.SetCoopDamage(damage);
        }
        else
        {
            _currentHealth -= damage;
            hp.SetHealth(_currentHealth);
            if (_currentHealth <= 0f)
            {
                SceneManager.LoadScene(1);
                FindObjectOfType<AudioManager>().PlayGameOverMusic();
            }
            bomb.Activate();
        }
    }
    public void AddHealth(int val)
    {
        if (GameManager.Instance.GetGameMode() == GameModes.Coop)   //COOP MODE
        {
            //ortak kalp al
            GameManager.Instance.SetCoopHealth(val);
        }
        else
        {
            _currentHealth += val;
            if (_currentHealth > _MaxHealth)
            {
                _currentHealth = _MaxHealth;
            }
            hp.SetHealth(_currentHealth);
            heal.ReLoc();
            
        }
    }
}
