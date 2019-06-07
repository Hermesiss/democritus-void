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
