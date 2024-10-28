/** 
 *  AUTHOR : Caio e Silva Barbieri (github: caio-o)
 *  DATE   : 24/10/2024, 10:34
 *  
 *      FILE containing definitions and inpplementations of the classes necessary for 
 *  the arithmetic expression tree. As of they are all here, but soon they will be  
 *  transported to their on files, as decoupled modules.
 */

using Windows.Data.Xml.Dom;
using System;

abstract class Expression
{
    // WARNING: NO PROPERTY
    public abstract bool   IsValid();
    public abstract double GetValue();
    public abstract string AsString();
    
    public string AsInParentheses()
    {
        return "(" + AsString() + ")";
    }
    
    public string GetResultString()
    {
        return this.AsString() +" = "+ this.GetValue();
    }
}

/**
 * Dependencies: Expression
 */
class Number : Expression
{
    public double Val { get; set; }
    
    public override double GetValue() { return Val;       }
    public override bool   IsValid()  { return Val!=null; }
    public override string AsString() { return Val.ToString(); }
    
    public Number(double val)
    {
        Val = val;
    }
}

/**
 * Dependencies: Expression, Number
*/
abstract class BinOp : Expression
{
    public Expression A { get; set; }
    public Expression B { get; set; }
    
    public override bool IsValid()
    {
        return A.IsValid() && B.IsValid();
    }
    
    public abstract override double GetValue();
    public abstract override string AsString();
    
    public BinOp(Expression a, Expression b)
    {
        A = a;
        B = b;
    }
    
    public BinOp(double a, double b):
        this(new Number(a), new Number(b))
    { }
}

class Ratio : BinOp
{
    public Ratio (double a, double b):
        base(a, b)
    { }
    
    public Ratio (Expression a, Expression b):
        base(a, b)
    { }
    
    public override double GetValue()
    {
        if (this.IsValid()) return A.GetValue() / B.GetValue();
        else
        {
            // ACTUALLY IT SHOULD EXIT THE PROGRAM, OR RETURN NULL. THEN IT SHOULD BE "double? GetVale()"
            Console.WriteLine ("INVALID RATIO AAA");
            
            return 0;
        }
    }
    
    public override string AsString()
    {
        string ret = "";
        if(A is Number)
            ret += A.AsString();
        else
            ret += A.AsInParentheses();
        
        ret += "/";
        
        if(B is Number && B.GetValue() >= 0)
            ret += B.AsString();
        else
            ret += B.AsInParentheses();
            
        return ret;
    }
    
    public override bool IsValid()
    {
        return base.IsValid() && B.GetValue() != 0;
    }
}

class Product : BinOp
{
    public Product (double a, double b):
        base(a, b)
    { }
    
    public Product (Expression a, Expression b):
        base(a, b)
    { }
    
    public override double GetValue()
    {
        if (this.IsValid()) return A.GetValue() * B.GetValue();
        else
        {
            // ACTUALLY IT SHOULD EXIT THE PROGRAM, OR RETURN NULL. THEN IT SHOULD BE "double? GetVale()"
            Console.WriteLine ("INVALID RATIO AAA");
            
            return 0;
        }
    }
    
    public override string AsString()
    {
        string ret = "";
        if(A is Number)
            ret += A.AsString();
        else
            ret += A.AsInParentheses();
        
        ret += "*";
        
        if(B is Number && B.GetValue() >= 0)
            ret += B.AsString();
        else
            ret += B.AsInParentheses();
            
        return ret;
    }
    
    public override bool IsValid()
    {
        return base.IsValid();
    }
}
