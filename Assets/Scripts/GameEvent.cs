using System;

public class GameEvent
{
    public static event Action AttachToMatrix;
    public static event Action LineHasFormed;

    public static void OnAttachToMatrix()
    {
        AttachToMatrix?.Invoke();
    }

    public static void OnLineHasFormed()
    {
        LineHasFormed?.Invoke();
    }
}  

