namespace Godizmos;

internal class Guard
{
    internal class Against
    {
        public static void NotInitialised()
        {
            if (Gizmos.Root == null!)
            {
                throw new GizmosException("[Godizmos] Gizmos has not been initialised! Call `Gizmos.Initialise` before using any of the draw methods.");
            }
        }
    }
}