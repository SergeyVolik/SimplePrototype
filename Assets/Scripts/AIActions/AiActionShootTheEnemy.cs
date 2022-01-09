using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IEnemyDetectedEvent))]
    [RequireComponent(typeof(AIAgentSimpleAI))]
    [RequireComponent(typeof(AIAimInputDataComponent))]
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(ThrowItemAIInputDataComponent))]
    [DisallowMultipleComponent]
    public class AiActionShootTheEnemy : MonoBehaviour
    {
        AIAimInputDataComponent AimInputDataComponent;
        IEnemyDetectedEvent Fov;
        AIAgentSimpleAI agentBrain;
        Player Player;
        ThrowItemAIInputDataComponent ThrowItemAIInputDataComponent;
        // Start is called before the first frame update
        void Awake()
        {
            ThrowItemAIInputDataComponent = GetComponent<ThrowItemAIInputDataComponent>();
            AimInputDataComponent = GetComponent<AIAimInputDataComponent>();
            agentBrain = GetComponent<AIAgentSimpleAI>();
            Fov = GetComponent<IEnemyDetectedEvent>();
            Player = GetComponent<Player>();
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
            var dataIn = agentBrain.InData;
            lastTraget = target;
            if (seeEnemy)
                AimInputDataComponent.PressDown.Invoke();
            else AimInputDataComponent.PressUp.Invoke();

            dataIn.SeeEnemy = Convert.ToSingle(seeEnemy);
            agentBrain.ChangeAgentData(dataIn);
        }

        void UpdateBulletsData(int bullets)
        {

            var dataIn = agentBrain.InData;
            dataIn.Ammo = bullets;
            agentBrain.ChangeAgentData(dataIn);
        }
        void UpdateHasGunData()
        {

            var dataIn = agentBrain.InData;
            dataIn.HasGun = 0f;
            agentBrain.ChangeAgentData(dataIn);
        }
        void Update()
        {



            if (seeEnemy)
            {
                var dataOut = agentBrain.OutData;

                if (lastTraget != null)
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
                            Player.DoAction();
                            var bullets = 0;
                            if (Player.LeftHand.ActiveGun != null)
                                bullets = Player.LeftHand.ActiveGun.GunData.CurrentBullets;
                            if (Player.RightHand.ActiveGun != null)
                                bullets +=Player.RightHand.ActiveGun.GunData.CurrentBullets;

                            UpdateBulletsData(bullets);
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
