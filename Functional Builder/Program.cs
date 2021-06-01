using System;
using System.Collections.Generic;
using System.Linq;

namespace Functional_Builder
{
    class Program
    {
       public    class Person
        {
            public string Name, Position;
            public override string ToString()
            {
                return $"Name {Name} Position {Position}";
            }
        }

        public class FunctionalBuilder<TSub, TSelf>
        where TSub : new()
        where TSelf : FunctionalBuilder<TSub, TSelf>{

            private List<Func<TSub, TSub>> actions = new List<Func<TSub, TSub>>();

            public TSelf Do(Action<TSub> action) => AddAction(action);

            public TSub Build() => actions.Aggregate(new TSub(), (p, f) => f(p));

            private TSelf AddAction(Action<TSub> action) {
                actions.Add(p => { action(p); return p; });
                return (TSelf)this;
            }
        }

        public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
        {
            public PersonBuilder CalledAs(string name) {
                return Do(p => p.Name = name);
            }

            public PersonBuilder WithPosition(string position) {
                return Do(p => p.Position = position);
            }
        }

        static void Main(string[] args)
        {
            var p = new PersonBuilder().CalledAs("hemant").WithPosition("SE").Build();
            Console.WriteLine(p.ToString()); ;
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
