using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(GravitySystem))]
    [RequireComponent(typeof(GravityDataComponent))]
    [RequireComponent(typeof(VelocityYDataComponent))]
    public class JumpSystem : MonoBehaviour
    {
        [SerializeField]
        private float m_JumpForce = 10;

        IGravityData gravityData;
        IVelocityY velocity;
        GravitySystem GravitySystem;
        // Start is called before the first frame update
        private void Start()
        {
            GravitySystem = GetComponent<GravitySystem>();
            gravityData = GetComponent<IGravityData>();
            velocity = GetComponent<IVelocityY>();
        }

        bool jump;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && GravitySystem.ground)
            {
                jump = true;
            }
        }
        private void FixedUpdate()
        {

            if (jump)
            { 
                jump = false;
                velocity.VelocityY += Mathf.Sqrt(m_JumpForce * -1.0f * gravityData.Gravity);

            }
        }

    }

}
