using System.Reflection;

namespace MoviesProject.Commons;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
