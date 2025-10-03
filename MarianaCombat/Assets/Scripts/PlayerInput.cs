using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode pauseKey = KeyCode.Escape, AltPauseKey = KeyCode.P;
    public static PlayerInput Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        
    }
}
