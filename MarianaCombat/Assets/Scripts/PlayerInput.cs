using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode parryKey = KeyCode.Mouse1;
    public KeyCode pauseKey = KeyCode.Escape;
    public KeyCode AltPauseKey = KeyCode.P;

    public static PlayerInput Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
}
