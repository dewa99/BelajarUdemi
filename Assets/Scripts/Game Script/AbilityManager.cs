using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private List<IAbilityBarbarian> _abilities = new List<IAbilityBarbarian>();
    private PlayerController _playerController;
    private IEnumerator _coroutine;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        PlayerController.UseAbility += UseAbility;
        //assign abilities
        _abilities.Add(new AbilitySprint());
        _abilities.Add(new AbilitySprint());
        _abilities.Add(new AbilitySpin());
    }

    private void UseAbility(int abilityIndex)
    {
        ApplyAbility(abilityIndex);
    }

    private void ApplyAbility(int abilityIndex)
    {
        if (_coroutine != null) return;
        for (var i = 0; i < _abilities.Count; i++)
        {
            if (i != abilityIndex) continue;
            _abilities[i].UseAbility(_playerController);
            StartAbilityCooldown(abilityIndex, _abilities[i].GetAbilityCd());
        }
    }

    private void StartAbilityCooldown(int abilityIndex, int abilityCooldown)
    {
        switch (abilityIndex)
        {
            case 1:
                _coroutine = AbilityCooldown(abilityCooldown);
                StartCoroutine(_coroutine);
                break;
        }
    }

    private IEnumerator AbilityCooldown(int cooldown)
    {
        while (cooldown > 0)
        {
            yield return new WaitForSeconds(1);
            cooldown--;
        }

        _coroutine = null;
    }
}