using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuSelection : MonoBehaviour
{
    [SerializeField] InputAction nextButton;
    [SerializeField] InputAction prevButton;
    [SerializeField] InputAction rightButton;
    [SerializeField] InputAction leftButton;
    [SerializeField] InputAction submitButton;

    [SerializeField] MenuButton[] buttons;
    [SerializeField] bool resetOnEnable;
    [SerializeField] UnityEvent onSelectionChange;
    [SerializeField] UnityEvent onSelect;
    int currentButtonIndex = 0;
    MenuButton CurrentButton => buttons[currentButtonIndex];
    private void Awake()
    {
        ResetButtons();
    }

    private void ResetButtons()
    {
        currentButtonIndex = 0;
        Array.ForEach(buttons, b => b.OnDeselect());
        CurrentButton.OnSelect();
    }

    private void OnEnable()
    {
        if (resetOnEnable)
            ResetButtons();

    }
    private void Update()
    {
        if (nextButton.WasPressedThisFrame())
            GoNext();
        if (prevButton.WasPressedThisFrame())
            GoPrev();
        if (submitButton.WasPressedThisFrame())
            SubmitButton();
        if (rightButton.WasPressedThisFrame())
            ButtonRight();
        if (leftButton.WasPressedThisFrame())
            ButtonLeft();
    }

    private void ButtonLeft()
    {
        CurrentButton.OnLeft();
    }

    private void ButtonRight()
    {
        CurrentButton.OnRight();
    }

    private void SubmitButton()
    {
        CurrentButton.OnSubmit();
        onSelect.Invoke();
    }

    private void GoPrev()
    {
        if (currentButtonIndex > 0)
        {
            onSelectionChange.Invoke();
            CurrentButton.OnDeselect();
            currentButtonIndex--;
            CurrentButton.OnSelect();
        }
    }

    private void GoNext()
    {
        if (currentButtonIndex < buttons.Length - 1)
        {
            onSelectionChange.Invoke();
            CurrentButton.OnDeselect();
            currentButtonIndex++;
            CurrentButton.OnSelect();
        }
    }
}
