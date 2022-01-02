using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class SwatAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private FieldOfViewSystem EyeSensor;
        [SerializeField]
        private Animator m_Controller;
        [SerializeField]
        private RigTarget TargetSetup;
        [SerializeField]
        private MoveSystemComponent controller;
        // Start is called before the first frame update
        private static readonly int m_AnimatorParamX;
        private static readonly int m_AnimatorParamY;
        private static readonly int m_AimParam;
        private static readonly int m_MoveParam;

        private IMoveInputData m_MoveData;
        static SwatAnimatorController()
        {
            m_AnimatorParamX = Animator.StringToHash("Horizontal");
            m_AnimatorParamY = Animator.StringToHash("Vertical");
            m_AimParam = Animator.StringToHash("Aim");
            m_MoveParam = Animator.StringToHash("Move");
        }
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {
            var m_Horizontal = m_MoveData.Horizontal;
            var m_Vertical = m_MoveData.Vertical;
            var forwardDot = Vector3.Dot(transform.forward, Vector3.forward);
            var rightDot = Vector3.Dot(transform.forward, Vector3.right);
            //Debug.Log($"forwardDot {forwardDot} {rightDot}");
            if (forwardDot > 0.9f && forwardDot > rightDot)
            {
                //Debug.Log("Up");

            }
            else if (forwardDot < -0.9 && rightDot > forwardDot)
            {
                //Debug.Log("Down");
                m_Horizontal = -m_Horizontal;
                m_Vertical = -m_Vertical;
            }
            else if (rightDot < -0.9 && rightDot < forwardDot)
            {
                var hor = m_Horizontal;
                m_Horizontal = m_Vertical;
                m_Vertical = hor;
                //Debug.Log("Left");
            }

            else if (rightDot > 0.9 && rightDot > forwardDot)
            {
                var hor = -m_Horizontal;
                m_Horizontal = -m_Vertical;
                m_Vertical = hor;
                //Debug.Log("Right");
            }

            //m_Controller.SetBool(m_MoveParam, controller.IsMove);
            //m_Controller.SetBool(m_AimParam, controller.Aim);
            m_Controller.SetFloat(m_AnimatorParamX, m_MoveData.Horizontal);
            m_Controller.SetFloat(m_AnimatorParamY, m_MoveData.Vertical);

            if (Input.GetMouseButtonDown(1))
            {
                if (EyeSensor.visibleTargets.Count > 0)
                    TargetSetup.SetTarget(EyeSensor.visibleTargets[0]);

            }
            else if (Input.GetMouseButtonUp(1))
            {
                TargetSetup.ClearTarget();
            }
        }
    }
}