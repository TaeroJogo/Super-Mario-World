using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public void Setup()
    {
        print(4);
        gameObject.SetActive(true);
    }

    public void PlayAgainButton()
    {
        gameObject.SetActive(false);
    }
}
