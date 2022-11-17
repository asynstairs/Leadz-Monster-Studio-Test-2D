using UnityEngine;

public class ChunkUnit : MonoBehaviour
{
    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            gameObject.SetActive(value);
        }
    }

    private bool _active;
}