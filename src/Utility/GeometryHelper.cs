using System.Linq;

namespace Godizmos;

internal static class GeometryHelper
{
    public static Geometry GetSquareGeometry(Vector2 size)
    {
        var vertices = new Vector2[]
        {
            new(-0.5f, -0.5f),
            new(0.5f, -0.5f),
            new(0.5f, 0.5f),
            new(-0.5f, 0.5f),
        };

        var indices = new[]
        {
            0, 1,
            1, 2,
            2, 3,
            3, 0
        };

        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] *= size;
        }

        return new Geometry(
            vertices.Select(s => new Vector3(s.X, 0f, s.Y)).ToArray(), 
            indices);
    }

    public static Geometry GetCubeGeometry(Vector3 size)
    {
        var vertices = new Vector3[]
        {
            new(-0.5f, -0.5f, -0.5f),
            new(0.5f, -0.5f, -0.5f),
            new(0.5f, -0.5f, 0.5f),
            new(-0.5f, -0.5f, 0.5f),
            new(-0.5f, 0.5f, -0.5f),
            new(0.5f, 0.5f, -0.5f),
            new(0.5f, 0.5f, 0.5f),
            new(-0.5f, 0.5f, 0.5f)
        };

        //Denotes vertices that are connected to form lines
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

        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] *= size;
        }

        return new Geometry(vertices.ToArray(), indices.ToArray());
    }

    public static Geometry GetCircleGeometry(float radius, int resolution)
    {
        return GetCircleGeometry(radius, resolution, Vector3.Zero);
    }

    private static Geometry GetCircleGeometry(float radius, int resolution, Vector3 rotation)
    {
        if (resolution < 6)
        {
            resolution = 6;
        }

        //Ensure resolution is a multiple of 6 to prevent gaps in the geometry
        var remainder = resolution % 6;
        if (remainder != 0)
        {
            resolution -= remainder;
        }

        var vertices = new List<Vector3>();
        var indices = new List<int>();

        //Get the angle between each point in radians
        var angle = Mathf.Round(360f / resolution) * (Mathf.Pi / 180f);

        for (var i = 0; i < resolution; i++)
        {
            var currentAngle = angle * i;

            var sin = Mathf.Sin(currentAngle);
            var cos = Mathf.Cos(currentAngle);

            vertices.Add(new Vector3(sin, 0f, cos) * radius);

            var nextIndex = i + 1 == resolution
                ? 0
                : i + 1;

            indices.Add(i);
            indices.Add(nextIndex);
        }

        rotation = new Vector3(
            Mathf.DegToRad(rotation.X),
            Mathf.DegToRad(rotation.Y),
            Mathf.DegToRad(rotation.Z));

        var rotationBasis = Basis.FromEuler(rotation);

        for (var i = 0; i < vertices.Count; i++)
        {
            vertices[i] = rotationBasis * vertices[i];
        }

        return new Geometry(vertices.ToArray(), indices.ToArray());
    }

    public static Geometry GetSphereGeometry(float radius, int resolution)
    {
        var vertices = new List<Vector3>();
        var indices = new List<int>();

        var circle1 = GetCircleGeometry(radius, resolution);
        var circle2 = GetCircleGeometry(radius, resolution, new Vector3(90f, 45f, 0f));
        var circle3 = GetCircleGeometry(radius, resolution, new Vector3(90f, 135f, 0f));

        vertices.AddRange(circle1.Vertices);
        vertices.AddRange(circle2.Vertices);
        vertices.AddRange(circle3.Vertices);

        indices.AddRange(circle1.Indices);
        indices.AddRange(circle2.Indices.Select(s => s + resolution));
        indices.AddRange(circle3.Indices.Select(s => s + resolution * 2));

        return new Geometry(vertices.ToArray(), indices.ToArray());
    }
}