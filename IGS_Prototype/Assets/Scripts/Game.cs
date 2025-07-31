using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : SingletonBehaviour<Game>
{
    [Header("Input")]
    [SerializeField] private InputActionReference quitAction;
    [SerializeField] private GameObject menu;
    [Header("Managers")]
    [SerializeField] private LevelManager levelManager;
    private EntityManager entityManager;

    private void Start()
    {
        entityManager = new EntityManager();
        quitAction.action.performed += ctx => OpenMenu();
    }

    private void Update()
    {
        entityManager.CustomUpdate();
    }

    private void FixedUpdate()
    {
        entityManager.CustomUpdateAtFixedRate();
    }

    private void OnDrawGizmos()
    {
        entityManager?.OnDrawGizmos();
    }

    public void StartGame()
    {
        menu.SetActive(false);
        levelManager.LoadLevel(levelManager.currentLevel);
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        entityManager.DeactivateAllEntities();
    }
    
    public static void CloseGame()
    {
        #if UNITY_EDITOR
        // Exit play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Quit the built application
        Application.Quit();
        #endif
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

    public void ResetSaveData()
    {
        levelManager.currentLevel = 0;
    }
}
