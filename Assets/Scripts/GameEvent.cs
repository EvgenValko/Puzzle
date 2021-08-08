using System;

public class GameEvent
{
    public static event Action OnJoin;
    public static event Action OnLine;

    public static void Join()
    {
        OnJoin?.Invoke();
    }

    public static void Line()
    {
        OnLine?.Invoke();
    }
}  

