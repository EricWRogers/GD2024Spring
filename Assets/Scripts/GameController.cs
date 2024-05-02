using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Battle}

public class GameController : MonoBehaviour
{

    [SerializeField] Player_Controller playerController;
    [SerializeField] BattleManager battleManager;
    [SerializeField] EntityController entityController;

    GameState state;
    
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
            entityController.ControllerStart();
        }
    }
}
