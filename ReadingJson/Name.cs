using System;

namespace ReadingJson
{
    /// Basic data model for the name of a person
    class Name
    {
        public string last {get; set;}
        public string first {get; set;}

        override public string ToString() {
            return String.Join(", ", last, first);
        }
    }
}
