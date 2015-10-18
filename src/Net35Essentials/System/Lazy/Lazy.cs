using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	//Actual: http://referencesource.microsoft.com/#mscorlib/system/Lazy.cs
	//.Net 3.5 Lazy<T> implementation http://stackoverflow.com/questions/3207580/implementation-of-lazyt-for-net-3-5
	/// <summary>
	/// Provides support for lazy initialization.
	/// </summary>
	/// <typeparam name="T">Specifies the type of object that is being lazily initialized.</typeparam>
	public sealed class Lazy<T>
	{
		private readonly object syncObj = new object();
		private readonly Func<T> createValue;
		private readonly bool threadSafe;
		private bool isValueCreated;
		private T value;

		/// <summary>
		/// Gets the lazily initialized value of the current Lazy{T} instance.
		/// </summary>
		public T Value
		{
			get
			{
				if (threadSafe)
					//Double check locking
					if (!isValueCreated)
					{
						lock (syncObj)
						{
							if (!isValueCreated)
							{
								value = createValue();
								isValueCreated = true;
							}
						}
					}
					else
						//No locking since thread safety wasn't requested
						if (!isValueCreated)
						{
							value = createValue();
							isValueCreated = true;
						}

				return value;
			}
		}

		/// <summary>
		/// Gets a value that indicates whether a value has been created for this Lazy{T} instance.
		/// </summary>
		public bool IsValueCreated
		{
			get
			{
				if (threadSafe)
					lock (syncObj)
					{
						return isValueCreated;
					}
				else
					return isValueCreated;
			}
		}


		/// <summary>
		/// Initializes a new instance of the Lazy{T} class.
		/// </summary>
		/// <param name="createValue">The delegate that produces the value when it is needed.</param>
		public Lazy(Func<T> createValue, bool isThreadSafe)
		{
			if (createValue == null) 
				throw new ArgumentNullException("createValue");

			threadSafe = isThreadSafe;
			this.createValue = createValue;
		}


		/// <summary>
		/// Creates and returns a string representation of the Lazy{T}.Value.
		/// </summary>
		/// <returns>The string representation of the Lazy{T}.Value property.</returns>
		public override string ToString()
		{
			if (threadSafe)
				lock (syncObj)
				{
					return Value.ToString();
				}
			else
				return Value.ToString();
		}
	}
}
