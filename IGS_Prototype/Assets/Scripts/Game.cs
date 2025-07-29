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
        levelManager.currentLevel = 0; //TODO: make this a feature later
    }

    private void Update()
    {
        entityManager.CustomUpdate();
    }

    private void FixedUpdate()
    {
        entityManager.CustomUpdateAtFixedRate();
    }

    public EntityManager GetEntityManager()
    {
        return entityManager;
    }

    public void NextLevel()
    {
        entityManager.DeactivateAllEntities();
        levelManager.AdvanceLevel();
    }
}
