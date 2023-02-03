using UnityEngine;

public abstract class State : ScriptableObject
{
    #region Variables

    public virtual string Name => name;

    #endregion

    #region Methods

    public virtual void OnEnter(CharacterControl characterControl)
    {
    }

    public virtual void Execute(CharacterControl characterControl)
    {
    }

    public virtual void OnExit(CharacterControl characterControl)
    {
    }

    #endregion
}

