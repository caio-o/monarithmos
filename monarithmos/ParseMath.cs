using System;
using System.Reflection.Metadata.Ecma335;

namespace ParseMath;

class MathParser
{
	private char cursor = ' ';
	private string buffer = "";
	private int count = 0;
	private bool end = false;

	static private bool IsDigit(char ch)
	{
		return (ch == '0') || (ch == '1') ||
		       (ch == '2') || (ch == '3') ||
		       (ch == '4') || (ch == '5') ||
		       (ch == '6') || (ch == '7') ||
		       (ch == '8') || (ch == '9');
	}

	private void nextChar()
	{
		if(count+1 < buffer.Length)
		{
			count++;
			cursor = buffer[count];
		}
		else
		{
			end = true;
		}

		if (!end && cursor == ' ')
			nextChar();
	}
	
	public Expression? Parse(string buff)
	{
		buffer = buff;

		Console.WriteLine("buffer.Length: " + buffer.Length.ToString());
		
		if (buffer.Length > 0)
		{
			buffer = buff;
			cursor = buff[0];	

			Console.WriteLine("Starting to parse string: " + buff);

			return E();
		}
		else
		{
			return null;
		}
	} 

	private Expression? E()
	{
		Expression? ret;
		Expression? tmp = null;
		
		ret = T();
		
		while (!end && ret!=null && (cursor == '+' || cursor == '-'))
		{ // some would call this block  " E' " or " Edash ". It specifies what can follow a term (either sum/diff or nothing).
			if(cursor == '+')
			{
				nextChar();
				
				tmp = ret;
				ret = T();
				if (ret!=null) ret = new Sum(tmp, ret);
			}
			else
			{
				nextChar();

				tmp = ret;
				ret = T();
				if(ret != null) ret = new Difference(tmp, ret);
			}
		}
		
		return ret;
	}
	
	private Expression? T()
	{
		Expression? ret = null;
		Expression? tmp = null;
		
		ret = F();
		
		while(!end && ret != null && (cursor == '*' || cursor == '/' || cursor == ':'))
		{ // Some would call thhis block " T' " or " Tdash ". It specifies what may follow a Factor (either multiply/divide or nothing)
			if(cursor == '*')
			{
				nextChar();
				tmp = ret;
				ret = F();
				if(ret != null) ret = new Product(tmp, ret);
			}

			else 
			{
				nextChar();
				tmp = ret;
				ret = F();
				if(ret != null) ret = new Ratio(tmp, ret);
			}
		}
		
		//ret = N();

		return ret;
	}
	
	private Expression? F()
	{
		Expression? ret = null;

		if(cursor == '(')
		{
			nextChar();
			ret = E();
			if(ret != null)
			{
				if (cursor == ')')
					nextChar();
			}
			else
			{
				ret = null;
			}
		}
		
		else if (IsDigit (cursor) || cursor == '-')
		{
			ret = N();
		}

		return ret;
	}

	private Expression? N()
	{
		bool gotNumber = false;
		double num = 0;
		int size = 0;

		while (count + size < buffer.Length && (IsDigit(buffer[count+size]) || buffer[count+size] == '.'))
		{
			size++;
			gotNumber = true;
		}

		Console.WriteLine("gotNumber:  " + gotNumber.ToString());
		Console.WriteLine("size:       " + size.ToString());

		if(gotNumber)
		{

			if(count + size >= buffer.Length) size = buffer.Length - count;

			Console.WriteLine("Trying to parse into double: ");
			num = double.Parse (buffer.Substring (count, size));

			count += size-1;
			nextChar();

			Console.WriteLine("Updating cursor: ");
			cursor = buffer[count];

			return new Number(num);
		}
		else
		{
			return null;
		}
	}
}
