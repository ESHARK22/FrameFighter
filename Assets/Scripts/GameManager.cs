using UnityEngine;

public class GameManager: MonoBehaviour
{
    private void Awake()
    {
        // Set the physics tick rate (60hz)
        Time.fixedDeltaTime = 1.0f / 60.0f;
        
        Debug.Log($"[GameManager] Physics timestep locked to: {Time.fixedDeltaTime} s (60Hz)");
    }
}