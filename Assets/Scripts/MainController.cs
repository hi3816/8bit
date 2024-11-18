using System;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // 액션 등록
    public event Action<Vector2> OnMoveEvent;
    protected void InvokeMoveEvent(Vector2 move)
    {
        OnMoveEvent?.Invoke(move);
    }
}
