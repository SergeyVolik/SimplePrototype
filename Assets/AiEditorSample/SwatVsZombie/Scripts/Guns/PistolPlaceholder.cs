using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class PistolPlaceholder : MonoBehaviour, IGunPlaceholder
    {
        public IEnumerable<IBullet> Bullets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GunFamily Type => GunFamily.Pistol;



        public void Drop()
        {
            throw new NotImplementedException();
        }

        public void SetUpRealGun(IGun gun)
        {
            gun.Bullets = Bullets;
        }
    }

}