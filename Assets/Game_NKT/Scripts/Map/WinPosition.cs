using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WinPosition : MonoBehaviour
{
    private void Start()
    {
        this.transform.position = PLatformManager.Instance.listPlatform[PLatformManager.Instance.listPlatform.Count - 1].transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            ColorType colorType = ColorType.none;
            Characters characters = other.GetComponent<Characters>();
            if (characters != null)
            {
                colorType = characters.OnWin();
            }

            UIManager.Ins.OpenUI<EndGame>().SetupOnOpen(colorType);

        }
    }
}
