using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeys {
    #region Variables
    private KeyCode _left;
    private KeyCode _right;
    public KeyCode _up;
    public KeyCode _down;
    public KeyCode _dash;
    public KeyCode _lAttack;
    public KeyCode _hAttack;
    public KeyCode _changeWeapon;
    public KeyCode Menu = KeyCode.Escape;
    #endregion
    #region Properties
    public KeyCode Left { get => _left; private set { _left = value; } }
    public KeyCode Right { get => _right; private set { _right = value; } }
    public KeyCode Up { get => _up; private set { _up = value; } }
    public KeyCode Down { get => _down; private set { _down = value; } }
    public KeyCode Dash { get => _dash; private set { _dash = value; } }
    public KeyCode LAttack { get => _lAttack; private set { _lAttack = value; } }
    public KeyCode HAttack { get => _hAttack; private set { _hAttack = value; } }
    public KeyCode ChangeWeapon { get => _changeWeapon; private set { _changeWeapon = value; } }
    #endregion
    #region Set
    public static InputKeys Instance { get => Manager.Input; set { Manager.Input = value; } }
    public static InputKeys Preset1 { get => new InputKeys(KeyCode.A,KeyCode.D,KeyCode.W,KeyCode.S,KeyCode.Space,KeyCode.Mouse0,KeyCode.Mouse1,KeyCode.LeftShift); }
    public static InputKeys Preset2  { get => new InputKeys(KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.UpArrow,KeyCode.DownArrow
        ,KeyCode.Z,KeyCode.C,KeyCode.X,KeyCode.V); }

    #endregion 
    private InputKeys(KeyCode left,KeyCode right,KeyCode up,KeyCode down,KeyCode dash,KeyCode lAttack,KeyCode hAttack,KeyCode changeWeapon){
        Left = left;
        Right = right;
        Up = up;
        Down = down;
        Dash = dash;
        LAttack = lAttack;
        HAttack = hAttack;
        ChangeWeapon = changeWeapon;
    }
}
