using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Game : SingletonBehaviour<Game>
{
    [Header("UI")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject deathScreen;
    public GameObject gameInterface;
    [Header("Input")]
    [SerializeField] private InputActionReference quitAction;
    [Header("Managers")]
    [SerializeField] private LevelManager levelManager;
    private EntityManager entityManager;

    private void Start()
    {
        entityManager = new EntityManager();
        quitAction.action.performed += ctx => OpenMenu();
        menu.SetActive(true);
    }

    private void Update()
    {
        entityManager.CustomUpdate();
        if (gameInterface.activeInHierarchy)
            LevelTimer.Instance.Update();
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
        gameInterface.SetActive(true);
        levelManager.LoadLevel(levelManager.currentLevel);
        PlayerInterfaceConsole.Instance.Setup();
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        gameInterface.SetActive(false);
        endScreen.SetActive(false);
        deathScreen.SetActive(false);
        entityManager.DeactivateAllEntities();
    }

    public void TriggerEndScreen()
    {
        entityManager.DeactivateAllEntities();
        gameInterface.SetActive(false);
        endScreen.SetActive(true);
    }
    
    public void TriggerDeathScreen()
    {
        entityManager.DeactivateAllEntities();
        gameInterface.SetActive(false);
        deathScreen.SetActive(true);
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
        if (entityManager.entityPool.GetInActiveObject(typeof(PlayerEntity), out var result))
            entityManager.entityPool.DestroyObject(result);
        GunInventoryUI.Instance.Reset();
    }
}
