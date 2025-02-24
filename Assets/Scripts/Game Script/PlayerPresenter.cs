using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private List<GameObject> abilityCooldownUI = new List<GameObject>();
    private List<IEnumerator> _abilityCoroutines = new List<IEnumerator>();

    private void Start()
    {
        PlayerDataManager.playerCollectTreeEvent += UpdatePlayerUI;
        PlayerController.UseAbility += StartAbilityCooldown;
    }

    private void UpdatePlayerUI()
    {
        woodText.text = PlayerDataManager._instance.GetWood().ToString(); //for now this is okay but next write like this below

        /*private void UpdatePlayerUI(int data)
        {
            woodText.text = data.ToString(); 

        }*/

        //create script called UIManager and call the UpdatePlayerUI from there. best practice of MVP Presenter is only bridge from model and view you need process the logic from manager
    }

    private void StartAbilityCooldown(int abilityIndex)
    {
        _abilityCoroutines.Add(null); //use loops
        _abilityCoroutines.Add(null);
        _abilityCoroutines.Add(null);
        _abilityCoroutines.Add(null);
        _abilityCoroutines.Add(null);

      /*  for (int i = 0; i < 5; i++) <====== use this ya, dont write same recuring code
        {
            _abilityCoroutines.Add(null); 
        }*/
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