using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IGravityData))]
    [RequireComponent(typeof(IVelocityY))]
    public class GravitySystem : MonoBehaviour
    {

        IGravityData data;
        IVelocityY velocity;
        CharacterController controller;
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
            data = GetComponent<IGravityData>();
            velocity = GetComponent<IVelocityY>();
        }

        // Update is called once per frame
        Vector3 playerVelocity;
        public bool ground;
        void FixedUpdate()
        {
          
            //Debug.Log(groundedPlayer);
            if (ground && velocity.VelocityY < 0)
            {
                velocity.VelocityY = 0f;
            }


            velocity.VelocityY += data.Gravity * Time.fixedDeltaTime;
            controller.Move(new Vector3(0, velocity.VelocityY, 0) * Time.fixedDeltaTime);
            ground = controller.isGrounded;


        }
    }

}
