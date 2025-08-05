
using LucasCustomClasses;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelTimer", menuName = "LevelManagement/LevelTimer")]
public class LevelTimer : ScriptableObjectSingleton<LevelTimer>
{
    [SerializeField] private TextPrefab textHolderPrefab;
    private Timer timer;
    [HideInInspector] public float maxTime;

    public void Setup(float newMaxTime)
    {
        CheckTextHolder();
        timer = new Timer(newMaxTime, OnTimerFinished);
        timer.running = true;
        maxTime = newMaxTime;
        textHolderPrefab.instanceTextComponent.text = "";
    }

    private void CheckTextHolder()
    {
        if (textHolderPrefab.instance)
            return;
        textHolderPrefab.Instantiate(Game.instance.gameInterface.transform);
    }

    private void OnTimerFinished()
    {
        textHolderPrefab.instanceTextComponent.text = "";
        if (!Game.instance.GetEntityManager().entityPool
                .GetActiveObject(typeof(PlayerEntity), out var result)) return;
        var playerEntity = (PlayerEntity)result;
        playerEntity.DoDie();
    }

    public void Update()
    {
        timer?.Update(Time.deltaTime);
        textHolderPrefab.instanceTextComponent.text = timer.GetFormattedTime(true);
    }
}