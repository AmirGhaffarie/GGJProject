using System;
using UnityEngine;
using UnityEngine.Events;

public class MenuSelection : MonoBehaviour
{
    private const string nextButton = "Down";
    private const string prevButton = "Up";
    private const string rightButton = "Right";
    private const string leftButton = "Left";
    private const string submitButton = "Enter";

    [SerializeField] MenuButton[] buttons;
    [SerializeField] bool resetOnEnable;
    [SerializeField] UnityEvent onSelectionChange;
    [SerializeField] UnityEvent onSelect;
    int currentButtonIndex = 0;
    MenuButton currentButton => buttons[currentButtonIndex];
    private void Awake()
    {
        ResetButtons();
    }

    private void ResetButtons()
    {
        currentButtonIndex = 0;
        Array.ForEach(buttons, b => b.OnDeselect());
        currentButton.OnSelect();
    }

    private void OnEnable()
    {
        if (resetOnEnable)
            ResetButtons();

    }
    private void Update()
    {
        if (Input.GetButtonDown(nextButton))
            GoNext();
        if (Input.GetButtonDown(prevButton))
            GoPrev();
        if (Input.GetButtonDown(submitButton))
            SubmitButton();
        if (Input.GetButtonDown(rightButton))
            ButtonRight();
        if (Input.GetButtonDown(leftButton))
            ButtonLeft();
    }

    private void ButtonLeft()
    {
        currentButton.OnLeft();
    }

    private void ButtonRight()
    {
        currentButton.OnRight();
    }

    private void SubmitButton()
    {
        currentButton.OnSubmit();
        onSelect.Invoke();
    }

    private void GoPrev()
    {
        if (currentButtonIndex > 0)
        {
            onSelectionChange.Invoke();
            currentButton.OnDeselect();
            currentButtonIndex--;
            currentButton.OnSelect();
        }
    }

    private void GoNext()
    {
        if (currentButtonIndex < buttons.Length - 1)
        {
            onSelectionChange.Invoke();
            currentButton.OnDeselect();
            currentButtonIndex++;
            currentButton.OnSelect();
        }
    }
}
