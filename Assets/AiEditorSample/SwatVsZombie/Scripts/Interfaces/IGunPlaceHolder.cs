public interface IGunPlaceHolder : IBulletConteiner, IGunFamily
{
    int BulletsInGun { get; set; }
    public void SetUpRealGun(IGun gun);

}
