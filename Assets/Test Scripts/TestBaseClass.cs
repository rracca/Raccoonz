using UnityEngine;

public abstract class TestBaseClass : MonoBehaviour
{
    private string basename;
    public TestBaseClass (string name)
    {
        basename = name;
    }

    public virtual void Display()
    {
        Debug.Log("I'm from the Base class.");
    }
}
