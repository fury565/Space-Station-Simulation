using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    public interface IEntity
    {
        public string GetEntityDescription();
        public string GetName();
        public int GetCount();
        public int ReturnID();
    }
}
