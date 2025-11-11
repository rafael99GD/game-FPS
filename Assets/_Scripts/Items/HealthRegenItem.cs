using System.Collections;
using UnityEngine;

public class HealthRegenItem : Item
{
    [SerializeField] private float totalHealth = 25f;
    [SerializeField] private int totalTicks = 5;
    [SerializeField] private float timeBetweenTicks = 1f;

    private float healthPerTick;

    protected override void Start()
    {
        base.Start();
        healthPerTick = totalHealth / totalTicks;
    }

    protected override void ApplyItem()
    {
        StartCoroutine(ApplyTickAction());
    }

    private IEnumerator ApplyTickAction()
    {
        for (int currentTick = 0; currentTick < totalTicks; currentTick++)
        {
            Debug.Log($"{healthPerTick} health added on tick {currentTick + 1}");
            yield return new WaitForSeconds(timeBetweenTicks);
        }

        FinishApplyItem();
    }
}
