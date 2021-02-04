using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [Header("Unit Properties")]
    [SerializeField] int unitLevel;
    [SerializeField] float experience;

    [Header("Gives Exp if Killed")]
    [SerializeField] float expBonus;

    [Header("Experience Modifiers")]
    [SerializeField] float expNeededForNextLevel;
    [SerializeField] float expThresholdGrowthRate;
    [SerializeField] float growthRateModifier; // modify level up threshold by this much
    [SerializeField] [Range(1, 10)] int levelModInterval; // number of levels before becomes harder to level up

    [Header("Level Up Bonuses")]
    [SerializeField] float attackBonus;
    [SerializeField] float healthBonus;
    [SerializeField] float defenseBonus;

    [Header("References")]
    [SerializeField] HitPoints hitPoints;
    [SerializeField] Attack unitAttributes;

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


    }
}
