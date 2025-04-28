using UnityEngine;

public class ZombieAttackCollider : MonoBehaviour
{
    [SerializeField] private ZombieSO data;
    private void OnTriggerEnter(Collider other)
    {
        PlayerHeath playerHeath = other.GetComponent<PlayerHeath>();
        if (playerHeath == null) return;

        playerHeath.HP -= data.AttackDamage;
    }
}
