using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LocomotionContext
{
    void SetState(LocomotionState newState);
}

public interface LocomotionState
{
    void Jump(LocomotionContext context);
    void Crouch(LocomotionContext context);
    void Land(LocomotionContext context);
    void Fall(LocomotionContext context);
}

public class StatePattern : MonoBehaviour, LocomotionContext
{
    LocomotionState _currentState = new GroundedState();

    public void Crouch() => _currentState.Crouch(this);

    public void Jump() => _currentState.Jump(this);

    public void Land() => _currentState.Land(this);

    public void Fall() => _currentState.Fall(this);

    void LocomotionContext.SetState(LocomotionState newState)
    {
        _currentState = newState;
    }
}

public class GroundedState : LocomotionState
{
    public void Jump(LocomotionContext context)
    {
        context.SetState(new InAirState());
    }

    public void Crouch(LocomotionContext context)
    {
        context.SetState(new CrouchingState());
    }

    public void Land(LocomotionContext context)
    {
        return; 
    }

    public void Fall(LocomotionContext context)
    {
        context.SetState(new InAirState());
    }
}

public class CrouchingState : LocomotionState
{
    public void Jump(LocomotionContext context)
    {
        context.SetState(new GroundedState());
    }

    public void Crouch(LocomotionContext context)
    {
        context.SetState(new GroundedState());
    }

    public void Land(LocomotionContext context)
    {
        return;
    }

    public void Fall(LocomotionContext context)
    {
        context.SetState(new InAirState());
    }
}


public class InAirState : LocomotionState
{
    public void Jump(LocomotionContext context)
    {
        return;
    }

    public void Crouch(LocomotionContext context)
    {
        return;
    }

    public void Land(LocomotionContext context)
    {
        context.SetState(new GroundedState());
    }

    public void Fall(LocomotionContext context)
    {
        return;
    }
}