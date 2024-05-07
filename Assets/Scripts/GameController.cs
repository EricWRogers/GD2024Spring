using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Battle}

public class GameController : MonoBehaviour
{

    [SerializeField] public Player_Controller playerController;
    [SerializeField] public BattleManager battleManager;
    [SerializeField] public Camera worldCamera;    
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public bool isBattleActive;


    

   public GameState state;

    private void Start()
    {
        playerController.onEncountered += StartNewBattle;
        battleManager.OnBattleOver += EndBattle;
        LoadEnemyPrefabs();
    }

    void StartNewBattle()
    {
        if (!isBattleActive) // Check if a battle is not currently active
        {
            isBattleActive = true; 
            state = GameState.Battle;
            battleManager.gameObject.SetActive(true);
            worldCamera.gameObject.SetActive(false);

            
            int numEnemiesToSpawn = Random.Range(1, 3);

            
            for (int i = 0; i < numEnemiesToSpawn && i < enemyPrefabs.Count; i++)
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Count);
                GameObject enemy = Instantiate(enemyPrefabs[randomIndex], battleManager.transform.position, Quaternion.identity);
                enemy.transform.SetParent(battleManager.transform); 
            }
        }
    }

    void EndBattle(bool won)
    {
        state = GameState.FreeRoam; 
        battleManager.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);

        isBattleActive = false; 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.IsChildOf(battleManager.transform))
            {
                Destroy(enemy);
            }
        }
        
    }

    

    void LoadEnemyPrefabs()
    {
       
        Object[] loadedPrefabs = Resources.LoadAll("EnemyPrefabs", typeof(GameObject));

        
        foreach (var prefab in loadedPrefabs)
        {
            enemyPrefabs.Add(prefab as GameObject);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if(state == GameState.Battle)
        {
            battleManager.HandleUpdate();
        }
    }
}
