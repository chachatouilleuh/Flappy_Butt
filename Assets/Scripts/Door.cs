using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool passed;

    public bool Passed { get { return passed; } }

    private void Start()
    {
        passed = false;
    }

    public void Pass()
    {
        passed = true;
    }
}
