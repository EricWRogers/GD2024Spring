using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AbilityUI : MonoBehaviour, IPointerDownHandler
{
    public TMPro.TMP_Text abilityText;
    public int abilityIndex = 0;

    bool isSelected;
    public void Init(string abilityName)
    {
        abilityText.text = abilityName;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var charData = BattleManager.Instance.currentCharacter.entityData;


        for(int i = 0; i < charData.entityAbilities.Count; i++)
        {

            if (abilityIndex.Equals(i))
            {
                if(isSelected)
                {
                    BattleManager.Instance.currentCharacter.entityData.Attack(charData.entityAbilities[i]);
                    isSelected = false;
                }
                else
                {
                    UIManager.Instance.SetEnergyNeededUI(charData.entityAbilities[i].energyCost, charData.curEnergy);
                    isSelected = true;
                }
                break;
            }
        }
    }
}
