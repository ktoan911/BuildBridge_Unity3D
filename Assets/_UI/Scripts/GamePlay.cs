using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{
    [SerializeField] private FloatingJoystick joystick;
    public void SetupOnOpen(Player player)
    {
        player.SetupJoyStick(joystick);
    }
    public void SettingButton()
    {
        GameManager.Ins.PauseGame();
        UIManager.Ins.OpenUI<Setting>();
    }
}
