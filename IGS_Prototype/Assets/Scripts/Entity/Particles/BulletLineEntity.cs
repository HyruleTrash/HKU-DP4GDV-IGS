using System;
using UnityEngine;

public class BulletLineEntity : IEntity
{
    public GameObject Body {get; set;}
    private LineRenderer lineRenderer;
    private Material material;
    public bool Active { get; set; }

    public BulletLineEntity(Material material)
    {
        this.material = material;
    }
    
    public void OnEnableObject()
    {
        InitializeComponents();
        Body.SetActive(true);
        
        Color startColor = Color.white;
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = startColor;
    }

    public void OnDisableObject()
    {
        Body.SetActive(false);
        lineRenderer.SetPositions(Array.Empty<Vector3>());
    }

    private void InitializeComponents()
    {
        if (Body == null)
            Body = new GameObject("BulletLine");
        if (lineRenderer == null)
        {
            lineRenderer = Body.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(material);
            lineRenderer.SetPositions(Array.Empty<Vector3>());
            lineRenderer.startWidth = lineRenderer.endWidth = 0.01f;
        }
    }

    public void SetData(Vector3 origin, Vector3 target, Material material)
    {
        this.material = material;
        InitializeComponents();
        
        lineRenderer.SetPositions(new [] { origin, target });
    }

    public void DoDie()
    {
        Game.instance.GetEntityManager().entityPool.DeactivateObject(this);
    }

    public void CustomUpdate()
    {
        if (!Active)
            return;
        Color color = lineRenderer.startColor;

        // Reduce alpha over time
        color.a = Mathf.MoveTowards(color.a, 0f, Time.deltaTime);
        
        // Update LineRenderer colors
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        
        if (color.a <= 0f)
        {
            DoDie();
        }
    }

    public void CustomUpdateAtFixedRate()
    {
        // throw new System.NotImplementedException();
    }
}