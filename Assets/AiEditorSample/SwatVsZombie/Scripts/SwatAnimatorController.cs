using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatAnimatorController : MonoBehaviour
{
    [SerializeField]
    private FieldOfView EyeSensor;
    [SerializeField]
    private Animator m_Controller;
    [SerializeField]
    private RigTarget TargetSetup;
    [SerializeField]
    private Controller controller;
    // Start is called before the first frame update
    private static readonly int m_AnimatorParamX;
    private static readonly int m_AnimatorParamY;
    private static readonly int m_AimParam;
    private static readonly int m_MoveParam;


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
        m_Controller.SetBool(m_MoveParam, controller.Move);
        m_Controller.SetBool(m_AimParam, controller.Aim);
        m_Controller.SetFloat(m_AnimatorParamX, controller.Horizontal);
        m_Controller.SetFloat(m_AnimatorParamY, controller.Vertical);

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
