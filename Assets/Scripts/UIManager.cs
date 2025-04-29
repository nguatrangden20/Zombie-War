using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    private UIDocument document;
    private VirtualJoystick movement, lookFire;
    private Button switchButton, c4, restart;
    private VisualElement gameOver;
    private Label timeText, title;
    public float totalTime = 180f;
    private float currentTime;


    private void Awake()
    {
        document = GetComponent<UIDocument>();

        var root = document.rootVisualElement;
        gameOver = root.Query<VisualElement>("GameOver");
        movement = root.Query<VirtualJoystick>("Movement");
        lookFire = root.Query<VirtualJoystick>("LookFire");
        switchButton = root.Query<Button>("Switch");
        restart = root.Query<Button>("restart");
        timeText = root.Query<Label>("timeCount");
        title = root.Query<Label>("title");
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
        restart.clicked += Restart_clicked;
    }

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        if (PlayerHeath.IsGameOver) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);

        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            ShowPopupWin();
        }
    }

    private void ShowPopupWin()
    {
        PlayerHeath.IsGameOver = true;
        ShowGameOver();
        title.text = "You Win!!!";
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowGameOver()
    {
        gameOver.style.display = DisplayStyle.Flex;
    }

    private void Restart_clicked()
    {
        SceneManager.LoadScene("Main");
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
