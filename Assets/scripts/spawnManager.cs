using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    //variables
    private float callInterval;
    private int prefabIndex;
    [SerializeField] private bool isRedLightS;
   [SerializeField] private bool isGreenLightS;
    [SerializeField] private bool isGameOverS;
    [SerializeField] bool isTimeUpS;
    private bool isPlayerWon;

    //references
    public GameObject[] stakePrefab;
    private playerController playerControllerScript;
    private gameController gameControllerScript;



    // Start is called before the first frame update
    void Start()
    {
        //gets external script
        playerControllerScript = GameObject.Find("player").GetComponent<playerController>();
        gameControllerScript = GameObject.Find("gameManager").GetComponent<gameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //sets external script's variable to internal variable
        isGameOverS = playerControllerScript.gameOver;
        isGreenLightS = gameControllerScript.isGreenLight;
        isRedLightS = gameControllerScript.isRedLight;
        isTimeUpS = gameControllerScript.isTimeUp;
        isPlayerWon = playerControllerScript.playerWon;

        //make sure that any change to the boolean value of isGameOverS, is set to the gameOver value of playerControllerScript
        playerControllerScript.gameOver = isGameOverS;

        //if green light,destroy all game object in the scene
        if (isGreenLightS || isGameOverS || isPlayerWon)
        {
            Destroy(stakePrefab[prefabIndex]);
        }
    }

    public void StakesCreator()
    {
        //checks if greenLight & isGameOver is false, redLight is true
        if (!isGameOverS && isRedLightS && !isPlayerWon && !isTimeUpS )
        {
            //creates bullets
            float posX = 24.0f;
            float spawnPosX = Random.Range(-posX, posX);
            float spawnPosY = 2.0f;
            float spawnPosZ = transform.position.z;
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

            //sets which prefab to instantiate
            prefabIndex = Random.Range(0, 3);
            Instantiate(stakePrefab[prefabIndex], spawnPos, stakePrefab[prefabIndex].transform.rotation);
        }


    }

    public IEnumerator ObjectSpawn()
    {
        while (!isGameOverS)
        {
            callInterval = 0.2f;
            yield return new WaitForSeconds(callInterval);
            StakesCreator();
        }

    }

 


}
