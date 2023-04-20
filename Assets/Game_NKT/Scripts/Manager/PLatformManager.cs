using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLatformManager : Singleton<PLatformManager>
{
    public List<GameObject> listPlatform; 

    private void Awake()
    {
        listPlatform = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ground"));
    }
}

