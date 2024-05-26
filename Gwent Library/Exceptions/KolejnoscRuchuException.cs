[Serializable]
public class KolejnoscRuchuException : Exception
{
	public KolejnoscRuchuException() { }
	public KolejnoscRuchuException(string message) : base(message) { }
	public KolejnoscRuchuException(string message, Exception inner) : base(message, inner) { }
	protected KolejnoscRuchuException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}