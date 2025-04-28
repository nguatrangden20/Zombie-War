using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHeath : MonoBehaviour, IHP
{
    [SerializeField] private UIDocument document;
    [SerializeField] private BloodPool bloodPool;
    private UI_HP uiHP;

    public float HP
    {
        get { return hp; }
        set 
        { 
            hp = value;
            uiHP.Progress = hp / 100;
            EffectBlood();
            if (hp <= 0) Death();
        }
    }
    private float hp;

    private void Awake()
    {
        hp = 100;
        uiHP = document.rootVisualElement.Query<UI_HP>();
    }

    private void EffectBlood()
    {
        var effect = bloodPool.Pool.Get();
        effect.transform.parent = transform;
        effect.transform.localPosition = new Vector3(0, 1, 0);
    }

    private void Death()
    {

    }
}
