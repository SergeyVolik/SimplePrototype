using UnityEngine;

namespace SerV112.UtilityAI.Game
{
  
    public interface IGun : IActiveItem, IShootable, IShootEvent, IDroppable, IEquipable/*, IGunFamily,*/ /*IBulletConteiner*/
    {
        IGunData GunData { get; }
        Vector3 GetPosistion();
        Quaternion GetRotation();
    }

}