using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.UI;

public class UIController : MonoBehaviour, GameControls.IUniversalActions {
    [SerializeField] private RectTransform pausePanel;
    [SerializeField] private Button continueBtn, saveBtn, loadBtn, settingsBtn, exitBtn;

    [SerializeField] private Slider rotation, acceleration, motionDamping, maximumSpeed, inertiaMult;
    
    private GameControls _controls;
    private bool _menuOpened;

    private void Awake() {
        _controls = new GameControls();
        _controls.Universal.SetCallbacks(this);
        
        SetPause(true);
        
        continueBtn.onClick.AddListener(() => SetPause(false));
        exitBtn.onClick.AddListener(Application.Quit);
        
    }

    void Start() {
        //SetSliders();
    }

    /*private void SetSliders() {
        SetSlider(rotation, PlayerController.Param.RotationSpeed, 360);
        SetSlider(acceleration, PlayerController.Param.Acceleration, 10);
        SetSlider(motionDamping, PlayerController.Param.MotionDamping, 1);
        SetSlider(maximumSpeed, PlayerController.Param.MaximumSpeed, 20);
        SetSlider(inertiaMult, PlayerController.Param.BrakingForce, 300);
    }*/

    /*private void SetSlider(Slider slider, PlayerController.Param param, float maxValue) {
        slider.maxValue = maxValue;
        slider.GetComponentInChildren<TextMeshProUGUI>().text = param.ToString();
        slider.value = PlayerController.Instance.GetParam(param);
        slider.onValueChanged.AddListener(value => {
            PlayerController.Instance.SetParam(param, value);
        });
    }*/

    public void OnMenu(InputAction.CallbackContext context) {
        SetPause(!_menuOpened);
    }
    
    public void OnEnable() {
        _controls.Universal.Enable();
    }

    public void OnDisable() {
        _controls.Universal.Disable();
    }

    private void SetPause(bool mode) {
        _menuOpened = mode;
        pausePanel.gameObject.SetActive(mode);
        Time.timeScale = mode ? 0 : 1;
    }
}
