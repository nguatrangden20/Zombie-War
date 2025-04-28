using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class UI_HP : VisualElement
{
    [UxmlAttribute]
    public float Progress
    {
        get => progress;
        set
        {
            progress = Mathf.Clamp01(value);
            UpdateProgress();
        }
    }

    private float progress;
    private VisualElement HP;

    public UI_HP()
    {
        name = "container";
        AddToClassList("hp-container");

        HP = new VisualElement { name = "progress" };
        HP.AddToClassList("progress");
        Add(HP);

        UpdateProgress();
    }

    private void UpdateProgress()
    {
        if (HP != null)
        {
            HP.style.width = new Length(progress * 100f, LengthUnit.Percent);
        }
    }
}
