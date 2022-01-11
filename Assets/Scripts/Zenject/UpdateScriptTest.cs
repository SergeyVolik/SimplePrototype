using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScriptTest : UpdatableMonoBehaviour, IUpdate, IFixedUpdate, ILateUpdate
{
    public void OnFixedUpdate()
    {
        print("OnFixedUpdate");
    }

    public void OnLateUpdate()
    {
        print("OnLateUpdate");
    }

    public void OnUpdate()
    {
        print("OnUpdate");
    }

    private void OnEnable()
    {       
        SubscribeToUpdate(this);
        //SubscribeToFixedUpdate(this);
        //SubscribeToLateUpdate(this);
    }

    private void OnDisable()
    {
        UnsubscribeFromFixedUpdate(this);
        //UnsubscribeFromLateUpdate(this);
        //UnsubscribeFromUpdate(this);
    }
}
