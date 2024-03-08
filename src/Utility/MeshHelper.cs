using Array = Godot.Collections.Array;

namespace Godizmos;

internal static class MeshHelper
{   
    public static void DrawLine(Vector3 from, Vector3 to, Color color)
    {
        var vertices = new[] { from, to };
        var indices = new[] { 0, 1 };

        var geometry = new Geometry(vertices, indices);
        BuildMesh(geometry, color);
    }
    
    public static void DrawCube(Vector3 position, Vector3 size, Color color)
    {
        var geometry = GeometryHelper.GetCubeGeometry(position, size);

        var meshInstance = BuildMesh(geometry, color);
        meshInstance.Position = -(Vector3.One * size) * 0.5f;
    }
    
    public static void DrawCircle(Vector3 position, Vector3 axis, float radius, Color color)
    {
        var geometry = GeometryHelper.GetCircleGeometry(axis, radius, Gizmos.Options.Resolution);

        var meshInstance = BuildMesh(geometry, color);
        meshInstance.Position = position;
    }
    
    public static void DrawSphere(Vector3 position, float radius, Color color)
    {
        var geometry = GeometryHelper.GetSphereGeometry(radius, Gizmos.Options.Resolution);

        var meshInstance = BuildMesh(geometry, color);
        meshInstance.Position = position;
    }

    private static MeshInstance3D BuildMesh(Geometry geometry, Color color)
    {
        var mesh = new ArrayMesh();
        var arrays = new Array();
        var colors = new Color[geometry.Vertices.Length];
        
        for (var i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }

        arrays.Resize((int)Mesh.ArrayType.Max);
        arrays[(int)Mesh.ArrayType.Vertex] = geometry.Vertices;
        arrays[(int)Mesh.ArrayType.Index] = geometry.Indices;
        arrays[(int)Mesh.ArrayType.Color] = colors;

        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, arrays);
        
        var node = new Node3D();
        var material = new StandardMaterial3D
        {
            ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded,
            NoDepthTest = true,
            VertexColorUseAsAlbedo = true,
            Transparency = BaseMaterial3D.TransparencyEnum.Alpha,
            CullMode = BaseMaterial3D.CullModeEnum.Disabled,
        };
        
        var meshInstance = new MeshInstance3D
        {
            Mesh = mesh,
            MaterialOverride = material
        };

        node.AddChild(meshInstance);
        Gizmos.Root.AddChild(node);
        
        return meshInstance;
    }
}