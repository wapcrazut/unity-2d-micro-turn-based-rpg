using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isPlayer;
    [SerializeField] private List<CombatAction> combatActions = new List<CombatAction>();
    [SerializeField] private Character opponent;

    private Vector3 _startPosition;

    // Events.
    public static event UnityAction OnHealthChange;
    public static event UnityAction<Character> OnDie;

    public int Health {  get => currentHealth; }
    public int MaxHealth {  get => maxHealth; }
    public float HealthPercentage { get => (float) currentHealth / maxHealth; }
    public bool IsPlayer { get => isPlayer; }
    public List<CombatAction> CombatActions { get => combatActions; }

    public Character Opponent { get => opponent; }

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        OnHealthChange?.Invoke();

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        OnDie?.Invoke(this);
        Destroy(gameObject);
    }

    private void Heal(int amount) 
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        } 
        else 
        {
            currentHealth += amount;
        }
        
        OnHealthChange?.Invoke();
    }

    public void CastCombatAction(CombatAction action)
    {
        
        
        switch (action.ActionType)
        {
            case CombatAction.Type.Attack:
                ExecuteCombatAttackType(action);
                break;
            case CombatAction.Type.Heal:
                ExecuteCombatActionHealType(action);        
                break;
            case CombatAction.Type.Rest:
                ExecuteCombatActionSkipType(action);
                break;
            default:
                Debug.Log("I don't know how to do that!");
                break;
                
        }
    }

    private void ExecuteCombatAttackType(CombatAction action)
    {
        Debug.Log("Attack!");
        StartCoroutine(action.IsProjectile ? AttackOpponentWithProjectile(action) : AttackOpponent(action));
    }

    private void ExecuteCombatActionHealType(CombatAction action)
    {
        Debug.Log("Heal");
        StartCoroutine(Heal(action));
    }

    private void ExecuteCombatActionSkipType(CombatAction action)
    {
        Debug.Log("Zzzzz");
        StartCoroutine(Heal(action));
    }

    private IEnumerator AttackOpponent(CombatAction action) 
    {
        // Move towards opponent.
        while (transform.position != opponent.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, opponent.transform.position, 50 * Time.deltaTime);

            yield return null;
        }

        // Send damage
        opponent.TakeDamage(action.DamageAmount);

        // Move back to original position.
        while (transform.position != _startPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPosition, 50 * Time.deltaTime);

            yield return null;
        }

        TurnManager.Instance.EndTurn();
    }
    
    private IEnumerator AttackOpponentWithProjectile(CombatAction action) 
    {
        // Move projectile towards opponent.
        GameObject projectileInstance = Instantiate(action.ProjectilePrefab, transform.position, Quaternion.identity);
        projectileInstance.GetComponent<Projectile>().Init(action.DamageAmount, action.ProjectileSpeed, opponent, TurnManager.Instance.EndTurn);
        
        yield return null;
    }

    private IEnumerator Heal(CombatAction action)
    {
        Heal(action.HealAmount);
        
        yield return null;

        TurnManager.Instance.EndTurn();
    }
}