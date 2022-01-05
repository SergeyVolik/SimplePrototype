using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace SerV112.UtilityAI.Game
{
    
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject Panel;

        [SerializeField]
        private TMP_Text m_NumberOfBullets;
        [SerializeField]
        private ThrowWeaponSystem m_ThrowWeaponSystem;
        [SerializeField]
        private EquipWeaponSystem m_EquipWeaponSystem;
        // Start is called before the first frame update
        void Start()
        {
            Panel.SetActive(false);
        }

        private void OnEnable()
        {
            m_EquipWeaponSystem.OnEquipGun.AddListener(EnableUI);
            m_ThrowWeaponSystem.OnEvent.AddListener(DisableUI);
        }

        private void OnDisable()
        {
            m_EquipWeaponSystem.OnEquipGun.RemoveListener(EnableUI);
            m_ThrowWeaponSystem.OnEvent.RemoveListener(DisableUI);
        }

        void DisableUI()
        {
            Panel.SetActive(false);
            currentData?.OnCurrentBulletsChanged.RemoveListener(UpdateBullets);
            currentData = null;
        }

        private IGunData currentData;
        void EnableUI(IGun data)
        {
            currentData = data.GunData;
            Panel.SetActive(true);

            currentData.OnCurrentBulletsChanged.AddListener(UpdateBullets);
            UpdateBullets(currentData.CurrentBullets);
        }

        void UpdateBullets(int bullets)
        {
            m_NumberOfBullets.text = bullets.ToString();
        }

    }

}
