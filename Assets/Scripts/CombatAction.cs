using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combat Action", menuName = "Create Combat Action")]
// Depends on CombatActionEditor.cs

public class CombatAction : ScriptableObject
{
    public enum Type
    {
        Attack,
        Heal,
        Rest
    }

    [SerializeField] private string displayName;
    [SerializeField] private Type actionType;

    [Header("Damage")]
    [SerializeField] private int damageAmount;
    [SerializeField] private bool isProjectile;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int projectileSpeed;

    [Header("Heal")]
    [SerializeField] private int healAmount;

    public string DisplayName { get => displayName; set => displayName = value; }
    public Type ActionType { get => actionType; set => actionType = value; }
    public int DamageAmount {  get => damageAmount; set => damageAmount = value; }
    public bool IsProjectile {  get => isProjectile; set => isProjectile = value; }
    public GameObject ProjectilePrefab {  get => projectilePrefab; set => projectilePrefab = value; }
    public int ProjectileSpeed {  get => projectileSpeed; set => projectileSpeed = value; }
    public int HealAmount {  get => healAmount; set => healAmount = value; }
}
