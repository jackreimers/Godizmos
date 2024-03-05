using System.Collections.Generic;
using Godot;

using Array = Godot.Collections.Array;

namespace Godizmos;

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

    public void DrawSphere(Vector3 position, float radius)
    {
        //TODO: Dynamically scale based on radius
        var resolution = 24;
        
        var mesh = new ArrayMesh();
        var surfaceArray = new Array();

        var vertices = new List<Vector3>();
        var indices = new List<int>();

        surfaceArray.Resize((int)Mesh.ArrayType.Max);

        var axisIndex = 0;

        while (axisIndex < 3)
        {
            //Get the angle between each point in radians
            var angle = Mathf.Round(360f / resolution) * (Mathf.Pi / 180f);
            
            for (var i = 0; i < resolution; i++)
            {
                indices.Add(i + axisIndex * resolution);
                var currentAngle = angle * i;

                switch (axisIndex)
                {
                    case 0:
                    {
                        var point = new Vector3(Mathf.Sin(currentAngle), 0f, Mathf.Cos(currentAngle));
                        vertices.Add(point * radius + position);

                        break;
                    }

                    case 1:
                    {
                        var point = new Vector3(0f, Mathf.Sin(currentAngle), Mathf.Cos(currentAngle));
                        vertices.Add(point * radius + position);
                        
                        break;
                    }

                    case 2:
                    {
                        var point = new Vector3(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle), 0f);
                        vertices.Add(point * radius + position);
                        
                        break;
                    }
                }
                
                if (i + 1 >= resolution)
                {
                    indices.Add(i + 1 + (resolution * axisIndex) - resolution);
                }

                else
                {
                    indices.Add(i + 1 + axisIndex * resolution);
                }
            }
            
            axisIndex++;
        }

        surfaceArray[(int)Mesh.ArrayType.Vertex] = vertices.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Index] = indices.ToArray();

        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, surfaceArray);

        var node = new Node3D();
        var meshInstance = new MeshInstance3D();

        node.AddChild(meshInstance);

        meshInstance.Mesh = mesh;
        meshInstance.RotateY(45f * Mathf.Pi / 180f);

        AddChild(node);
    }
}