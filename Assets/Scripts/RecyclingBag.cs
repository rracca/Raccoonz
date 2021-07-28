using UnityEngine;

public class RecyclingBag : InteractableBag
{
    public RecyclingBag() : base("Recycling Bag", BagType.Recycling)
    {
        
    }
    public override void DepositedBag(GameObject bag)
    {
        bag.GetComponent<RecyclingBag>().Status = BagStatus.Deposited;
    }
}
