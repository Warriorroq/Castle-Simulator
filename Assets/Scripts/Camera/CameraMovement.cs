using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]private float _moveSpeed;
    [SerializeField]private Rect _screenRect;
    private Vector2 _screenCenter;
    private Vector3 _moveVector;
    private void Start()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        _screenCenter = screenSize / 2f;
        _screenRect.position = screenSize * 0.01f;
        _screenRect.width = screenSize.x * 0.98f;
        _screenRect.height = screenSize.y * 0.98f;
    }
    private void Update()
    {
        if (!_screenRect.Contains(Input.mousePosition))
        {
            _moveVector = ((Vector2)Input.mousePosition - _screenCenter).normalized;
            _moveVector *= _moveSpeed * Time.deltaTime;
            _moveVector.z = _moveVector.y;
            _moveVector.y = 0;
            transform.position += _moveVector;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += Vector3.up * Time.deltaTime * _moveSpeed;
        }

        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position -= Vector3.up * Time.deltaTime * _moveSpeed;
        }
    }
}
