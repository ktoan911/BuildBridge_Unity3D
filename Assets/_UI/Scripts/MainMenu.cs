using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        GameManager.Ins.ResumeGame();

        UIManager.Ins.OpenUI<GamePlay>().SetupOnOpen(GameManager.Ins.Player);

        Close(0);
    }

    public void SettingButton()
    {
        GameManager.Ins.PauseGame();

        UIManager.Ins.OpenUI<Setting>();
        Close(0);
    }
}
