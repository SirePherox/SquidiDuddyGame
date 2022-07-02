using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    //variables
    public float gameTime;
    public float greenTime;
    public float redTime;
    public bool isRedLight;
    public bool isGreenLight;
    public bool isTimeUp;
    [SerializeField] private bool isPlayerWon;

    public bool isGameOver = false;
    public bool isGameActive;
   public float timePlayer; //to know if the player pplayed the game
    [SerializeField] private AudioClip greenAudio;
    [SerializeField] private AudioClip redAudio;
    [SerializeField] private AudioClip gameMusic;

    //referencs
    private playerController playerScript;
    private difficultyController diffScript;
    private spawnManager spawnManagerScript;
    private difficultyController difficultyControllerScript;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timeIsUpText;
    public GameObject restartButton;
    public GameObject titleScreen;
    private AudioSource gameAudio;

    // Start is called before the first frame update
    void Start()
    {
        //gameTime = 1000.0f;
        //gets external script
        playerScript = GameObject.Find("player").GetComponent<playerController>();
        diffScript = GameObject.Find("walk").GetComponent<difficultyController>();
        spawnManagerScript = GameObject.Find("spawnManager").GetComponent<spawnManager>();

        gameAudio = GetComponent<AudioSource>();
        gameTime = GameObject.FindObjectOfType<difficultyController>().gameTimeB;
        timePlayer = 5.0f;


    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = playerScript.gameOver;
        playerScript.gameOver = isGameOver;
        isGameActive = !isGameOver;
        isPlayerWon = playerScript.playerWon;

        if (!isGameOver && isGameActive && !isPlayerWon)
        {
            gameTime -= Time.deltaTime;
            RedLight();
            GreenLight();
            timerText.text = "Time: " + (int)gameTime;
        }
        if (isTimeUp && timePlayer == 0)
        {
            //sets all time to 0
            // redTime = 0;
            // greenTime = 0;
            Debug.Log("Time is up");
            //activates the restart button
            timeIsUpText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        if (gameTime < 1)
        {
            isGameOver = true;
            isTimeUp = true;
            gameTime = 0;
           // redTime = 0;
            //greenTime = 0;
        }

        //to know if the player at least started the game, before time run out
        //timePlayer -= Time.deltaTime;
        //if(timePlayer < 0)
       // {
            //to ensure decrement command doesnt run forever
          //  timePlayer = 0;
       // }

    }


    void GreenLight()
    {
        //runs if greenLight is true & redLight and game over is false
        //decrements greentime over time,once greentime reaches 0,
        //sets redtime and calls redLight

        if (isGreenLight && !isRedLight && !isGameOver && isGameActive && !isTimeUp)
        {
            isGreenLight = true;
            isRedLight = false;
            greenTime -= Time.deltaTime;

            if (greenTime < 0)
            {
                Debug.Log("green time hit zero");
                isRedLight = true;
                isGreenLight = false;
                redTime = 15.0f;
                gameAudio.PlayOneShot(redAudio, 1.0f);
            }
        }

    }

    void RedLight()
    {
        //runs if redLight is true & greenLight and game over is false
        //decrements redtime over Time,once redtime reaches 0,
        //sets greentime within the range(3,7) & calls greenLight
        if (isRedLight && !isGreenLight && !isGameOver && isGameActive && !isTimeUp)
        {
            isRedLight = true;
            isGreenLight = false;
            redTime -= Time.deltaTime;

            if (redTime < 0)
            {
                Debug.Log("red time hit zero");
                isRedLight = false;
                isGreenLight = true;
                greenTime = Random.Range(3.0f, 7.0f);
                gameAudio.PlayOneShot(greenAudio, 2.0f);

            }
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void StartGame(float gametime, float redtime)
    {
        //sets start value for new game session
        redTime = redtime; //difficultyControllerScript.redTimeB;
        this.gameTime = gametime;
        isRedLight = true;
        isGreenLight = false;
        isTimeUp = false;
        playerScript.playerWon = false;
        gameAudio.PlayOneShot(gameMusic, 0.3f);

        //calls bullets instantiator at intervals
        StartCoroutine(spawnManagerScript.ObjectSpawn());
        //hides the title menu
        titleScreen.SetActive(false);
    }
}
