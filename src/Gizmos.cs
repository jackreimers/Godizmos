namespace Godizmos;

public static class Gizmos
{
    public static void DrawLine(Vector3 from, Vector3 to)
    {
        if (GizmosNode.Instance == null)
        {
            GD.PushError($"[Godizmos] {nameof(GizmosNode)} instance not found! Did you forget to add a {nameof(GizmosNode)} to the scene?");
            return;
        }

        GizmosNode.Instance.DrawLine(from, to);
    }
    
    public static void DrawCube(Vector3 position, Vector3 size)
    {
        if (GizmosNode.Instance == null)
        {
            GD.PushError($"[Godizmos] {nameof(GizmosNode)} instance not found! Did you forget to add a {nameof(GizmosNode)} to the scene?");
            return;
        }

        GizmosNode.Instance.DrawCube(position, size);
    }
    
    public static void DrawSphere(Vector3 position, float radius)
    {
        if (GizmosNode.Instance == null)
        {
            GD.PushError($"[Godizmos] {nameof(GizmosNode)} instance not found! Did you forget to add a {nameof(GizmosNode)} to the scene?");
            return;
        }

        GizmosNode.Instance.DrawSphere(position, radius);
    }
}