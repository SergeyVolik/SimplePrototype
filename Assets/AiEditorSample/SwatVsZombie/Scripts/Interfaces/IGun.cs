using UnityEngine;

namespace SerV112.UtilityAI.Game
{
  
    public interface IGun : IReloadeable, IShootable, IDropable, IEquipable/*, IGunFamily,*/ /*IBulletConteiner*/
    {
        IGunData GunData { get; }
        Vector3 GetPosistion();
        Quaternion GetRotation();
    }

}