using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class ElementDAL : IElement
    {
        private readonly SamuraiContext _context;
        public ElementDAL(SamuraiContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteElement = await _context.Elements.FirstOrDefaultAsync(s => s.ElementId == id);
                if (deleteElement == null)
                    throw new Exception($"Data Element dengan id {id} tidak ditemukan");
                _context.Elements.Remove(deleteElement);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Element>> GetAll()
        {
            var elements = await _context.Elements.OrderBy(q => q.Name).ToListAsync();
            return elements;
        }

        public Task<Element> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Element> Insert(Element obj)
        {
            try
            {
                _context.Elements.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public Task<Element> Update(Element obj)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Element>> GetByName(string name)
        {
            var elements = await _context.Elements.Where(c => c.Name.Contains(name))
                .OrderBy(s => s.Name).ToListAsync();
            return elements;
        }
    }
}
