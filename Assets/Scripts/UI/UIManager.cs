using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform rowHolder;
    public Transform nameHolder;

    [Header("UI Prefabs")]
    public GameObject rowPrefab;
    public GameObject namePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnRow()
    {
        //instantiate the row
        GameObject tmpRow = Instantiate(rowPrefab);
        tmpRow.transform.SetParent(rowHolder, false);

        //instantiate name
        GameObject tmpName = Instantiate(namePrefab);
        tmpName.transform.SetParent(nameHolder, false);
    }
}
