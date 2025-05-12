namespace MoviesProject.Commons.Attributes;

/// <summary>
/// Atributo para determinar que IRequest debe ser auditado.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class AuditLogAttribute : Attribute
{

}
