using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "ScriptableObjects/Level difficulties", order = 1)]
public class DifficultySciptableObject : ScriptableObject
{
    public float HorizontalSpeed => _horizontalSpeed;
    
    [SerializeField, Min(0f)] private float _horizontalSpeed;
}
