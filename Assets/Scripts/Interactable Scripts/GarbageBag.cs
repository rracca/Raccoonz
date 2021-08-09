using UnityEngine;

public class GarbageBag : InteractableBag, IGarbageBag
{
    public GarbageBag() : base("Garbage Bag", BagType.Garbage)
    {
        
    }

    public void DivideBag(GameObject bagToDivide)
    {
        try
        {
            Transform bagToDivideTransform = bagToDivide.transform;

            GameObject firstDividedBag = BagPool.SharedInstance.GetBag(BagType.Garbage, BagSize.Small);
            GameObject secondDividedBag = BagPool.SharedInstance.GetBag(BagType.Garbage, BagSize.Small);

            firstDividedBag.transform.position = new Vector3(bagToDivideTransform.position.x + bagToDivideTransform.localScale.x, bagToDivideTransform.position.y, bagToDivideTransform.position.z);
            secondDividedBag.transform.position = new Vector3(bagToDivideTransform.position.x - bagToDivideTransform.localScale.x, bagToDivideTransform.position.y, bagToDivideTransform.position.z);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Unable to Merge Bag. Error: " + ex);
        }
    }
    public void MergeBag(GameObject firstBagToMerge, GameObject secondBagToMerge, Transform mergedBagTransform)
    {
        try
        {
            BagPool.SharedInstance.ReturnBag(firstBagToMerge);
            BagPool.SharedInstance.ReturnBag(secondBagToMerge);

            GameObject mergedBag = BagPool.SharedInstance.GetBag(BagType.Garbage, BagSize.Large);

            //putting distance of spawning object at least one player scale away
            //edit since this might cause issues for spawning
            //check if the position is free
            mergedBag.transform.position = mergedBagTransform.position + mergedBagTransform.localScale;
            mergedBag.SetActive(true);
        }
        catch(System.Exception ex)
        {
            Debug.Log("Unable to Merge Bag. Error: " + ex);
        }
    }
}
