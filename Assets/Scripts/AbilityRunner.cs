using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRunner : MonoBehaviour
{
    [SerializeField] private IAbility _currentAbility = new RageAbility();

    private void UseAbility()
    {
        _currentAbility.Use();
    }
}

public interface IAbility
{
    void Use();
}

public class RageAbility : IAbility
{
    public void Use()
    {
        Debug.Log("Rage Ability Used");
    }
}

public class FireBallAbility : IAbility
{
    public void Use()
    {
        Debug.Log("Fireball Ability Used");
    }
}

public class HealAbility : IAbility
{
    public void Use()
    {
        Debug.Log("Heal Ability Used");
    }
}