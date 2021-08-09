using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBin : Interactables
{
    private Stack<GameObject> binContents;
    private int capacity;
    private bool isOpen;
    private BagSize acceptableBagSize;
    private BagType acceptableBagType;
    protected InteractableBin(int Capacity, string ObjectName, BagSize AcceptableBagSize, BagType AcceptableBagType) : base(ObjectName)
    {
        binContents = new Stack<GameObject>();
        acceptableBagSize = AcceptableBagSize;
        acceptableBagType = AcceptableBagType;
        capacity = Capacity;
        IsOpen = false;
    }
    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public virtual void OpenCloseBin()
    {
        IsOpen = !IsOpen;
    }
    public virtual GameObject CollectBag() 
    {
        try
        {
            if (binContents.Count != 0 && IsOpen)
            {
                GameObject bag = binContents.Pop();
                if (bag != null)
                {
                    capacity++;
                    return bag;
                }
                else
                    return null;
            }
            else
                return null;
        }
        catch(System.Exception)
        {
            return null;
        }
    }
    public virtual bool DepositBag(GameObject bag)
    {
        try
        {
            InteractableBag bagToDeposit = bag.GetComponent(typeof(InteractableBag)) as InteractableBag;
            if (binContents.Count < capacity && IsOpen && bagToDeposit.Size == acceptableBagSize && bagToDeposit.Type == acceptableBagType)
            {
                binContents.Push(bag);
                bagToDeposit.DepositedBag();
                capacity--;
                return true;
            }
            else
                return false;
        }
        catch(System.Exception)
        {
            return false;
        }
    }
}