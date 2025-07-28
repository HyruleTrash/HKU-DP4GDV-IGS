using System;
using UnityEngine;

public class Game : SingletonBehaviour<Game>
{
    [SerializeField] private GameObject startButton;
    
    [SerializeField] private LevelManager levelManager;
    private EntityManager entityManager;

    public void StartGame()
    {
        startButton.SetActive(false);
        levelManager.LoadLevel(levelManager.currentLevel);
    }

    private void Start()
    {
        entityManager = new EntityManager();
    }

    private void Update()
    {
        entityManager.Update();
    }

    public EntityManager GetEntityManager()
    {
        return entityManager;
    }
}
