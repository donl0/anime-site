using System;

namespace Application.Common
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string name, object key)
			: base($"Entity \"{name}\" ({key}) not found") { }
	}
}
