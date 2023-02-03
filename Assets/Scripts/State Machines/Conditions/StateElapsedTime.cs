using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Condition/StateElapsedTime")]
public class StateElapsedTime : TransitionCondition
{
    [SerializeField] float elapsedTime;
    public override bool Check(CharacterControl characterControl)
    {
        return characterControl.ElapsedStateTime >= elapsedTime;
    }
}