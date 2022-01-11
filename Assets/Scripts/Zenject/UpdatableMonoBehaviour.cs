using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class UpdatableMonoBehaviour : MonoBehaviour
{
    private GameUpdate _update;

    [Inject]
    void Construct(GameUpdate Update)
    {
        _update = Update;
    }

    public void SubscribeToUpdate(IUpdate updObj)
    {

        _update.SubscribeToUpdate(updObj);
    }
    public void UnsubscribeFromUpdate(IUpdate updObj)
    {

        _update.UnsubscribeFromUpdate(updObj);
    }

    public void SubscribeToFixedUpdate(IFixedUpdate updObj)
    {
        _update.SubscribeToFixedUpdate(updObj);

    }
    public void UnsubscribeFromFixedUpdate(IFixedUpdate updObj)
    {
        _update.UnsubscribeFromFixedUpdate(updObj);

    }

    public void SubscribeToLateUpdate(ILateUpdate updObj)
    {
        _update.SubscribeToLateUpdate(updObj);

    }
    public void UnsubscribeFromLateUpdate(ILateUpdate updObj)
    {
        _update.UnsubscribeFromLateUpdate(updObj);

    }


}

