using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Appointments.Domain.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}