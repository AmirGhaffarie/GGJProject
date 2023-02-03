using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Condition/")]
public class  : TransitionCondition
{
    public override bool Check(CharacterControl characterControl)
    {
        return false;
    }
}