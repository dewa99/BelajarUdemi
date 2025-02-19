using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpin : IAbilityBarbarian
{
    private string _abilityName = "Spin";
    private int _abilityCooldown = 15;

    public void UseAbility(PlayerController playerController)
    {
        playerController.ApplyAbilityEffect(2);
    }

    public int GetAbilityCd()
    {
        return _abilityCooldown;
    }
}