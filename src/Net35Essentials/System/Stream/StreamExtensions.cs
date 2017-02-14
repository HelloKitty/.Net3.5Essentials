using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.IO
{
	public static class StreamExtensions
	{	
		//Was added in net40. So this backports it.
		/// <summary>
		/// Copys the source into the destination.
		/// </summary>
		/// <param name="destination">The destination stream.</param>
		/// <param name="bufferSize">The size of the buffer to use.</param>
		public static void CopyTo(this Stream source, Stream destination, int bufferSize = 81920)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (destination == null) throw new ArgumentNullException(nameof(destination));
			if (bufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));

			byte[] buffer = new byte[81920]; // Fairly arbitrary size
			int bytesRead;

			while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
			{
				destination.Write(buffer, 0, bytesRead);
			}
		}
	}
}
