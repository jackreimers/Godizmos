namespace Godizmos;

public class GizmoHelpers
{
    public static Geometry GetCubeGeometry(Vector3 position, Vector3 size)
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

    public static Geometry GetSphereGeometry(Vector3 position, float radius, int resolution)
    {
        var vertices = new List<Vector3>();
        var indices = new List<int>();

        var axisIndex = 0;

        while (axisIndex < 3)
        {
            //Get the angle between each point in radians
            var angle = Mathf.Round(360f / resolution) * (Mathf.Pi / 180f);

            for (var i = 0; i < resolution; i++)
            {
                var currentAngle = angle * i;

                var sin = Mathf.Sin(currentAngle);
                var cos = Mathf.Cos(currentAngle);

                var point = axisIndex switch
                {
                    0 => new Vector3(sin, 0f, cos),
                    1 => new Vector3(0f, sin, cos),
                    2 => new Vector3(sin, cos, 0f),
                    _ => Vector3.Zero
                };

                vertices.Add(point * radius + position);
                
                var currentIndex = i + axisIndex * resolution;
                var nextIndex = i + 1 == resolution
                    ? currentIndex + 1 - resolution
                    : currentIndex + 1;
                
                indices.Add(currentIndex);
                indices.Add(nextIndex);
            }

            axisIndex++;
        }
        
        return new Geometry(vertices.ToArray(), indices.ToArray());
    }
}