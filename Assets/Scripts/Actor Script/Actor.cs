using System.Collections.Generic;
using UnityEngine;
public abstract class Actor : MonoBehaviour
{
    private string actorType;
    private GameObject collidedObject;
    private Stack<GameObject> pickedUpObject;
    protected float healthPoints;
    protected int maxHeldObject;
    public GameObject CollidedObject { get => collidedObject; set => collidedObject = value; }
    public Stack<GameObject> PickedUpObject { get => pickedUpObject; set => pickedUpObject = value; }

    protected Actor (string ActorType, float HealthPoints) {
        actorType = ActorType;
        healthPoints = HealthPoints;
        pickedUpObject = new Stack<GameObject>();
    }
    protected virtual void PickUpBag() 
    {
        InteractableBag bag = CollidedObject.GetComponent(typeof(InteractableBag)) as InteractableBag;
        if (bag != null && pickedUpObject.Count < maxHeldObject)
        {
            bag.CollectedBag(CollidedObject);
            PickedUpObject.Push(CollidedObject);
        }
    }
    protected virtual void OpenCloseBin()
    {
        InteractableBin bin = CollidedObject.GetComponent(typeof(InteractableBin)) as InteractableBin;
        if(bin != null)
            bin.OpenCloseBin();
    }

    protected virtual void WithdrawBag()
    {
        InteractableBin bin = CollidedObject.GetComponent(typeof(InteractableBin)) as InteractableBin;
        if (bin != null && PickedUpObject.Count < maxHeldObject)
        {
            GameObject bag = bin.CollectBag();
            if (bag != null)
                PickedUpObject.Push(bag);
        }
    }

    void OnTriggerEnter(Collider other) {
        CollidedObject = other.gameObject;
    }
    void OnTriggerExit(Collider other) {
        CollidedObject = null;
    }
} 