using UnityEngine;

[RequireComponent(typeof(CharacterControl))]
public class CharacterStateMachine : MonoBehaviour
{
    [SerializeField] private State currentState;
    [SerializeField] private CharacterControl characterControl;

    [SerializeField] private StateTransitionDictionary _transitionDictionary = new StateTransitionDictionary();

    private void Awake() => characterControl = GetComponent<CharacterControl>();

    private void Start()
    {
        DoOnEnter();
    }

    public void ChangeState(State state)
    {
        DoOnExit();
        currentState = state;
        DoOnEnter();
    }

    private void DoOnEnter()
    {
        characterControl.ElapsedStateTime = 0;
        characterControl.animator.Play(currentState.Name);
        currentState.OnEnter(characterControl);
    }

    private void DoOnExit()
    {
        currentState.OnExit(characterControl);
    }

    private void LateUpdate()

    {
        if (_transitionDictionary.ContainsKey(currentState))
        {
            foreach (var transition in _transitionDictionary[currentState].Transitions)
            {
                if (transition.CheckConditions(characterControl))
                {
                    ChangeState(transition.nextState);
                    return;
                }
            }
        }
        currentState.Execute(characterControl);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        characterControl = characterControl != null ? characterControl : GetComponent<CharacterControl>();
    }
#endif

}
