using UnityEngine;

public class Enemy : Actor, IEnemyRaccoonController
{
    private const int MAX_HELD_OBJECTS = 1;

    void Start ()
    {
        this.actorType = "Enemy";
        this.healthPoints = 100;
        this.maxHeldObject = MAX_HELD_OBJECTS;

        InitializeActor();
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
