using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class TestFunc : MonoBehaviour
{
    public struct test {
        public test123 a;
    }
    public class test123
    {
        public int a;
    }
    public test abc;
    private void Start()
    {
        if (func1(out abc))
        {
            Debug.Log(abc.a.a);
        }
        Debug.Log(abc.a.a);
    }
    public bool func1(out test test1)
    {
        test1 = new test();
        test1.a = new test123();
        test1.a.a = 1;
        return true;
    }
}
