using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public interface ISword : ICrud<Sword>
    {
        Task<IEnumerable<Sword>> GetByName(string name);
        Task<IEnumerable<Sword>> GetAllSword(PaggingParam obj);
        Task<IEnumerable<Sword>> InsertSwordWType(Sword obj);
        Task<IEnumerable<Sword>> AddElementToExistingSword(Sword obj);
        Task<IEnumerable<Sword>> DelElementToExistingSword(Sword obj);
    }
}
