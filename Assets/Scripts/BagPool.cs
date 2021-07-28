using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPool : MonoBehaviour
{
    public static BagPool SharedInstance;

    private const int MAX_NUMBER_OF_BAGS = 20;
    public int numberOfGarbageBags;
    public Queue<GameObject> garbageBagPool;
    public GameObject garbageBag;

    public int numberOfRecyclingBags;
    public Queue<GameObject> recyclingBagPool;   
    public GameObject recyclingBag;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        if (garbageBag != null && recyclingBag != null)
        {
            InitializeGarbageBagPool();
            InitializeRecyclingBagPool();
        }
    }


    //Fix issue. It should connect/initialize to the BagPool component
    void InitializeGarbageBagPool()
    {
        garbageBagPool = new Queue<GameObject>();
        GameObject tempGarbageBag;
        for (int counter = 0; counter < numberOfGarbageBags; counter++)
        {
            tempGarbageBag = Instantiate(garbageBag);
            tempGarbageBag.SetActive(false);
            garbageBagPool.Enqueue(tempGarbageBag);
        }
    }

    void InitializeRecyclingBagPool ()
    {
        recyclingBagPool = new Queue<GameObject>();
        GameObject tempRecyclingBag;
        for (int counter = 0; counter < numberOfRecyclingBags; counter++)
        {
            tempRecyclingBag = Instantiate(recyclingBag);
            tempRecyclingBag.SetActive(false);
            recyclingBagPool.Enqueue(tempRecyclingBag);
        }
    }

    /// <summary>
    /// public virtual void CollectedBag(GameObject bag)
    /// Please clean <Bag> based on CollectedBag method changes and make sure all are enabled/disabled accordingly.
    /// </summary>

    public GameObject GetBag(BagType bagType, BagSize bagSize)
    {
        GameObject bag;
        //Public T
        //bag.AddComponent<GarbageBag>() as GarbageBag;
        if (bagType == BagType.Garbage)
        {
            bag = garbageBagPool.Dequeue();
            bag.GetComponent<GarbageBag>().Size = bagSize;
        }
        else
        {
            bag = recyclingBagPool.Dequeue();
            bag.GetComponent<RecyclingBag>().Size = bagSize;
        }
        return bag;
    }

    public void ReturnBag (BagType bagType, GameObject bag)
    {
        bag.SetActive(false);
        if (bagType == BagType.Garbage)
        {
            bag.GetComponent<GarbageBag>().IsOpen = false;
            bag.GetComponent<GarbageBag>().Status = BagStatus.Dropped;
            garbageBagPool.Enqueue(bag);
        }
        else
        {
            bag.GetComponent<RecyclingBag>().IsOpen = false;
            bag.GetComponent<RecyclingBag>().Status = BagStatus.Dropped;
            recyclingBagPool.Enqueue(bag);
        }
    }
}
