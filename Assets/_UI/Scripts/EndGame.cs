using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : UICanvas
{
    [SerializeField] private Text textWhoWin;

    public void SetupOnOpen(ColorType colorType)
    {
        SetWinner(ref colorType);
    }
    private void SetWinner(ref ColorType color)
    {

        textWhoWin.text = color.ToString();
    }
}
