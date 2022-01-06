using UnityEngine;

namespace SerV112.UtilityAI.Game
{
  
    public interface IGun : IShootable, IShootEvent, IDropable, IEquipable/*, IGunFamily,*/ /*IBulletConteiner*/
    {
        IGunData GunData { get; }
        Vector3 GetPosistion();
        Quaternion GetRotation();
    }

}