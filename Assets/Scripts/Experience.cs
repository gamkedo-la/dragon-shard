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
    [SerializeField] [Range(1.0f, 10f)] float attackBonus;
    [SerializeField] [Range(1.0f, 10f)] float healthBonus;
    [SerializeField] [Range(1.0f, 10f)] float defenseBonus;

    [Header("References")]
    [SerializeField] HitPoints hitPoints;
    [SerializeField] Attack unitAttributes;
    public bool LevelUpMeleeDamage = true;
    public bool LevelUpRangedDamage = true;
    public bool LevelUpHp = true;

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
        ++unitLevel;

        if (unitLevel % levelModInterval == 0)
        {
            ApplyRateChange(expThresholdGrowthRate, growthRateModifier);
            ApplyRateChange(attackBonus, growthRateModifier);
            ApplyRateChange(healthBonus, growthRateModifier);
            ApplyRateChange(defenseBonus, growthRateModifier);
        }

        ApplyRateChange(expNeededForNextLevel, expThresholdGrowthRate);

        ApplyLevelUpBonus();
    }

    private void ApplyLevelUpBonus()
    {
        if (LevelUpMeleeDamage)
            ApplyRateChange(unitAttributes.MeleeDamage, attackBonus);

        if (LevelUpRangedDamage)
            ApplyRateChange(unitAttributes.RangedDamage, attackBonus);

        if (LevelUpHp)
            ApplyRateChange(hitPoints.MaxHP, healthBonus);
    }

    private void ApplyRateChange(float toModify, float modifier)
    {
        toModify *= modifier;
    }
}
