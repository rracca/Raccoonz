using UnityEngine;

public class GarbageBag : InteractableBag, IGarbageBag
{
    public GarbageBag() : base("Garbage Bag", BagType.Garbage)
    {
        
    }
    public override void DepositedBag(GameObject bag)
    {
        bag.GetComponent<GarbageBag>().Status = BagStatus.Deposited;
    }

    public void DivideBag(GameObject bag, GameObject newBag)
    {
        try
        {
            Transform newBagTransform = bag.transform;
            Destroy(bag);
            Instantiate(newBag, new Vector3(newBagTransform.position.x + newBagTransform.localScale.x, newBagTransform.position.y, newBagTransform.position.z), newBagTransform.rotation);
            Instantiate(newBag, new Vector3(newBagTransform.position.x - newBagTransform.localScale.x, newBagTransform.position.y, newBagTransform.position.z), newBagTransform.rotation);
        }
        catch(System.Exception)
        {

        }
    }
    public void MergeBag(GameObject firstBag, GameObject secondBag)
    {
        try
        {
            Transform newBagPosition = firstBag.transform;
            BagPool.SharedInstance.ReturnBag(BagType.Garbage, firstBag);
            BagPool.SharedInstance.ReturnBag(BagType.Garbage, secondBag);
            //Regular Bag from Bag Pool transform = position of firstbag.transform
            //set active regular bag
        }
        catch(System.Exception)
        {

        }
    }
}
