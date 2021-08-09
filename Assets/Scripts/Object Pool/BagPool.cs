using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPool : MonoBehaviour
{
    public static BagPool SharedInstance;

    private const int MAX_NUMBER_OF_BAGS = 20;

    public int numberOfBags;
    public Queue<GameObject> bagPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        bagPool = new Queue<GameObject>();
        if (bagPool.Count == 0 && numberOfBags < MAX_NUMBER_OF_BAGS)
        {
            InitializeBagPool();
        } 
        else if (numberOfBags >= MAX_NUMBER_OF_BAGS)
        {
            Debug.Log("Number of bag is bigger than set amount of MAX_NUMBER_OF_BAGS: " + MAX_NUMBER_OF_BAGS.ToString());
        }
    }

    void InitializeBagPool()
    {
        GameObject newBag;
        for (int counter = 0; counter < numberOfBags; counter++)
        {
            newBag = new GameObject("Bag#"+ (counter + 1).ToString());
            newBag.transform.SetParent(this.transform);
            bagPool.Enqueue(newBag);
        }
    }

    /// <summary>
    /// public virtual void CollectedBag(GameObject bag)
    /// Please clean <Bag> based on CollectedBag method changes and make sure all are enabled/disabled accordingly.
    /// </summary>
    public GameObject GetBag(BagType bagType, BagSize bagSize)
    {
        //Public T
        //bag.AddComponent<GarbageBag>() as GarbageBag;

        GameObject bagToRelease;
        
        try
        {
            bagToRelease = bagPool.Dequeue().gameObject;
        } 
        catch (System.Exception ex)
        {
            Debug.Log("Error on Getting Bag. Error: " + ex);
            return null;
        }
        

        if (bagType == BagType.Garbage)
        {
            bagToRelease.AddComponent<GarbageBag>().Size = bagSize;
        }
        else if (bagType == BagType.Recycling)
        {
            bagToRelease.AddComponent<RecyclingBag>().Size = bagSize;
        }
        else
        {
            Debug.Log("Type of BagType provided not found.");
            return null;
        }

        return bagToRelease;
    }

    public void ReturnBag (GameObject returnedBag)
    {
        if (returnedBag.GetComponent<InteractableBag>() != null)
        {
            returnedBag.SetActive(false);

            //Or we can safely assume it's always going to be one type of interactable bag class inside a returnedBag so we can just do GetComponent<InteractableBag>
            InteractableBag[] componentToRemove = returnedBag.GetComponents<InteractableBag>();

            for (int counter = 0; counter < componentToRemove.Length; counter++)
            {
                Destroy(componentToRemove[counter]);
            }

            bagPool.Enqueue(returnedBag);
        }
        else
        {
            Debug.Log("Type of returnedBag is invalid.");
        }
    }
}