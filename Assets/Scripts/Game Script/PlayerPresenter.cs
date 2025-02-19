using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerDataManager player;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private List<GameObject> abilityCooldownUI = new List<GameObject>();
    private List<IEnumerator> _abilityCoroutines = new List<IEnumerator>();

    private void Start()
    {
        player = FindObjectOfType<PlayerDataManager>();
        PlayerDataManager.playerCollectTreeEvent += UpdatePlayerUI;
        PlayerController.UseAbility += StartAbilityCooldown;
    }

    private void UpdatePlayerUI()
    {
        woodText.text = player.GetWood().ToString();
    }

    private void StartAbilityCooldown(int abilityIndex)
    {
        _abilityCoroutines.Add(null);
        _abilityCoroutines.Add(null);
        _abilityCoroutines.Add(null);
        _abilityCoroutines.Add(null);
        _abilityCoroutines.Add(null);
        abilityIndex -= 1;
        if (_abilityCoroutines.Count > 0)
        {
            if (_abilityCoroutines[abilityIndex] != null) return;
        }

        switch (abilityIndex)
        {
            case 0:
                abilityCooldownUI[abilityIndex].SetActive(true);
                _abilityCoroutines[abilityIndex] = AbilityCooldown(abilityIndex, 12);
                StartCoroutine(_abilityCoroutines[abilityIndex]);
                break;
            case 1:
                abilityCooldownUI[abilityIndex].SetActive(true);
                _abilityCoroutines[abilityIndex] = AbilityCooldown(abilityIndex, 12);
                StartCoroutine(_abilityCoroutines[abilityIndex]);
                break;
        }
    }

    private IEnumerator AbilityCooldown(int abilityIndex, int cooldown)
    {
        var abilityCooldownText = abilityCooldownUI[abilityIndex].GetComponentInChildren<TextMeshProUGUI>();
        abilityCooldownText.text = cooldown.ToString();
        while (cooldown > 0)
        {
            yield return new WaitForSeconds(1);
            cooldown--;
            abilityCooldownText.text = cooldown.ToString();
        }

        abilityCooldownUI[abilityIndex].SetActive(false);
        _abilityCoroutines[abilityIndex] = null;
    }
}