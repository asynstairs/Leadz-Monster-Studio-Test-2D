using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Coupled pair of a Button and a DifficultySciptableObject.
/// </summary>
[RequireComponent(typeof(Button))]
public class DifficultySelectorButton : MonoBehaviour
{
    public event Action<DifficultySciptableObject> Clicked;
        
    [SerializeField] private DifficultySciptableObject _description;

    private Button _button;

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void SubscribeToEvents()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        Clicked?.Invoke(_description);
    }
}
