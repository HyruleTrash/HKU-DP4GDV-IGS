using LucasCustomClasses;
using UnityEngine;

public interface IEntity : IPoolable
{
    public GameObject Body {get; set;}
    public void CustomUpdate();
    public void CustomUpdateAtFixedRate();
    void IPoolable.Destroy()
    {
        GameObject.Destroy(Body);
    }
}