using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    public class PistolBullet : MonoBehaviour, IBullet
    {

        [SerializeField]
        private int m_Damage = 10;
        public int Damage => m_Damage;
    }
}