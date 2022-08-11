
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsControlLib
{

    public class ClassA : List<ClassB>
    {
        public string? Name { get; init; }
        public string Title => $"Title({Name})";
    }

    public class ClassB : List<ClassC>
    {
        public string? Name { get; init; }
        public string Title => $"Title({Name})";
    }

    public class ClassC
    {
        public string? Name { get; init; }
        public string Title => $"Title({Name})";
    }

    public class ClassATest : ClassA
    {
        public override string ToString()
        {
            return $"{GetType().Name}";
        }

        public ClassATest()
        {
            Name = $"{GetType().Name}";

            for (int i = 0; i < 5; i++)
            {
                ClassB classB = new ClassB()
                {
                    Name = $"{nameof(ClassB)}[{i}]",
                };

                for (int j = 0; j < 3; j++)
                {
                    classB.Add(new ClassC()
                    {
                        Name=$"{nameof(ClassC)}[{i},{j}]",
                    });
                }

                Add(classB);
            }
        }
    }

}
