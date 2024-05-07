using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject LoseMenuUI;

    public Health Health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.health <0.1f)
        {
            Lose()
        }
    }

    public void Lose()
    {
        LoseMenuUI.SetActive(true);
        Time.timeScale = 0f

    }
}
