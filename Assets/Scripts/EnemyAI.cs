using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AnimationCurve healDecisionCurve;
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        TurnManager.Instance.OnBeginTurn += OnBeginTurn;
    }

    private void OnDisable()
    {
        TurnManager.Instance.OnBeginTurn -= OnBeginTurn;
    }

    private void OnBeginTurn(Character c)
    {
        // Only execute if character is self (from gameobject), probably there is a better way to do this.
        if (character == c) 
        { 
            DetermineCombatAction();
        }
    }

    private void DetermineCombatAction()
    {
        float healthPercentage = character.HealthPercentage;
        bool decideToHeal = Random.value < healDecisionCurve.Evaluate(healthPercentage);
        
        CombatAction action = null;
        
        if (decideToHeal && HasCombatActionOfType(CombatAction.Type.Heal))
        {
            action = GetCombatActionOfType(CombatAction.Type.Heal);
            character.CastCombatAction(action);
        }
        else if (HasCombatActionOfType(CombatAction.Type.Attack))
        {
            action = GetCombatActionOfType(CombatAction.Type.Attack);
            character.CastCombatAction(action);
        }
        
        if (action == null)
        {
            Debug.LogError("No combat action found for character: " + character.name);
        }
    }

    private bool HasCombatActionOfType(CombatAction.Type type)
    {
        // Loop through the list using condition.
        return character.CombatActions.Exists(a => a.ActionType == type);
    }

    private CombatAction GetCombatActionOfType(CombatAction.Type type)
    {
        // Loop through the list and find by condition.
        List<CombatAction> availableActions = character.CombatActions.FindAll(a => a.ActionType == type);

        return availableActions[Random.Range(0, availableActions.Count)];
    }
}