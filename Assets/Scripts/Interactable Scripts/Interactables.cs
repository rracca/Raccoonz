using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    private string objectName;

    protected Interactables (string ObjectName) 
    {
        objectName = ObjectName;
    }
}
