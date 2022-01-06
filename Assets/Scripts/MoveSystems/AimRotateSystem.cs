using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IRotationSpeed))]
    [RequireComponent(typeof(IAimInputData))]
    [DisallowMultipleComponent]
    public class AimRotateSystem : MonoBehaviour
    {
        private IRotationSpeed data;
        private IAimInputData aimData;
        private IAimDirection m_AimDir;

        private void Awake()
        {
            m_AimDir = GetComponent<IAimDirection>();
            data = GetComponent<IRotationSpeed>();
            aimData = GetComponent<IAimInputData>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            AimRot();
        }

        private void OnEnable()
        {
            aimData.PressDown.AddListener(PressDownEvt);
            aimData.PressUp.AddListener(PressUpEvt);
        }

        private void OnDisable()
        {
            aimData.PressDown.RemoveListener(PressDownEvt);
            aimData.PressUp.AddListener(PressUpEvt);
        }

        private bool NeedRotate;

        private void PressDownEvt()
        {
            NeedRotate = true;
        }
        private void PressUpEvt()
        {
            NeedRotate = false;
        }

        private void AimRot()
        {
            if (NeedRotate)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_AimDir.Direction), Time.fixedDeltaTime * data.RotationSpeed);
            
        }
    }

}
