using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType { red, blue, green, none }

[CreateAssetMenu(fileName = "New ColorData", menuName = "ColorData")]
public class ColorData : ScriptableObject
{
    [SerializeField]public List<Material> matsList = new List<Material>();

    public Material GetColor(int numberEnum)
    {
        return matsList[numberEnum];
    }
}

