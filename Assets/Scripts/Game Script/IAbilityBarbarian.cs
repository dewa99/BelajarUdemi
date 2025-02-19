using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityBarbarian
{
    void UseAbility(PlayerController playerController);
    int GetAbilityCd();
}