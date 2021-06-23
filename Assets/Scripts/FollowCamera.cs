using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    private Transform playerTransform;
    public Player Player;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 temp = transform.position;
        temp.x = playerTransform.position.x;
        if (!Player.hasPassed && playerTransform.position.y > -4)
        {
            temp.y = playerTransform.position.y;
        }
        if (Player.hasPassed)
        {

            temp.y = playerTransform.position.y;
        }

        transform.position = temp;
    }

    public void RestartCamera()
    {
        Vector3 temp = transform.position;
        temp.x = -36;
        temp.y = -5;
        transform.position = temp;
    }
}
