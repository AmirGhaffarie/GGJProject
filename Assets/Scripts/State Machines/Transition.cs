using System;
using System.Collections.Generic;

[Serializable]
public class Transition
{
    public State nextState;

    public List<TransitionCondition> conditions;

    public bool CheckConditions(CharacterControl characterControl)
    {
        var result = conditions.TrueForAll(c => c.Check(characterControl));
        return result;
    }
}