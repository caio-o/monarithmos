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
    

    public abstract double GetValue();
    public abstract bool IsValid();
}

abstract class BinOp : Expression
{
    protected double? numA = null;
    protected double? numB = null;
}

class Sum : BinOp
{
    public override bool IsValid()
    {
        return numA != null && numB != null;
    } 

    public override double GetValue()
    {
        if(IsValid())
        {
            return (double)(numA! + numB!);
        }
        else
        {
            Console.WriteLine("    ERROR at Tree.cs, in Sum.GetValue(): INVALID EXPRESSION CANNOT BE EVALUATED.");
            // TODO: EXIT THE PROGRAM WITH AN ERROR.
            return 0;
        }
    }
}

class Subtraction : BinOp
{
    public override bool IsValid()
    {
        return numA != null && numB != null;
    } 

    public override double GetValue()
    {
        if(IsValid())
        {
            return (double)(numA! - numB!);
        }
        else
        {
            Console.WriteLine("    ERROR at Tree.cs, in Sum.GetValue(): INVALID EXPRESSION CANNOT BE EVALUATED.");
            // TODO: EXIT THE PROGRAM WITH AN ERROR.
            return 0;
        }
    }
}

class Product : BinOp
{
    public override bool IsValid()
    {
        return numA != null && numB != null;
    } 

    public override double GetValue()
    {
        if(IsValid())
        {
            return (double)(numA! * numB!);
        }
        else
        {
            Console.WriteLine("    ERROR at Tree.cs, in Sum.GetValue(): INVALID EXPRESSION CANNOT BE EVALUATED.");
            // TODO: EXIT THE PROGRAM WITH AN ERROR.
            return 0;
        }
    }
}

class Ratio : BinOp
{
    public override bool IsValid()
    {
        return numA != null && numB != null && numB != 0;
    } 

    public override double GetValue()
    {
        if(IsValid())
        {
            return (double)(numA! / numB!);
        }
        else
        {
            Console.WriteLine("    ERROR at Tree.cs, in Sum.GetValue(): INVALID EXPRESSION CANNOT BE EVALUATED.");
            // TODO: EXIT THE PROGRAM WITH AN ERROR.
            return 0;
        }
    }
}

class Power : BinOp
{
    public override bool IsValid()
    {
        if(numA != null && numA == 0)
        {
            return numB != null && numB > 0; 
        }
        else
        {
            return numA != null && numB != null;
        }
    } 

    public override double GetValue()
    {
        if(IsValid())
        {
            return Math.Pow((double) numA!, (double) numB!);
        }
        else
        {
            Console.WriteLine("    ERROR at Tree.cs, in Sum.GetValue(): INVALID EXPRESSION CANNOT BE EVALUATED.");
            // TODO: EXIT THE PROGRAM WITH AN ERROR.
            return 0;
        }
    }
}

class Root : BinOp
{
    public override bool IsValid()
    {
        if(numA != null && numA == 0)
        {
            return numB != null && numB > 0; 
        }
        else
        {
            return numA != null && numB != null;
        }
    } 

    public override double GetValue()
    {
        if(IsValid())
        {
            // rt(a, b) = a^(1/b)
            return Math.Pow((double) numA!, (double) (1/numB!) );
        }
        else
        {
            Console.WriteLine("    ERROR at Tree.cs, in Sum.GetValue(): INVALID EXPRESSION CANNOT BE EVALUATED.");
            // TODO: EXIT THE PROGRAM WITH AN ERROR.
            return 0;
        }
    }
}

class ArithTree
{
    
}