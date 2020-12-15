using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Tank", menuName = "Tank")]
public class TankTypesScriptable : ScriptableObject
{
    public string tankName;
    public int health;
    public int damage;
    public float speed;
}
