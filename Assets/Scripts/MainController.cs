using System;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // 액션 등록
    public event Action<Vector2> OnMoveEvent;
    
    public event Action OnJumpEvent;
    protected void InvokeMoveEvent(Vector2 move)
    {
        OnMoveEvent?.Invoke(move);
    }

    protected void InvokeJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }
}
