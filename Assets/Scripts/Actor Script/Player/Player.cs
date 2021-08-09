using UnityEngine;

public class Player : Actor, IPlayerController
{ 
    private const int MAX_HELD_OBJECTS = 2;
    
    void Start()
    {
        this.actorType = "Player";
        this.healthPoints = 100;
        this.maxHeldObject = MAX_HELD_OBJECTS;

        InitializeActor();
    }

    void Update() {
        PlayerInput();
    }

    private void PlayerInput() 
    {
        //Interact with Bag
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (PickedUpObject.Count < MAX_HELD_OBJECTS && PickedUpObject.Count >= 0)
            { 
                PickUpBag(); 
            } 
            else if (PickedUpObject.Count == MAX_HELD_OBJECTS)
            {
                CombineBag();
            }
            else
            {
                Debug.Log("Number of objects held is out of bounds.");
            }
        }
        //Open / Close Bin
        if (Input.GetKeyDown(KeyCode.X))
            OpenCloseBin();
        //Interact with Bin
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (PickedUpObject.Count > 0)
                DepositBag();
            else
                WithdrawBag();
        }
        //Scare Raccoon
        if (Input.GetKeyDown(KeyCode.Space))
            ScareAway();
    }

    #region IPlayerController Implementation
    //Removes from Player's <Bag> stack and pushes it to the <Bin> stack
    public void DepositBag()
    {
        InteractableBin bin = CollidedObject.GetComponent(typeof(InteractableBin)) as InteractableBin;
        if (bin != null && PickedUpObject.Count > 0)
        {
            GameObject bagToDeposit = PickedUpObject.Pop();
            //Tests that if it's the wrong bin, it will get pushed back to the hands of the player

            //allow different bag then implement fines for wrong bags
            if (!bin.DepositBag(bagToDeposit))
            {
                PickedUpObject.Push(bagToDeposit);
                Debug.Log("Wrong Bin Bro");
            }
        }
    }
    public void CloseBag()
    {
        InteractableBag bag = CollidedObject.GetComponent(typeof(InteractableBag)) as InteractableBag;
        if (bag != null && bag.IsOpen)
            bag.OpenCloseBag();
    }
    public void CombineBag()
    {
        try
        {
            //Combines the top two of the stack of PickedUpObjects given that they're both small Garbage Bags.
            GameObject firstBagToMerge = PickedUpObject.Pop();
            GameObject secondBagToMerge = PickedUpObject.Pop();

            if (firstBagToMerge.GetComponent<GarbageBag>().Size == BagSize.Small && secondBagToMerge.GetComponent<GarbageBag>().Size == BagSize.Small)
            {
                //passing the transform of the current player
                firstBagToMerge.GetComponent<GarbageBag>().MergeBag(firstBagToMerge, secondBagToMerge, this.transform);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error: " + ex);
        }
    }
    public void ScareAway()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}