using System;
using System.Collections.Generic;

namespace EfSQLiteTests
{
    public class Person
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public virtual List<int> Numbers { get; set; }

        public Person()
        {
            Id = Guid.NewGuid().ToString();
            Numbers = new List<int>();
        }
    }
}
