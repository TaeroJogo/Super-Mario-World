using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
        print(3);
    }

    public void RestartButton()
    {
        gameObject.SetActive(false);
    }
}
