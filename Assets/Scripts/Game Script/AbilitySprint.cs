using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySprint : IAbilityBarbarian
{
    private string _abilityName = "Sprint";
    private int _abilityCooldown = 12;

    public void UseAbility(PlayerController playerController)
    {
        playerController.ApplyAbilityEffect(1);
    }

    public int GetAbilityCd()
    {
        return _abilityCooldown;
    }
}