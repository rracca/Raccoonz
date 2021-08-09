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
    public void MergeBag(GameObject firstBagToMerge, GameObject secondBagToMerge)
    {
        try
        {
            Transform mergedBagPosition = firstBagToMerge.transform;
            BagPool.SharedInstance.ReturnBag(firstBagToMerge);
            BagPool.SharedInstance.ReturnBag(secondBagToMerge);

            GameObject mergedBag = BagPool.SharedInstance.GetBag(BagType.Garbage, BagSize.Large);

            mergedBag.transform.position = mergedBagPosition.position;
            mergedBag.SetActive(true);
        }
        catch(System.Exception ex)
        {
            Debug.Log("Unable to Merge Bag. Error: " + ex);
        }
    }
}
