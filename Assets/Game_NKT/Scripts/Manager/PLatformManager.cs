using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLatformManager : MonoBehaviour
{
    private static PLatformManager instance;
    public static PLatformManager Instance { get => instance; }
    public List<GameObject> listPlatform; 

    private void Awake()
    {
        instance = this;
        listPlatform = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ground"));
    }
}

