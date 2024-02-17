using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CombatAction))]
public class CombatActionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CombatAction combatAction = (CombatAction)target;
        
        combatAction.DisplayName = EditorGUILayout.TextField("Display Name", combatAction.DisplayName);
        combatAction.ActionType = (CombatAction.Type)EditorGUILayout.EnumPopup("Action Type", combatAction.ActionType);
        
        switch (combatAction.ActionType)
        {
            case CombatAction.Type.Attack:
                combatAction.DamageAmount = EditorGUILayout.IntField("Damage Amount", combatAction.DamageAmount);
                combatAction.IsProjectile = EditorGUILayout.Toggle("Is Projectile", combatAction.IsProjectile);
                if (combatAction.IsProjectile)
                {
                    combatAction.ProjectilePrefab = (GameObject)EditorGUILayout.ObjectField("Projectile Prefab", combatAction.ProjectilePrefab, typeof(GameObject), false);
                    combatAction.ProjectileSpeed = EditorGUILayout.IntField("Projectile Speed", combatAction.ProjectileSpeed);
                }
                break;
            case CombatAction.Type.Heal:
                combatAction.HealAmount = EditorGUILayout.IntField("Heal Amount", combatAction.HealAmount);
                break;
            case CombatAction.Type.Rest:
                combatAction.HealAmount = EditorGUILayout.IntField("Heal Amount", combatAction.HealAmount);
                break;
        }
    }
}