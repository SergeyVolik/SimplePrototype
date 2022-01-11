using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUpdate
{
    void OnUpdate();
}

public interface IFixedUpdate
{
    void OnFixedUpdate();
}

public interface ILateUpdate
{
    void OnLateUpdate();
}

public class GameUpdate : MonoBehaviour
{

    private List<IUpdate> m_UpdateObj = new List<IUpdate>(1000);
    private List<IFixedUpdate> m_FixedUpdateObj = new List<IFixedUpdate>(1000);
    private List<ILateUpdate> m_LateUpdateObj = new List<ILateUpdate>(1000);

    public void SubscribeToUpdate(IUpdate updObj)
    {
        
        m_UpdateObj.Add(updObj);
    }
    public void UnsubscribeFromUpdate(IUpdate updObj)
    {

        m_UpdateObj.Remove(updObj);
    }

    public void SubscribeToFixedUpdate(IFixedUpdate updObj)
    {

        m_FixedUpdateObj.Add(updObj);
    }
    public void UnsubscribeFromFixedUpdate(IFixedUpdate updObj)
    {

        m_FixedUpdateObj.Remove(updObj);
    }

    public void SubscribeToLateUpdate(ILateUpdate updObj)
    {

        m_LateUpdateObj.Add(updObj);
    }
    public void UnsubscribeFromLateUpdate(ILateUpdate updObj)
    {

        m_LateUpdateObj.Remove(updObj);
    }


    private void Update()
    {
        for (int i = 0; i < m_UpdateObj.Count; i++)
        {
            m_UpdateObj[i].OnUpdate();
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < m_LateUpdateObj.Count; i++)
        {
            m_LateUpdateObj[i].OnLateUpdate();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < m_UpdateObj.Count; i++)
        {
            m_UpdateObj[i].OnUpdate();
        }
    }

}
