[Serializable]
public class ZaMaloKartException : Exception
{
	public ZaMaloKartException() { }
	public ZaMaloKartException(string message) : base(message) { }
	public ZaMaloKartException(string message, Exception inner) : base(message, inner) { }
	protected ZaMaloKartException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}