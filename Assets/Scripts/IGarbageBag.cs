using UnityEngine;

public interface IGarbageBag
{
    void DivideBag(GameObject bag, GameObject newBag);
    void MergeBag(GameObject firstBag, GameObject secondBag, GameObject newBag);
}
