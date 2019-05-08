using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GameCursor : MonoBehaviour {
    public enum Mode {
        None,
        Aim,
        Arrow
    }

    [SerializeField] private Sprite AimSprite;
    [SerializeField] private Sprite ArrowSprite;

    private SpriteRenderer _spriteRenderer;
    private readonly Dictionary<Mode, Sprite> _cursors = new Dictionary<Mode, Sprite>();

    public void SetCursor(Mode mode) {
        _spriteRenderer.sprite = _cursors[mode];
    }

    #region MonoBehaviour

    private void Awake() {
        Initialize();
        SetCursor(Mode.Aim);
    }

    private void Update() {
        transform.position = GameInput.MousePosition + Vector3.forward;
    }

    #endregion

    private void Initialize() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;

        _cursors.Add(Mode.None, null);
        _cursors.Add(Mode.Aim, AimSprite);
        _cursors.Add(Mode.Arrow, ArrowSprite);
    }
}