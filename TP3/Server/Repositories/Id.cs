using System;

namespace IFT585_TP3.Server.Repositories
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class Id : Attribute
    {
    }
}
