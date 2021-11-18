using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _playerPos;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {

        Vector3 touch = _input.Player.TouchPosition.ReadValue<Vector2>();
        Ray ray =  Camera.main.ScreenPointToRay(touch);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, _playerPos.y, _playerPos.z), Time.deltaTime * _speed);

    }
}
