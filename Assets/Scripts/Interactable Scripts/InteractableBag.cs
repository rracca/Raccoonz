using UnityEngine;

public abstract class InteractableBag : Interactables
{
    [SerializeField]
    private BagSize size;
    private BagType type;
    private BagStatus status;
    private bool isOpen;
    protected InteractableBag(string objectName, BagType bagType) : base(objectName)
    {
        IsOpen = false;
        size = BagSize.Large;
        type = bagType;
        status = BagStatus.Dropped;
    }
    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public BagType Type { get => type; set => type = value; }
    public BagStatus Status { get => status; set => status = value; }
    public BagSize Size { get => size; set => size = value; }

    public void OpenCloseBag()
    {
        this.isOpen = !this.isOpen;
    }
    public void DepositedBag()
    {
        this.status = BagStatus.Deposited;
    }

    public virtual void CollectedBag(GameObject bag)
    {
        InteractableBag collectedBag = bag.GetComponent(typeof(InteractableBag)) as InteractableBag;
        if (collectedBag != null)
        {
            bag.GetComponent<MeshRenderer>().enabled = false;
            bag.GetComponent<BoxCollider>().enabled = false;
            bag.SetActive(false);
            collectedBag.Status = BagStatus.Collected;
        }
    }
}

public enum BagStatus
{
    Dropped, //Outside of the Bin
    Collected, //On Hold
    Deposited //Inside of the Bin
}

public enum BagType
{
    Recycling,
    Garbage
}

public enum BagSize
{
    Large, //Normal Bag
    Small //Divided Bag
}