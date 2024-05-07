using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject LoseMenuUI;

     Health Health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.health <0.1f)
        {
            LoseScreen();
        }
    }

    public void LoseScreen()
    {
        LoseMenuUI.SetActive(true);
        Time.timeScale = 0f;

    }
}
