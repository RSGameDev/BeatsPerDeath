using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player playerScript;

    public bool pushBack;

    public string direction;

    private void Awake()
    {
        playerScript = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (pushBack)
        {
            PlayerPushBack(direction);
        }        
    }

    void Movement()
    {
        Vector3 position;
        position = transform.position;

        if (Input.GetKeyDown("w"))
        {
            direction = "w";
            transform.LookAt(transform.position + Vector3.forward);
            transform.position += new Vector3(0, 0, 1.25f);
            if (transform.position.z > 7)
            {
                transform.position = position;
            }
        }

        if (Input.GetKeyDown("s"))
        {
            direction = "s";
            transform.LookAt(transform.position + Vector3.back);
            transform.position += new Vector3(0, 0, -1.25f);
            if (transform.position.z < 0)
            {
                transform.position = position;
            }
        }

        if (Input.GetKeyDown("a"))
        {
            direction = "a";
            transform.LookAt(transform.position + Vector3.left);
            transform.position += new Vector3(-1.25f, 0, 0);
            if (transform.position.x < 0)
            {
                transform.position = position;
            }
        }

        if (Input.GetKeyDown("d"))
        {
            direction = "d";
            transform.LookAt(transform.position + Vector3.right);
            transform.position += new Vector3(1.25f, 0, 0);
            if (transform.position.x > 5)
            {
                transform.position = position;
            }
        }
    }

    public void PlayerPushBack(string direction)
    {
        switch (direction)
        {
            case "w":
                transform.position += new Vector3(0, 0, -1.25f);
                break;
            case "s":
                transform.position += new Vector3(0, 0, 1.25f);
                break;
            case "a":
                transform.position += new Vector3(1.25f, 0, 0);
                break;
            case "d":
                transform.position += new Vector3(-1.25f, 0, 0);
                break;                
        }
        pushBack = false;
    }
}
