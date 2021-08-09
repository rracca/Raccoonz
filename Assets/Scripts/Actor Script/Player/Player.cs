using UnityEngine;

public class Player : Actor, IPlayerController
{ 
    public GameObject regularBag; 
    public Player(int maxHeldObject = 2) : base("Player", 100) 
    {
        this.maxHeldObject = maxHeldObject;
    }

    void Update() {
        PlayerInput();
    }

    private void PlayerInput() 
    {
        //Interact with Bag
        if (Input.GetKeyDown(KeyCode.Z))
            PickUpBag();
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
        //Combine 
        GarbageBag bag = CollidedObject.GetComponent(typeof(GarbageBag)) as GarbageBag;
        if (bag != null && PickedUpObject.Count == maxHeldObject)
        {
            //Get the top part of the stack.
            GameObject recentBag = PickedUpObject.Pop();
            bag.MergeBag(PickedUpObject.Pop(), recentBag);
            PickedUpObject.Clear();
        }
    }
    public void ScareAway()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
