using UnityEngine;
using System.Collections;
using TMPro;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class AchievementScriptable : ScriptableObject
{
    public Sprite Badge;
    public string Title;
    public bool Unlocked = false;
}
