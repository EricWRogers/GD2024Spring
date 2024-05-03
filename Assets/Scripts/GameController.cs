using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Battle}

public class GameController : MonoBehaviour
{

    [SerializeField] public Player_Controller playerController;
    [SerializeField] public BattleManager battleManager;
    [SerializeField] public Camera worldCamera;
    

   public GameState state;

    private void Start()
    {
        playerController.onEncountered += StartBattle;
        battleManager.onBattleOver += EndBattle;
    }

    void StartBattle()
    {
        state = GameState.Battle;
        battleManager.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

    }

    void EndBattle(bool won)
    {
        state = GameState.FreeRoam; 
    
        worldCamera.gameObject.SetActive(true);
        battleManager.gameObject.SetActive(false);
        
    }

    
    
    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleManager.StartBattle();
        }
    }
}
