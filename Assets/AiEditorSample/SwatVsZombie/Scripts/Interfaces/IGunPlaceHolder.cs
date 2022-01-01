namespace SerV112.UtilityAI.Game
{
    public interface IGunPlaceholder : IBulletConteiner, /*IGunFamily,*/ IDropable
    {
        public void SetUpRealGun(IGun gun);

    }
}
