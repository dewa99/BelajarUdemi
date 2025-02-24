using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BarbarianContext
{
    void SetState(BarbarianState state);
}

public interface BarbarianState
{
    void Walking(BarbarianContext context, Animator animator);
    void Attack(BarbarianContext context, Animator animator, PlayerController playerController);
    void Idle(BarbarianContext context, Animator animator, PlayerController playerController);
    void Ability(BarbarianContext context, Animator animator, int abilityIndex);
}

public class StateBarbarian : MonoBehaviour, BarbarianContext
{
    private BarbarianState _currentState = new IdleState();
    [SerializeField] private Animator animator;
    private PlayerController _playerController;

    private void Start()
    {
        PlayerController.playerWalking += Walking;
        PlayerController.playerChopTree += Attack; //The playerChopTree event is subscribed to with PlayerController.playerChopTree += Attack. This will be confusing if Attack is not only for chopping trees in the future. It would be better to rename playerChopTree to playerAttack or just Attack
        PlayerController.playerIdling += Idle;
        PlayerController.UseAbility += Ability;
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Walking() => _currentState.Walking(this, animator);
    private void Attack() => _currentState.Attack(this, animator, _playerController);
    private void Idle() => _currentState.Idle(this, animator, _playerController);
    private void Ability(int index) => _currentState.Ability(this, animator, index);

    void BarbarianContext.SetState(BarbarianState state)
    {
        _currentState = state;
    }
}

public class IdleState : BarbarianState
{
    public void Attack(BarbarianContext context, Animator animator, PlayerController playerController)
    {
        context.SetState(new AttackState());
        animator.SetBool("IsAttacking", true);
        playerController.DisableMovement();
        // Debug.Log("Barbarian is attacking while idle");
    }

    public void Idle(BarbarianContext context, Animator animator, PlayerController playerController)
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        playerController.EnableMovement();
    }

    public void Ability(BarbarianContext context, Animator animator, int abilityIndex)
    {
        switch (abilityIndex)
        {
            case 1:
                context.SetState(new AbilityState());
                return;
                break; // this is unused because u already call return;  switch case always has break in the end of condition so dont use return if its not necessary
            case 2:
                context.SetState(new AbilityState());
                animator.SetBool("IsSpinning", true);
                break;
        }
    }

    public void Walking(BarbarianContext context, Animator animator)
    {
        context.SetState(new WalkingState());
        animator.SetBool("IsWalking", true);
        // Debug.Log("Barbarian is running while idle");
    }
}

public class WalkingState : BarbarianState
{
    public void Attack(BarbarianContext context, Animator animator, PlayerController playerController)
    {
        context.SetState(new AttackState());
        animator.SetBool("IsWalking", false);
        // Debug.Log("Barbarian is attacking while running");
    }

    public void Idle(BarbarianContext context, Animator animator, PlayerController playerController)
    {
        context.SetState(new IdleState());
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        playerController.EnableMovement();
        // Debug.Log("Barbarian is idle while running");
    }

    public void Ability(BarbarianContext context, Animator animator, int abilityIndex)
    {
        switch (abilityIndex)
        {
            case 1:
                context.SetState(new AbilityState());
                return;
                break; // this is unused because u already call return;  switch case always has break in the end of condition so dont use return if its not necessary
            case 2:
                context.SetState(new AbilityState());
                animator.SetBool("IsSpinning", true);
                break;
        }
    }

    public void Walking(BarbarianContext context, Animator animator)
    {
        animator.SetBool("IsWalking", true);
        // Debug.Log("Barbarian is running");
        return;
    }
}

public class AttackState : BarbarianState
{
    public void Attack(BarbarianContext context, Animator animator, PlayerController playerController)
    {
        animator.SetBool("IsAttacking", true);
        return;
    }

    public void Idle(BarbarianContext context, Animator animator, PlayerController playerController)
    {
        context.SetState(new IdleState());
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        playerController.EnableMovement();

        // Debug.Log("Barbarian is idle while attacking");
    }

    public void Ability(BarbarianContext context, Animator animator, int abilityIndex)
    {
        return;
    }

    public void Walking(BarbarianContext context, Animator animator)
    {
        context.SetState(new WalkingState());
        animator.SetBool("IsAttacking", false);
        // Debug.Log("Barbarian is running while attacking");
    }
}

public class AbilityState : BarbarianState
{
    public void Attack(BarbarianContext context, Animator animator, PlayerController playerController)
    {
        return;
    }

    public void Idle(BarbarianContext context, Animator animator, PlayerController playerController)
    {
        context.SetState(new IdleState());
        animator.SetBool("IsSpinning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        playerController.EnableMovement();
        // Debug.Log("Barbarian is idle while spinning");
    }

    public void Ability(BarbarianContext context, Animator animator, int abilityIndex)
    {
        switch (abilityIndex)
        {
            case 1:
                context.SetState(new AbilityState());
                return;
                break; // this is unused because u already call return;  switch case always has break in the end of condition so dont use return if its not necessary
            case 2:
                context.SetState(new AbilityState());
                animator.SetBool("IsSpinning", true);
                break;
        }
    }

    public void Walking(BarbarianContext context, Animator animator)
    {
        context.SetState(new WalkingState());
        animator.SetBool("IsSpinning", false);
        animator.SetBool("IsWalking", true);
        // Debug.Log("Barbarian is running while spinning");
    }
}