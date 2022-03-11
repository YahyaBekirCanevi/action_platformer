using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static InputKeys Input;
    public static Manager Instance { get; private set; }
    public Character player;
    /* public List<Character> enemies = new List<Character>();*/    
    private void Start() {
        Instance = this;
        Input = InputKeys.Preset2;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }
}
