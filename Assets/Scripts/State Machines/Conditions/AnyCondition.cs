using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Condition/AnyCondition")]
public class AnyCondition : TransitionCondition
{
    public override bool Check(CharacterControl characterControl)
    {
        return false;
    }
}