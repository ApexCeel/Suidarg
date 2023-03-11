using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Space]
    private int _speed;

    private float _horizontalInput;
    private float _verticalInput;
    
    private float _speedBonus = 1;
    public float SpeedBonus
    {
        get => _speedBonus;
        set => _speedBonus = value;
    }

    private Transform _playerTransform;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;
    

    private void Awake()
    {
        _playerTransform = transform;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void CalculateMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(_horizontalInput, _verticalInput, 0);

        _playerTransform.Translate(direction * (_speed * Time.deltaTime * SpeedBonus));

        // Get camera bounds
        var cameraBounds = GetCameraBounds();

        // Get player bounds
        var playerBounds = _spriteRenderer.bounds;

        // Clamp player position to camera view bounds
        var clampedPosition = ClampedPlayerPosition(cameraBounds, playerBounds);

        _playerTransform.position = clampedPosition;
    }

    public void SetSpeed(int speedToSet)
    {
        _speed = speedToSet;
    }

    private Vector3 ClampedPlayerPosition(Bounds cameraBounds, Bounds playerBounds)
    {
        var position = _playerTransform.position;
        var clampedPosition = new Vector3(
            Mathf.Clamp(
                position.x, 
                cameraBounds.min.x + playerBounds.extents.x,
                cameraBounds.max.x - playerBounds.extents.x),
            Mathf.Clamp(
                position.y, 
                cameraBounds.min.y + playerBounds.extents.y,
                cameraBounds.max.y - playerBounds.extents.y),
            position.z
        );
        return clampedPosition;
    }

    private Bounds GetCameraBounds()
    {
        var position = _mainCamera.transform.position;
        var cameraTopRight = _mainCamera.ViewportToWorldPoint(
            new Vector3(1, 1, Mathf.Abs(-position.z)));
        var cameraBottomLeft = _mainCamera.ViewportToWorldPoint(
            new Vector3(0, 0, Mathf.Abs(-position.z)));
        var cameraBounds = new Bounds(
            (cameraTopRight + cameraBottomLeft) / 2, 
            cameraTopRight - cameraBottomLeft);
        return cameraBounds;
    }
}
