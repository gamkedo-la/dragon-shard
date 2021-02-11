using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [Header("Unit Properties")]
    [SerializeField] int unitLevel = 1;
    [SerializeField] float experience;

    [Header("Gives Exp if Killed")]
    [SerializeField] float expBonus;

    [Header("Experience Modifiers")]
    [SerializeField] float expNeededForNextLevel;
    [SerializeField] [Range(1.0f, 10f)] float expThresholdGrowthRate = 1.2f;
    [SerializeField] [Range(1.0f, 10f)] float growthRateModifier = 1.2f; // modify level up threshold by this much
    [SerializeField] [Range(1, 10)] int levelModInterval = 5; // number of levels before becomes harder to level up

    [Header("Level Up Bonuses")]
    [SerializeField] [Range(1.01f, 1.5f)] float attackBonus = 1.2f;
    [SerializeField] [Range(1.01f, 1.5f)] float healthBonus = 1.2f;
    [SerializeField] [Range(1.01f, 1.5f)] float defenseBonus = 1.2f;

    [Header("References")]
    [SerializeField] HitPoints hitPoints;
    [SerializeField] Attack unitAttributes;
    public bool LevelUpMeleeDamage = true;
    public bool LevelUpRangedDamage = true;
    public bool LevelUpHp = true;
    public bool LevelUpDef = true;

    void Start()
    {
        if (expNeededForNextLevel == 0)
        {
            Debug.LogWarning("Level up requirement not initialized.  Setting default value of 100");
            expNeededForNextLevel = 100;
        }

        if (hitPoints == null)
        {
            hitPoints = GetComponent<HitPoints>();

            if (hitPoints == null)
                Debug.LogError("No HitPoints component on: " + gameObject.name);
        }

        if (unitAttributes == null)
        {
            unitAttributes = GetComponent<Attack>();

            if (unitAttributes == null)
                Debug.LogError("No HitPoints component on: " + gameObject.name);
        }
    }

    public float GetExpBonus() { return expBonus; }

    public void RewardExp(float experienceGained)
    {
        experience += experienceGained;

        if (CheckForLevelUp())
            LevelUp();
    }

    private bool CheckForLevelUp()
    {
        return experience >= expNeededForNextLevel;
    }

    private void LevelUp()
    {
        do
        {
            ++unitLevel;

            if (unitLevel % levelModInterval == 0)
            {
                attackBonus = attackBonus * growthRateModifier;
                healthBonus = healthBonus * growthRateModifier;
                defenseBonus = defenseBonus * growthRateModifier;
            }

            expNeededForNextLevel = expNeededForNextLevel * expThresholdGrowthRate;

            expThresholdGrowthRate = expThresholdGrowthRate * growthRateModifier;

            ApplyLevelUpBonus();

        } while (CheckForLevelUp());
    }

    private void ApplyLevelUpBonus()
    {
        if (LevelUpMeleeDamage)
            unitAttributes.AttackLevelBuff = Mathf.FloorToInt(unitAttributes.AttackLevelBuff * attackBonus);

        if (LevelUpRangedDamage)
            unitAttributes.RangedDamage = Mathf.FloorToInt(unitAttributes.RangedDamage * attackBonus);

        if (LevelUpHp)
            hitPoints.MaxHP = Mathf.FloorToInt(hitPoints.MaxHP * healthBonus);

        if (LevelUpDef)
            unitAttributes.DefenseLevelBuff = Mathf.FloorToInt(unitAttributes.DefenseLevelBuff * defenseBonus);
    }
}
