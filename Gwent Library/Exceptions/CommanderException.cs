[Serializable]
public class ExceptionDowodca : Exception
{
	public ExceptionDowodca() { }
	public ExceptionDowodca(string message) : base(message) { }
	public ExceptionDowodca(string message, Exception inner) : base(message, inner) { }
	protected ExceptionDowodca(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}