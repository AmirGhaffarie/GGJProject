using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;

[Serializable]
public class StateTransitionDictionary : SerializableDictionaryBase<State, TransitionList>
{
}
[Serializable]
public class TransitionList
{
    public List<Transition> Transitions;
}