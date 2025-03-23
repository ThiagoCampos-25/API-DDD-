using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Generics;
using Entities.Entities;

namespace Domain.Interfaces
{
    public interface IMessage : IGeneric<Message>
    {
    }
}
