using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform rowHolder;
    public Transform nameHolder;

    [Header("UI Prefabs")]
    public GameObject rowPrefab;
    public GameObject namePrefab;

    [Header("First PlayerUI")]
    public RowUI defaultRowUI;

    public GameObject actionWindow;

    public static int currentUICount  = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnRow(out RowUI processedUI)
    {
        //instantiate the row
        GameObject tmpRow = Instantiate(rowPrefab);
        tmpRow.transform.SetParent(rowHolder, false);
        RowUI rowTmpInfo = tmpRow.GetComponent<RowUI>();

        //instantiate name
        GameObject tmpName = Instantiate(namePrefab);
        tmpName.transform.SetParent(nameHolder, false);
        TMPro.TMP_Text txtName = tmpName.GetComponent<TMPro.TMP_Text>();

        tmpRow.name = "Character" + tmpRow.transform.childCount;
        
        rowTmpInfo.entityUI = txtName;

        processedUI = rowTmpInfo;
    }
}
