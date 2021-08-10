using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class ItemAttribute
    {
        public int ID { get; private set; }
        public int Value {get; private set;}
        public ItemAttribute(int id, int value)
        {
            ID=id;
            Value = value;
        }
        public override string ToString()
        {
            return ID.ToString() + " +" + Value.ToString();
        }
    }
}
