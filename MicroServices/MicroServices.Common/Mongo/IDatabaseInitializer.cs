using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitialzeAsync();

    }
}
