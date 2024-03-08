namespace Godizmos;

public static class Gizmos
{
    public static Node Root { get; private set; } = null!;
    public static GizmosOptions Options { get; private set; } = null!;
    
    public static void Initialise(Node root)
    {
        Root = root;
        Options = new GizmosOptions
        {
            Resolution = 32
        };
    }
    
    public static void Initialise(Node root, GizmosOptions options)
    {
        Root = root;
        Options = options;
    }
    
    public static void DrawLine(Vector3 from, Vector3 to)
    {
        Guard.Against.NotInitialised();
        MeshHelper.DrawLine(from, to, Colors.Gray);
    }
    
    public static void DrawLine(Vector3 from, Vector3 to, Color color)
    {
        Guard.Against.NotInitialised();
        MeshHelper.DrawLine(from, to, color);
    }

    public static void DrawCube(Vector3 position, Vector3 size)
    {
        Guard.Against.NotInitialised();
        MeshHelper.DrawCube(position, size, Colors.Gray);
    }
    
    public static void DrawCube(Vector3 position, Vector3 size, Color color)
    {
        Guard.Against.NotInitialised();
        MeshHelper.DrawCube(position, size, color);
    }

    public static void DrawCircle(Vector3 position, Vector3 rotation, float radius)
    {
        Guard.Against.NotInitialised();
        MeshHelper.DrawCircle(position, rotation, radius, Colors.Gray);
    }

    public static void DrawCircle(Vector3 position, Vector3 rotation, float radius, Color color)
    {
        Guard.Against.NotInitialised();
        MeshHelper.DrawCircle(position, rotation, radius, color);
    }

    public static void DrawSphere(Vector3 position, float radius)
    {
        Guard.Against.NotInitialised();
        MeshHelper.DrawSphere(position, radius, Colors.Gray);
    }
    
    public static void DrawSphere(Vector3 position, float radius, Color color)
    {
        Guard.Against.NotInitialised();
        MeshHelper.DrawSphere(position, radius, color);
    }
}