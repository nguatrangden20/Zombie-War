using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class VirtualJoystick : VisualElement
{
    public Vector2 joystickDelta; // Between -1 and 1

    private Vector2 joystickPointerDownPosition;
    private VisualElement joystickBack;
    private VisualElement joystickHandle;

    public VirtualJoystick()
    {
        name = "Joystick";

        joystickBack = new VisualElement() { name = "JoystickBack" };
        joystickBack.AddToClassList("joystick-back");
        Add(joystickBack);

        joystickHandle = new VisualElement() { name = "JoystickHandle" };
        joystickHandle.AddToClassList("joystick-handle");
        joystickBack.Add(joystickHandle);

        joystickHandle.RegisterCallback<PointerDownEvent>(OnPointerDown);
        joystickHandle.RegisterCallback<PointerUpEvent>(OnPointerUp);
        joystickHandle.RegisterCallback<PointerMoveEvent>(OnPointerMove);
    }

    void OnPointerDown(PointerDownEvent e)
    {
        joystickHandle.CapturePointer(e.pointerId);
        joystickPointerDownPosition = e.position;
    }

    void OnPointerUp(PointerUpEvent e)
    {
        joystickHandle.ReleasePointer(e.pointerId);
        joystickHandle.transform.position = Vector3.zero;
        joystickDelta = Vector2.zero;
    }

    void OnPointerMove(PointerMoveEvent e)
    {
        if (!joystickHandle.HasPointerCapture(e.pointerId))
            return;
        var pointerCurrentPosition = (Vector2)e.position;
        var pointerMaxDelta = (joystickBack.worldBound.size - joystickHandle.worldBound.size) / 2;
        var pointerDelta = Clamp(pointerCurrentPosition - joystickPointerDownPosition, -pointerMaxDelta,
            pointerMaxDelta);
        joystickHandle.transform.position = pointerDelta;
        joystickDelta = pointerDelta / pointerMaxDelta;
    }

    static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max) =>
        new Vector2(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));
}
