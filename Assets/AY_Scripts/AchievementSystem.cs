using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Achievement
{
    Destructor,
    Hotshot
};

public class AchievementSystem : MonoSingletonGeneric<AchievementSystem>
{
    [SerializeField] private List<AchievementScriptable> achi;
    [SerializeField] private RectTransform panel;
    [SerializeField] private Image badge;
    [SerializeField] private TextMeshProUGUI title;
    private Animator panelAnim;
    private int enemiesDestroyed;
    private int bulletsFired;
    private string panelEntryAnimName = "Achievement Unlocked Entry";
    private string panelExitAnimName = "Achievement Unlocked Exit";

    protected override void Awake()
    {
        base.Awake();
        panelAnim = panel.GetComponent<Animator>();
    }

    private void Start()
    {
        enemiesDestroyed = 0;
        bulletsFired = 0;
        EventsManager.EnemyDeath += EnemyDestyoyed;
        EventsManager.BulletFired += BulletFired;
        EventsManager.PlayerDead += TankDestoyed;
    }

    private void TankDestoyed()
    {
        Unsubscribe();
    }

    private void EnemyDestyoyed()
    {
        enemiesDestroyed++;
        CheckForAchievement();
    }

    private void BulletFired()
    {
        bulletsFired++;
        CheckForAchievement();
    }

    private void CheckForAchievement()
    {
        if(enemiesDestroyed == 10 && !achi[(int)Achievement.Destructor].Unlocked)
        {
            StartCoroutine(DisplayAchievement(achi[(int)Achievement.Destructor]));
        }
        
        if(bulletsFired == 20 && !achi[(int)Achievement.Hotshot].Unlocked)
        {
            StartCoroutine(DisplayAchievement(achi[(int)Achievement.Hotshot]));
        }
    }
    
    private IEnumerator DisplayAchievement(AchievementScriptable achieved)
    {
        badge.sprite = achieved.Badge;
        title.text = achieved.Title;
        achieved.Unlocked = true;
        panelAnim.Play(panelEntryAnimName);
        yield return new WaitForSeconds(5);
        panelAnim.Play(panelExitAnimName);
    }

    private void Unsubscribe()
    {
        EventsManager.EnemyDeath -= EnemyDestyoyed;
        EventsManager.BulletFired -= BulletFired;
        EventsManager.PlayerDead -= TankDestoyed;
    }
}