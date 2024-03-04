using Godot;
using Godot.Collections;

namespace Strategy;

public partial class GizmosNode : Node3D
{
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
        var mesh = new ArrayMesh();
        var arrays = new Array();
        
        arrays.Resize((int)Mesh.ArrayType.Max);
        arrays[(int)Mesh.ArrayType.Vertex] = vertices;
        
        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, arrays);
        
        var node = new Node3D();
        var meshInstance = new MeshInstance3D();
        
        meshInstance.Mesh = mesh;
        
        node.AddChild(meshInstance);
        AddChild(node);
    }

    public void DrawCube(Vector3 position, Vector3 size)
    {
        var mesh = new ArrayMesh();
        var arrays = new Array();
        
        var vertices = new Vector3[]
        {
            new(0, 0, 0),
            new(1, 0, 0),
            new(1, 0, 1),
            new(0, 0, 1),
            new(0, 1, 0),
            new(1, 1, 0),
            new(1, 1, 1),
            new(0, 1, 1)
        };
        
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] = vertices[i] * size + position;
        }
        
        var indices = new[]
        {
            0, 1,
            1, 2,
            2, 3,
            3, 0,

            4, 5,
            5, 6,
            6, 7,
            7, 4,

            0, 4,
            1, 5,
            2, 6,
            3, 7
        };
        
        arrays.Resize((int)Mesh.ArrayType.Max);
        arrays[(int)Mesh.ArrayType.Vertex] = vertices;
        arrays[(int)Mesh.ArrayType.Index] = indices;

        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, arrays);

        var node = new Node3D();
        var meshInstance = new MeshInstance3D();

        node.AddChild(meshInstance);

        meshInstance.Mesh = mesh;
        meshInstance.Position = -(Vector3.One * size) * 0.5f;

        AddChild(node);
    }
}