using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    //variables

    //references
    private Animator enemyAnim;
    private playerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = GameObject.Find("player").GetComponent<playerController>();
        enemyAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerControllerScript.playerWon)
        {
            enemyAnim.SetBool("Death_b", true);
            enemyAnim.SetInteger("DeathType_int", 2);
        }
    }
}
