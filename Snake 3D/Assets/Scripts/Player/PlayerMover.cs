using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.MoveX.performed += ctx => MoveX();
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
        
    }

    private void MoveX()
    {

    }
}
