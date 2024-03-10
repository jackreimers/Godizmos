namespace Godizmos;

public static class Gizmos
{
    public static bool Enabled { get; set; } = true;
    
    public static Node Root { get; private set; } = null!;
    public static GizmosOptions Options { get; private set; } = null!;
    
    public static void Initialise(Node root)
    {
        Root = root;
        Options = new GizmosOptions
        {
            Resolution = 60
        };
    }
    
    public static void Initialise(Node root, GizmosOptions options)
    {
        Root = root;
        Options = options;
    }
    
    public static Node3D DrawLine(Vector3 from, Vector3 to)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawLine(from, to, Colors.Gray);
    }
    
    public static Node3D DrawLine(Vector3 from, Vector3 to, Color color)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawLine(from, to, color);
    }

    public static Node3D DrawSquare(Vector3 position, Vector2 size)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawSquare(position, size, Colors.Gray);
    }
    
    public static Node3D DrawSquare(Vector3 position, Vector2 size, Color color)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawSquare(position, size, color);
    }
    
    public static Node3D DrawCube(Vector3 position, Vector3 size)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawCube(position, size, Colors.Gray);
    }
    
    public static Node3D DrawCube(Vector3 position, Vector3 size, Color color)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawCube(position, size, color);
    }

    public static Node3D DrawCircle(Vector3 position, float radius)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawCircle(position, radius, Colors.Gray);
    }

    public static Node3D DrawCircle(Vector3 position, float radius, Color color)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawCircle(position, radius, color);
    }

    public static Node3D DrawSphere(Vector3 position, float radius)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawSphere(position, radius, Colors.Gray);
    }
    
    public static Node3D DrawSphere(Vector3 position, float radius, Color color)
    {
        Guard.Against.NotInitialised();
        return MeshHelper.DrawSphere(position, radius, color);
    }
}