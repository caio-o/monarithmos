using System
using AsithmeticTree.cs

class MathParser
{
	private char cursor;
	
	private char readNext();
	private void nextChar();
	
	private Expression? E();
	private Expression? T();	
	private Expression? F();
	
	private Expression? E()
	{
		Expression? ret = null;
		Expression? tmp = null;
		
		ret = T();
			
		if(ret != null) 
		{ // some would call this block  " E' " or " Edash ". It specifies what can follow a term (either sum/diff or nothing).
			if(cursor == '+')
			{
				tmp = ret;
				ret = new Sum(tmp, T());
				nextChar();
			}
			else if (cursor == '-')
			{
				tmp = ret;
				ret = new Difference(tmp, T());
				nextChar();
			}
		}
		
		return ret;
	}
	
	private Expression? T()
	{
		Expression? ret = null;
		Expression? tmp = null;
		
		ret = F();
		
		if(ret != null)
		{ // Somw would call thhis block " T' " or " Tdash ". It specifies what may follow a Factor (either multiply/divide or nothing)
			if(cursor == '*')
			{
				tmp = ret;
				ret = new Product(tmp, F());
				nextChar();
			}
			else if (cursor == '/') 
			{
				tmp = ret;
				ret = new Ratio(tmp, F());
			}
		}
		
		return ret;
	}
	
	private Expression? F()
	{
		return null;
	}
}