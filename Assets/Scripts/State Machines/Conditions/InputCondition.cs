using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "State Machines/Condition/Input")]
public class InputCondition : TransitionCondition
{
    [SerializeField] PushState pushState;
    [SerializeField] InputAction action;

    [Serializable]
    enum PushState
    {
        Perform,
        Press,
        Release
    }
    public override bool Check(CharacterControl characterControl)
    {
        if (!action.enabled)
            action.Enable();
        return pushState switch
        {
            PushState.Perform => action.WasPerformedThisFrame(),
            PushState.Press => action.WasPressedThisFrame(),
            PushState.Release => action.WasReleasedThisFrame(),
            _ => false,
        };
    }
}