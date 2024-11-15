using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MainController
{
    private Camera camera;
    void Awake()
    {
        camera = Camera.main;
    }

    public void OnMove(InputValue input)
    {
        Vector2 direction = input.Get<Vector2>().normalized;
        InvokeMoveEvent(direction);
    }
}
