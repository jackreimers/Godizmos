using System.Linq;

namespace Godizmos;

internal static class GeometryHelper
{
    internal static Geometry GetCubeGeometry(Vector3 position, Vector3 size)
    {
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

        //Offset and scale the shape based on the position and size
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] = vertices[i] * size + position;
        }

        return new Geometry(vertices, indices);
    }

    internal static Geometry GetCircleGeometry(Vector3 rotation, float radius, int resolution)
    {
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
                ? i + 1 - resolution
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

    internal static Geometry GetSphereGeometry(float radius, int resolution)
    {
        var vertices = new List<Vector3>();
        var indices = new List<int>();

        var circle1 = GetCircleGeometry(Vector3.Zero, radius, resolution);
        var circle2 = GetCircleGeometry(new Vector3(90f, 45f, 0f), radius, resolution);
        var circle3 = GetCircleGeometry(new Vector3(90f, 135f, 0f), radius, resolution);

        vertices.AddRange(circle1.Vertices);
        vertices.AddRange(circle2.Vertices);
        vertices.AddRange(circle3.Vertices);

        indices.AddRange(circle1.Indices);
        indices.AddRange(circle2.Indices.Select(s => s + resolution));
        indices.AddRange(circle3.Indices.Select(s => s + resolution * 2));

        return new Geometry(vertices.ToArray(), indices.ToArray());
    }
}