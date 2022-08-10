using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    internal interface IElementSword : ICrud<ElementSword>
    {
     Task<IEnumerable<ElementSword>> AddElementToExistingSword(ElementSword obj);
    }
}
