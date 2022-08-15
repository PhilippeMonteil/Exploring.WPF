
# C# Notes

## Delegates, Anonymous methods, Lambda expressions, Variance

### Exemple

    public static class Class1
    {

        public delegate int Mydelegate(int par0);

        static int method0(int par0)
        {
            return par0;
        }

    public static class Class1
    {
        delegate int Mydelegate(int par0);

        static int method0(int par0)
        {
            return par0;
        }

        public static void Test0()
        {

            {
                Mydelegate d = method0;
                int v = d(100);
            }

            // delegate <- anonymous method
            {
                Mydelegate d = delegate (int par)
                {
                    return par * 10;
                };
                int v = d(100);
            }

            // delegate <- lambda expression
            {
                Mydelegate d = p => p * 100;
                int v = d(100);
            }
        }
	
### [Lambda expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)

You use a lambda expression to create an anonymous function.

Use the lambda declaration operator => to separate the lambda's parameter list from its body.

A lambda expression can be of any of the following two forms:

- Expression lambda that has an __expression__ as its body:

    (input-parameters) => expression

- Statement lambda that has a __statement__ block as its body:

    (input-parameters) => { <sequence-of-statements> }

Any lambda expression can be __converted to a delegate type__. 

    Func<int, int> square = x => x * x;

Expression lambdas can also be __converted to the expression tree types__,
as the following example shows:

    System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
    Console.WriteLine(e);
    // Output:
    // x => (x * x)

