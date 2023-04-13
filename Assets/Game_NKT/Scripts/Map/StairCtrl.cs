using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCtrl : MonoBehaviour
{
    [SerializeField] private ColorData colorData;

    public ColorType colorType;

    private void Start()
    {
        SetStairColor((int)ColorType.none);
    }

    public void SetStairColor(int numberEnum)
    {
        GetComponent<Renderer>().material = colorData.GetColor(numberEnum);
        colorType = (ColorType)numberEnum;
    }
}
