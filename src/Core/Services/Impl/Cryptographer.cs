using System;
using System.Security.Cryptography;
using System.Text;

namespace CodeCampServer.Core.Services.Impl
{
	public class Cryptographer : ICryptographer
	{
		/// <summary>
		/// Create salt for encrypting user passwords.  
		/// Original Source: http://davidhayden.com/blog/dave/archive/2004/02/16/157.aspx
		/// </summary>
		/// <returns></returns>
		public string CreateSalt()
		{
			int size = 64;
			//Generate a cryptographic random number.
			var rng = new RNGCryptoServiceProvider();
			var buff = new byte[size];
			rng.GetBytes(buff);

			// Return a Base64 string representation of the random number.
			return Convert.ToBase64String(buff);
		}

		/// <summary>
		/// Create a password hash based on a password and salt.  
		/// Adapted from: http://davidhayden.com/blog/dave/archive/2004/02/16/157.aspx
		/// </summary>
		/// <returns></returns>
		public string ComputeHash(string valueToHash)
		{
			HashAlgorithm algorithm = SHA512.Create();
			byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(valueToHash));

			return Convert.ToBase64String(hash);
		}
	}
}