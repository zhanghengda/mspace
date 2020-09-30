using System;
using System.Text;

namespace Mmc.Windows.Utils
{
	internal class UrlDecoder
	{
		private readonly int _bufferSize;

		private byte[] _byteBuffer;

		private readonly char[] _charBuffer;

		private readonly Encoding _encoding;

		private int _numBytes;

		private int _numChars;

		internal UrlDecoder(int bufferSize, Encoding encoding)
		{
			this._bufferSize = bufferSize;
			this._encoding = encoding;
			this._charBuffer = new char[bufferSize];
		}

		internal void AddByte(byte b)
		{
			bool flag = this._byteBuffer == null;
			if (flag)
			{
				this._byteBuffer = new byte[this._bufferSize];
			}
			byte[] arg_39_0 = this._byteBuffer;
			int numBytes = this._numBytes;
			this._numBytes = numBytes + 1;
			arg_39_0[numBytes] = b;
		}

		internal void AddChar(char ch)
		{
			bool flag = this._numBytes > 0;
			if (flag)
			{
				this.FlushBytes();
			}
			char[] arg_2F_0 = this._charBuffer;
			int numChars = this._numChars;
			this._numChars = numChars + 1;
			arg_2F_0[numChars] = ch;
		}

		private void FlushBytes()
		{
			bool flag = this._numBytes > 0;
			if (flag)
			{
				this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
				this._numBytes = 0;
			}
		}

		internal string GetString()
		{
			bool flag = this._numBytes > 0;
			if (flag)
			{
				this.FlushBytes();
			}
			bool flag2 = this._numChars > 0;
			string result;
			if (flag2)
			{
				result = new string(this._charBuffer, 0, this._numChars);
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
