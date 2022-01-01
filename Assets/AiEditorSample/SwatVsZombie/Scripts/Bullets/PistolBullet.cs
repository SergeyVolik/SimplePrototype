using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PistolBullet : MonoBehaviour, IBullet
{
    public GunFamily Type =>  GunFamily.Pistol;

    [SerializeField]
    private int m_Damage = 10;
    public int Damage => m_Damage;
}
