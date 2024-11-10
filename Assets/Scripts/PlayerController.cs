using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private Vector3 offset;

    private float maxLeft;
    private float maxRight;
    private float maxDown;
    private float maxUp;

    void Start()
    {
        _camera = Camera.main;

        maxLeft = _camera.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = _camera.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;

        maxDown = _camera.ViewportToWorldPoint(new Vector2(0, 0.05f)).y;
        maxUp = _camera.ViewportToWorldPoint(new Vector2(0, 0.6f)).y;
    }

    void Update()
    {
        if (Touch.fingers.Count > 0 && Touch.fingers[0].isActive)
        {
            Touch myTouch = Touch.fingers[0].currentTouch;
            Vector3 touchPos = myTouch.screenPosition;
            touchPos = _camera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, _camera.nearClipPlane));

            if (myTouch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                offset = touchPos - transform.position;
            }
            if (myTouch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                transform.position = new Vector3(touchPos.x - offset.x, touchPos.y - offset.y, 0);
            }
            if (myTouch.phase == UnityEngine.InputSystem.TouchPhase.Stationary)
            {
                transform.position = new Vector3(touchPos.x - offset.x, touchPos.y - offset.y, 0);
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxLeft,maxRight),Mathf.Clamp(transform.position.y,maxDown,maxUp),0);
        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}
