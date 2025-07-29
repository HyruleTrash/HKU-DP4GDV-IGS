using LucasCustomClasses;

public interface IEntity : IPoolable
{
    public void CustomUpdate();
    public void CustomUpdateAtFixedRate();
    new void DoDie() => Game.instance.GetEntityManager().entityPool.DeactivateObject(this);
}