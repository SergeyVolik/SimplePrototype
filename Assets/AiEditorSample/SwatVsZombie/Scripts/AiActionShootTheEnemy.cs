using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(FieldOfViewSystem))]
    [RequireComponent(typeof(AIAgentSimpleAI))]
    [RequireComponent(typeof(AIAimInputDataComponent))]
    [RequireComponent(typeof(HandComponent))]

    public class AiActionShootTheEnemy : MonoBehaviour
    {
        AIAimInputDataComponent AimInputDataComponent;
        FieldOfViewSystem Fov;
        AIAgentSimpleAI agentBrain;
        HandComponent HandComponent;
        // Start is called before the first frame update
        void Awake()
        {
            AimInputDataComponent = GetComponent<AIAimInputDataComponent>();
            agentBrain = GetComponent<AIAgentSimpleAI>();
            Fov = GetComponent<FieldOfViewSystem>();
            HandComponent = GetComponent<HandComponent>();
        }

        bool seeEnemy;
        bool prevSeeEnemy;
        // Update is called once per frame
        void Update()
        {
          
            seeEnemy = Fov.visibleTargets.Count > 0 ? true : false;

            var dataOut = agentBrain.GetOutData();

            if (prevSeeEnemy != seeEnemy)
            {
                
                var dataIn = agentBrain.GetInData();

                print(seeEnemy);
                AimInputDataComponent.UpdateInput(seeEnemy);
                dataIn.SeeEnemy = Convert.ToSingle(seeEnemy);
                agentBrain.ChangeAgentData(dataIn);
            }

            if (dataOut.SimpleAiActions == SimpleAiActions.ShootToEnemy && seeEnemy)
            {

                var pos = Fov.visibleTargets[0].position;

                AimInputDataComponent.UpdateDirection(Vector3.ProjectOnPlane(pos - transform.position, Vector3.up).normalized);
                
                //HandComponent.ActiveGun.Shoot();
            
            }

            prevSeeEnemy = seeEnemy;
        }
    }

}
