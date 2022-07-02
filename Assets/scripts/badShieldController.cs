using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badShieldController : MonoBehaviour
{
    private playerController playerScript;
    public float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 3.0f;
        playerScript = GameObject.Find("player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateWall();
    }

    void RotateWall()
    {
        if (playerScript.playerEnterTrigger1)
        {
            currentTime += Time.deltaTime;
            transform.Rotate(0, currentTime, 0);
        }
    }
}
