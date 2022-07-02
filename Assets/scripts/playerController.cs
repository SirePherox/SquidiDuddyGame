using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerController : MonoBehaviour
{
    //variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    
    public bool gameOver;
    public bool playerWon;
    public bool playerEnterTrigger1;
    public int playerLives = 3;
    public AudioClip stakeSound;
    public AudioClip bulletSound;
    public AudioClip dieSound;


    private Vector3 moveDirection;
    
    //references
    private CharacterController controller;
    private Animator playerAnim;
    public TextMeshProUGUI livesText;
    private AudioSource playerAudio;
    public GameObject winScreen;
    private gameController gameScript;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameScript = GameObject.Find("gameManager").GetComponent<gameController>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameScript.isTimeUp && !gameScript.isGameOver)
        {
            Move();

            //making sure the player starts the game for sometime before calling timeUP
            gameScript.timePlayer -= Time.deltaTime;
            if(gameScript.timePlayer < 1)
            {
                gameScript.timePlayer = 0;
            }
        }
        
    }

    private void Update()
    {
        livesText.text = "Lives: " + playerLives;

        //makes sure player lives doesnt become negative
        if(playerLives < 1)
        {
            playerLives = 0;
            //playerAudio.PlayOneShot(dieSound, 1.0f);
        }

        if (playerWon)
        {
            playerAnim.SetTrigger("Jump_trig");
            winScreen.SetActive(true);
        }
    }

    private void Move()
    {
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(moveX, 0, moveZ);

        //if game isnt over , move user 
        if (!gameOver && !playerWon)
        {
            if (moveDirection == Vector3.zero)
            {
                Idle();
            }
            else if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }

            moveDirection *= moveSpeed;
            controller.Move(moveDirection * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if user collides with bullets
        if (other.CompareTag("bullet"))
        {
            playerAudio.PlayOneShot(bulletSound, 1.0f);
            playerLives -= playerLives;
            Destroy(other.gameObject);
            playerAnim.SetBool("isShot", true);
            Debug.Log("Game Over");
            gameOver = true;

        }

        //if user collides with stakes
        if (other.CompareTag("stake"))
        {
            playerAudio.PlayOneShot(stakeSound, 1.0f);
            Destroy(other.gameObject);
            //reduce player lives on collission with stakes , destroy if live hits zero
            playerLives -= 1;
            if (playerLives == 0)
            {
                playerAnim.SetBool("isShot", true);
                gameOver = true;
            }

        }

        //if player collides with triggers , triggers set to increase difficulty

        if (other.CompareTag("trigger1"))
        {
            playerEnterTrigger1 = true;
        }

        if (other.CompareTag("finishLine"))
        {
            Debug.Log("You Entered Finish line");
            playerWon = true;
        }


    }

    private void Idle()
    {
        playerAnim.SetFloat("Speed_f", 0,0.1f,Time.deltaTime);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        playerAnim.SetFloat("Speed_f", 0.5f,0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        playerAnim.SetFloat("Speed_f", 1 , 0.1f, Time.deltaTime);
    }

    
}
