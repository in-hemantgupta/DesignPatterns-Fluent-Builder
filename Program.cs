using System;

namespace Fluent_Builder
{
    class Program
    {
        internal class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }
        }

        internal class Builder : PersonJobBuilder<Builder>
        {
            public static Builder New() => new Builder();
        }

       internal abstract class PersonBuilder
        {
           internal Person person = new Person();

            public override string ToString()
            {
                return $"Name {person.Name} , Position {person.Position}";
            }

        }

        internal class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
        {
            public SELF CalledAs(string Name)
            {
                person.Name = Name;
                return (SELF)this;
            }
        }

        internal class PersonJobBuilder<SELF> : PersonInfoBuilder<SELF> where SELF : PersonJobBuilder<SELF>
        {

            public SELF WorksAs(string Position)
            {
                person.Position = Position;
                return (SELF)this;
            }


        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var p = Builder.New();
            p.CalledAs("Hemant").WorksAs("SE");
            Console.WriteLine(p);
            Console.Read();
        }
    }
}
