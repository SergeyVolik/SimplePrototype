using System.Collections.Generic;

public interface IBulletConteiner
{
    IEnumerable<IBullet> Bullets { get; set; }
}
