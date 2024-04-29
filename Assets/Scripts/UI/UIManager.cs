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
    public OnClickGenericEvent firstOnClick;

    public GameObject actionWindow;

    [Header("Ability Window")]
    public Transform abilityUIHolder;
    public GameObject abilityUIPrefab;
    public Text manaNeededUI;


    public static int currentUICount  = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnRow(out RowUI processedUI, EntityData passedData)
    {
        //instantiate the row
        GameObject tmpRow = Instantiate(rowPrefab);
        tmpRow.transform.SetParent(rowHolder, false);
        RowUI rowTmpInfo = tmpRow.GetComponent<RowUI>();

        //instantiate name
        GameObject tmpName = Instantiate(namePrefab);
        tmpName.transform.SetParent(nameHolder, false);
        TMPro.TMP_Text txtName = tmpName.GetComponent<TMPro.TMP_Text>();
        OnClickGenericEvent onClickEvent = tmpName.GetComponent<OnClickGenericEvent>();

        tmpRow.name = "Character" + tmpRow.transform.childCount;
        
        rowTmpInfo.entityUI = txtName;
        onClickEvent.charHolder = passedData;

        processedUI = rowTmpInfo;
    }

    public void FillAbilityWindow()
    {
        CleanAbilityWindow();

        var data = BattleManager.Instance.currentCharacter.entityData.entityAbilities;

        for (int i = 0; i < data.Count; i++)
        {
            GameObject tmpAbilityPrefab = Instantiate(abilityUIPrefab);
            tmpAbilityPrefab.transform.SetParent(abilityUIHolder);

            AbilityUI tmpAbUI = tmpAbilityPrefab.GetComponent<AbilityUI>();
            tmpAbUI.abilityIndex = i;
        }

        
    }

    void CleanAbilityWindow()
    {
        foreach (Transform item in abilityUIHolder)
        {
            Destroy(item.gameObject);
        }
    }
}
