using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    private UIDocument document;
    private VirtualJoystick movement, lookFire;
    private Button switchButton, c4;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        var root = document.rootVisualElement;
        movement = root.Query<VirtualJoystick>("Movement");
        lookFire = root.Query<VirtualJoystick>("LookFire");
        switchButton = root.Query<Button>("Switch");
        c4 = root.Query<Button>("C4");
        switchButton.clicked += SwitchButton_clicked;
        c4.clicked += C4_clicked;
        movement.OnDeltaChange += (x) => inputManager.DetalMovement = new Vector2(x.x, -x.y);
        lookFire.OnDeltaChange += (x) =>
        {
            if (x == Vector2.zero) inputManager.IsFirePress = false;
            else inputManager.IsFirePress = true;
            inputManager.DetalLook = new Vector2(x.x, -x.y);
        };
    }

    private void C4_clicked()
    {
        inputManager.OnC4Click?.Invoke();
    }

    private void SwitchButton_clicked()
    {
        inputManager.OnSwitchClick?.Invoke();
    }
}
