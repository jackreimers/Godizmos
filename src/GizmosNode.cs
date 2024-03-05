using Array = Godot.Collections.Array;

namespace Godizmos;

public partial class GizmosNode : Node3D
{
    [Export] private bool clearEveryFrame = true;

    public bool ClearEveryFrame
    {
        get => clearEveryFrame;
        set => clearEveryFrame = value;
    }

    public static GizmosNode? Instance { get; private set; }

    public override void _Ready()
    {
        if (Instance != null)
        {
            GD.PushWarning("[Godizmos] Attempted to initialise multiple GizmosNode instances!");
        }

        Instance = this;
    }

    public void DrawLine(Vector3 from, Vector3 to)
    {
        var vertices = new[] { from, to };
        var indices = new[] { 0, 1 };

        var mesh = new ArrayMesh();
        var arrays = new Array();

        arrays.Resize((int)Mesh.ArrayType.Max);
        arrays[(int)Mesh.ArrayType.Vertex] = vertices;
        arrays[(int)Mesh.ArrayType.Index] = indices;

        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, arrays);

        var node = new Node3D();
        var meshInstance = new MeshInstance3D();

        meshInstance.Mesh = mesh;

        node.AddChild(meshInstance);
        AddChild(node);
    }

    public void DrawCube(Vector3 position, Vector3 size)
    {
        var geometry = GizmoHelpers.GetCubeGeometry(position, size);

        var mesh = new ArrayMesh();
        var arrays = new Array();

        arrays.Resize((int)Mesh.ArrayType.Max);
        arrays[(int)Mesh.ArrayType.Vertex] = geometry.Vertices;
        arrays[(int)Mesh.ArrayType.Index] = geometry.Indices;

        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, arrays);

        var node = new Node3D();
        var meshInstance = new MeshInstance3D();

        node.AddChild(meshInstance);

        meshInstance.Mesh = mesh;
        meshInstance.Position = -(Vector3.One * size) * 0.5f;

        AddChild(node);
    }

    public void DrawSphere(Vector3 position, float radius)
    {
        var geometry = GizmoHelpers.GetSphereGeometry(position, radius, 16);

        var mesh = new ArrayMesh();
        var arrays = new Array();

        arrays.Resize((int)Mesh.ArrayType.Max);
        arrays[(int)Mesh.ArrayType.Vertex] = geometry.Vertices;
        arrays[(int)Mesh.ArrayType.Index] = geometry.Indices;

        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, arrays);

        var node = new Node3D();
        var meshInstance = new MeshInstance3D();

        node.AddChild(meshInstance);

        meshInstance.Mesh = mesh;
        meshInstance.RotateY(45f * Mathf.Pi / 180f);

        AddChild(node);
    }
}