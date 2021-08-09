using System.Collections.Generic;
using UnityEngine;
public abstract class Actor : MonoBehaviour
{
    private GameObject collidedObject;
    private Stack<GameObject> pickedUpObject;
    protected string actorType;
    protected float healthPoints;
    protected int maxHeldObject;
    public GameObject CollidedObject { get => collidedObject; set => collidedObject = value; }
    public Stack<GameObject> PickedUpObject { get => pickedUpObject; set => pickedUpObject = value; }

    protected void InitializeActor() 
    {
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

    //Add indicator visually on which bin is 
    void OnTriggerEnter(Collider other) {
        CollidedObject = other.gameObject;
    }
    void OnTriggerExit(Collider other) {
        if (CollidedObject.GetInstanceID() == other.GetInstanceID())
            CollidedObject = null;
    }
} 