using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stakeController : MonoBehaviour
{
    public float stakeSpeed = 25.0f;

    private gameController gameControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = GameObject.Find("gameManager").GetComponent<gameController>();
    }

    // Update is called once per frame
    void Update()
    {
        StakeController();
        if (gameControllerScript.isGreenLight)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //destroy bullets if it comes in contact with wall
        if (other.CompareTag("shieldWall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("badShield"))
        {
            Destroy(gameObject);
        }
    }

    void StakeController()
    {
        //moves bullets down the screen
        transform.Translate(Vector3.back * stakeSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.left, 360);

        //destroys bullets when it leaves the user view
        float downBound = -200.0f;
        if (transform.position.z < downBound)
        {
            Destroy(gameObject);
        }
    }

}
