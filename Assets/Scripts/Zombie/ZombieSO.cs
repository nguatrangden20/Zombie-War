using UnityEngine;

[CreateAssetMenu(fileName = "ZombieSO", menuName = "Scriptable Objects/ZombieSO")]
public class ZombieSO : ScriptableObject
{
    public float AttackDistance = 1.5f;
    public float RotationSpeed = 5f;
    public float RotationThreshold = 5;
    public float DestinationRefreshRate = 1;
    public float HP = 100;
}
