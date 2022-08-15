
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
    Func<int, int, bool> testForEquality = (x, y) => x == y;

Expression lambdas can also be __converted to the expression tree types__,
as the following example shows:

    System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
    Console.WriteLine(e);
    // Output:
    // x => (x * x)

### Async lambdas

#### Exemple

    public static async Task<int> Test0()
    {
        Func<Task<int>> d = async () =>
        {
            await Task.Delay(1000);
            return 1;
        };
        //
        return await d();
    }

### Static lambdas

Beginning with C# 9.0, you can apply the static modifier to a lambda expression to prevent 
unintentional capture of local variables or instance state by the lambda:

    Func<double, double> square = static x => x * x;

A static lambda can't capture local variables or instance state from enclosing scopes, 
but may reference static members and constant definitions.

### Variance

#### Exemple

        // delegate covariant
        public delegate T Mydelegate1<out T>();

        // delegate non variant
        public delegate T Mydelegate2<T>(out T par0);
        // Error : public delegate T Mydelegate2A<out T>(out T par0);
        // Error : public delegate T Mydelegate2B<out T>(ref T par0);

        // delegate contravariant
        public delegate void Mydelegate3<in T>(T par0);

        static string method2(out string par0)
        {
            return par0 = "Test";
        }

        public static void Test1()
        {
            // delegate generic fermé
            {
                Mydelegate2<string> d0 = method2;
            }

            // Variance
            {
                Mydelegate1<string> d1 = () => String.Empty;
                Mydelegate1<object> d0 = d1;
                object v = d0();
            }

            // Contravariance
            {
                Mydelegate3<object> d0 = null;
                Mydelegate3<string> d1 = d0;
            }

            {
                Mydelegate1<int> d = delegate ()
                {
                    return 777;
                };
            }
        }
