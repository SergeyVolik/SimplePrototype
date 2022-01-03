using System.Collections.Generic;

namespace SerV112.UtilityAI.Game
{
    public interface IBulletConteiner
    {
        IEnumerable<IBullet> Bullets { get; set; }
    }

}