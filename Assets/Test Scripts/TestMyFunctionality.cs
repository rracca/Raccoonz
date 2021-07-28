using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMyFunctionality : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestBaseClass baseclass = GetComponent(typeof(TestBaseClass)) as TestBaseClass;
        if (baseclass != null)
            Debug.Log("Found It!");
        baseclass.Display();
    }
}
