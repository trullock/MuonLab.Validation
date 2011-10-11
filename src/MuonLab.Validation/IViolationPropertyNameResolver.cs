namespace MuonLab.Validation
{
	public interface IViolationPropertyNameResolver
	{
		string ResolvePropertyName(IViolation violation);
	}

}