using LucasCustomClasses;

public interface IEntity : IPoolable
{
    public void CustomUpdate();
    public void CustomUpdateAtFixedRate();
}