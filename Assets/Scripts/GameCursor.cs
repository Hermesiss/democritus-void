using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GameCursor : MonoBehaviour {
    public enum Mode {
        None,
        Aim,
        Arrow
    }
    
    private void Initialize() {
        _image = GetComponent<Image>();
        Cursor.visible = false;

        _cursors.Add(Mode.None, null);
        _cursors.Add(Mode.Aim, AimSprite);
        _cursors.Add(Mode.Arrow, ArrowSprite);
    }

    public static GameCursor Instance { get; private set; }

    [SerializeField] private Sprite AimSprite;
    [SerializeField] private Sprite ArrowSprite;

    private Image _image;
    private readonly Dictionary<Mode, Sprite> _cursors = new Dictionary<Mode, Sprite>();
    private RectTransform _rectTransform;

    public void SetCursor(Mode mode) {
        _image.sprite = _cursors[mode];
    }

    #region MonoBehaviour

    private void Awake() {
        if (Instance!=null) {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        _rectTransform = GetComponent<RectTransform>();
        Initialize();
        SetCursor(Mode.Aim);
    }

    private void Update() {
        _rectTransform.anchoredPosition = Input.mousePosition;
    }

    #endregion
}