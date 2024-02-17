using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatActionUI : MonoBehaviour
{
    [SerializeField] private GameObject visualContainer;
    [SerializeField] private Button[] combatActionButtons;

    private void OnEnable()
    {
        // Subscribe to TurnManager events.
        TurnManager.Instance.OnBeginTurn += OnBeginTurn;
        TurnManager.Instance.OnEndTurn+= OnEndTurn;
    }

    private void OnDisable()
    {
        // Unsubscribe from TurnManager events.
        TurnManager.Instance.OnBeginTurn -= OnBeginTurn;
        TurnManager.Instance.OnEndTurn -= OnEndTurn;
    }

    void OnBeginTurn(Character character)
    {
        if (!character.IsPlayer) 
        { 
            return;
        }

        // Populate button area with custom actions from character.
        for (int i = 0; i < combatActionButtons.Length; i++)
        {
            if (i <  character.CombatActions.Count)
            {
                CombatAction combatAction = character.CombatActions[i];
                combatActionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = combatAction.DisplayName;
                combatActionButtons[i].onClick.RemoveAllListeners();
                combatActionButtons[i].onClick.AddListener(() => { OnClickCombatAction(combatAction); });
                combatActionButtons[i].gameObject.SetActive(true);
            } 
            else
            {
                // Disable button if there is no enught combat actions for character.
                combatActionButtons[i].gameObject.SetActive(false);
            }
        }

        visualContainer.SetActive(true);
    }

    void OnEndTurn(Character character)
    {
        // Disable for all
        visualContainer.SetActive(false);
        
        // Set interactable to true for all buttons.
        foreach (var button in combatActionButtons)
        {
            button.interactable = true;
        }
    }

    void OnClickCombatAction(CombatAction combatAction)
    {
        // Disable all buttons after click.
        foreach (var b in combatActionButtons)
        {
            b.interactable = false;
        }
        
        TurnManager.Instance.GetCurrentCharacter().CastCombatAction(combatAction);
    }
}
