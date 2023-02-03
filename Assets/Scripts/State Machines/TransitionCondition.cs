using UnityEngine;

public abstract class TransitionCondition : ScriptableObject
{
    public abstract bool Check(CharacterControl characterControl);
}