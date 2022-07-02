using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class difficultyController : MonoBehaviour
{
    //variables
    public float gameTimeB;
    public float redTimeB;
    //references
    private Button button;
    private gameController gameControllerScript;

    
    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = GameObject.Find("gameManager").GetComponent<gameController>();
        button = GetComponent<Button>();
        button.onClick.AddListener(setDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setDifficulty()
    {
        //****
        if (gameObject.name == "walk")
        {
            gameTimeB = 100.0f;
            redTimeB = 9.0f;
            Debug.Log("This is a easy one");
        }
        else if (gameObject.name == "run")
        {
            gameTimeB = 60.0f;
            redTimeB = 12.0f;
        }
        else if (gameObject.name == "sprint")
        {
            gameTimeB = 40.0f;
            redTimeB = 15.0f;
        }
        //********

        gameControllerScript.StartGame(gameTimeB,redTimeB);
        Debug.Log(gameObject.name + "was clicked");
    }

}
