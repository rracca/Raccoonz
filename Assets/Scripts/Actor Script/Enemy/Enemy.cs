using UnityEngine;

public class Enemy : Actor, IEnemyRaccoonController
{
    public GameObject smallerBag;
    public Enemy(int maxHeldObject = 1) : base("Enemy", 100) 
    {
        this.maxHeldObject = maxHeldObject;
    }
    public void OpenBag()
    {
        InteractableBag bag = CollidedObject.GetComponent(typeof(InteractableBag)) as InteractableBag;
        if (bag != null && !bag.IsOpen)
            bag.OpenCloseBag();
    }
    public void DivideBag()
    {
        GarbageBag bag = CollidedObject.GetComponent(typeof(GarbageBag)) as GarbageBag;
        if (bag != null)
            bag.DivideBag(CollidedObject);
    }

    public void GetScared()
    {
        throw new System.NotImplementedException();
    }
}
