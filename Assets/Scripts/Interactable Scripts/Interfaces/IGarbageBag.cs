using UnityEngine;

public interface IGarbageBag
{
    void DivideBag(GameObject bagToDivide);
    void MergeBag(GameObject firstBagToMerge, GameObject secondBagToMerge);
}
