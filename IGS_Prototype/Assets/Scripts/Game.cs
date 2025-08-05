using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Game : SingletonBehaviour<Game>
{
    [Header("UI")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject deathScreen;
    public GameObject gameUI;
    [Header("Input")]
    [SerializeField] private InputActionReference quitAction;
    [Header("Managers")]
    [SerializeField] private LevelManager levelManager;
    private EntityManager entityManager;

    private void Start()
    {
        entityManager = new EntityManager();
        quitAction.action.performed += ctx => OpenMainMenu();
        mainMenu.SetActive(true);
    }

    private void Update()
    {
        entityManager.CustomUpdate();
        if (gameUI.activeInHierarchy)
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
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        levelManager.LoadLevel(levelManager.currentLevel);
        PlayerInterfaceConsole.Instance.Setup();
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        winScreen.SetActive(false);
        deathScreen.SetActive(false);
        entityManager.DeactivateAllEntities();
    }

    public void TriggerWinScreen()
    {
        entityManager.DeactivateAllEntities();
        gameUI.SetActive(false);
        winScreen.SetActive(true);
    }
    
    public void TriggerDeathScreen()
    {
        entityManager.DeactivateAllEntities();
        gameUI.SetActive(false);
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
