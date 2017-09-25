using System;
using System.Collections.Generic;
using System.Text;

namespace bank_utility
{
    class Bic
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Bic(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
