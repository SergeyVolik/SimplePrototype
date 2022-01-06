using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IEnemyDetectedEvent))]
    [RequireComponent(typeof(AIAgentSimpleAI))]
    [RequireComponent(typeof(AIAimInputDataComponent))]
    [RequireComponent(typeof(HandData))]
    [RequireComponent(typeof(ThrowItemAIInputDataComponent))]
    [DisallowMultipleComponent]
    public class AiActionShootTheEnemy : MonoBehaviour
    {
        AIAimInputDataComponent AimInputDataComponent;
        IEnemyDetectedEvent Fov;
        AIAgentSimpleAI agentBrain;
        HandData HandComponent;
        ThrowItemAIInputDataComponent ThrowItemAIInputDataComponent;
        // Start is called before the first frame update
        void Awake()
        {
            ThrowItemAIInputDataComponent = GetComponent<ThrowItemAIInputDataComponent>();
            AimInputDataComponent = GetComponent<AIAimInputDataComponent>();
            agentBrain = GetComponent<AIAgentSimpleAI>();
            Fov = GetComponent<IEnemyDetectedEvent>();
            HandComponent = GetComponent<HandData>();
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
        void UpdateHasGunData()
        {
            var dataIn = agentBrain.GetInData();
            dataIn.HasGun = 0f;
            agentBrain.ChangeAgentData(dataIn);
        }
        void Update()
        {
          
           

            if (seeEnemy)
            {
                var dataOut = agentBrain.GetOutData();

                if(lastTraget != null)
                    AimInputDataComponent.UpdateDirection(Vector3.ProjectOnPlane(lastTraget.position - transform.position, Vector3.up).normalized);

                switch (dataOut.SimpleAiActions)
                {

                    case SimpleAiActions.ThrowWeaponToEnemy:
                        ThrowItemAIInputDataComponent.PressDown.Invoke();
                        UpdateHasGunData();
                        break;
                    case SimpleAiActions.ShootToEnemy:
                      

                        if (ShootDelay < t)
                        {
                            UpdateBulletsData(HandComponent.ActiveGun.Shoot());
                            t = 0;
                        }
                        break;
                    default:
                        break;
                }

                t += Time.deltaTime;
            }
        }
    }

}
