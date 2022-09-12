// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

{
    var derivée = new Derived();
    Console.ReadLine();
}


public class Base
{
    public Base()
    {
        Init();
    }

    public virtual void Init()
    {
        Console.WriteLine("Base.Init");
    }
}

public class Derived : Base
{
    private string s = "Non initialisée!";
    public Derived()
    {
        s = "variable initialisée";
    }

    public override void Init()
    {
        Console.WriteLine("Derived.Init. var s = " + s);
    }
}

