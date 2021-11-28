using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lookDelta;
    [SerializeField] private Vector3 _playerPos;

    private GameManager _gameManager;
    private PlayerInput _input;

    public float PlayerSpeed => _speed;
    public Vector3 PlayerPos => _playerPos;

    public PlayerInput Input => _input;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
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

        if (Physics.Raycast(ray, out hit) && !_gameManager.FeverModeIsActive)
        {
            Vector3 hitPoint = new Vector3(hit.point.x, _playerPos.y, _playerPos.z);
            Vector3 lookPoint = new Vector3(hit.point.x, _playerPos.y, _playerPos.z + _lookDelta);

            transform.position = Vector3.Lerp(transform.position, hitPoint, Time.deltaTime * _speed);
            transform.LookAt(lookPoint, Vector3.up);

            if (Mathf.Abs((hitPoint - transform.position).magnitude) < 0.01)
            {
                transform.position = hitPoint;
                transform.rotation = Quaternion.identity;
            }
        }

    }
}
