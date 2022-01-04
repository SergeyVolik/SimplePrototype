using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IEnemyDetectedEvent))]
    [RequireComponent(typeof(AIAgentSimpleAI))]
    [RequireComponent(typeof(AIAimInputDataComponent))]
    [RequireComponent(typeof(HandComponent))]
    [RequireComponent(typeof(ThrowItemAIInputDataComponent))]

    public class AiActionShootTheEnemy : MonoBehaviour
    {
        AIAimInputDataComponent AimInputDataComponent;
        IEnemyDetectedEvent Fov;
        AIAgentSimpleAI agentBrain;
        HandComponent HandComponent;
        ThrowItemAIInputDataComponent ThrowItemAIInputDataComponent;
        // Start is called before the first frame update
        void Awake()
        {
            AimInputDataComponent = GetComponent<AIAimInputDataComponent>();
            agentBrain = GetComponent<AIAgentSimpleAI>();
            Fov = GetComponent<IEnemyDetectedEvent>();
            HandComponent = GetComponent<HandComponent>();
        }

        private void OnEnable()
        {
            Fov.OnTargetLost.AddListener(TargetLost);
            Fov.OnTargetDetected.AddListener(TargetDetected);
        }
        private void OnDisable()
        {
            Fov.OnTargetLost.RemoveListener(TargetLost);
            Fov.OnTargetDetected.RemoveListener(TargetDetected);
        }

        void TargetDetected(Transform target)
        {
            seeEnemy = true;
            UpdateSeeData(target);
        }
        void TargetLost(Transform target)
        {
            seeEnemy = false;
            UpdateSeeData(null);
        }

        // Update is called once per frame
        [SerializeField]
        private float ShootDelay = 0.5f;
        private float t;
        bool seeEnemy;
        Transform lastTraget;
        void UpdateSeeData(Transform target)
        {
            var dataIn = agentBrain.GetInData();
            lastTraget = target;
            print(seeEnemy);
            if (seeEnemy)
                AimInputDataComponent.PressDown.Invoke();
            else AimInputDataComponent.PressUp.Invoke();

            dataIn.SeeEnemy = Convert.ToSingle(seeEnemy);
            agentBrain.ChangeAgentData(dataIn);
        }

        void UpdateBulletsData(int bullets)
        {
            var dataIn = agentBrain.GetInData();
            dataIn.Ammo = bullets;
            agentBrain.ChangeAgentData(dataIn);
        }
        void Update()
        {
          
            var dataOut = agentBrain.GetOutData();


            if (dataOut.SimpleAiActions == SimpleAiActions.ShootToEnemy && seeEnemy)
            {

                AimInputDataComponent.UpdateDirection(Vector3.ProjectOnPlane(lastTraget.position - transform.position, Vector3.up).normalized);

                if (ShootDelay < t)
                {
                    UpdateBulletsData(HandComponent.ActiveGun.Shoot());
                    t = 0;
                }
            
            }

            t+= Time.deltaTime;
        }
    }

}
