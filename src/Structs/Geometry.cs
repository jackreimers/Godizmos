namespace Godizmos;

internal struct Geometry
{
    public Vector3[] Vertices { get; }
    public int[] Indices { get; }
    
    public Geometry(Vector3[] vertices, int[] indices)
    {
        Vertices = vertices;
        Indices = indices;
    }
}