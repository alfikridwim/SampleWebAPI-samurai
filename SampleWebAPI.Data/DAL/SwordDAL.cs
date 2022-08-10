using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class SwordDAL : ISword
    {
        private readonly SamuraiContext _context;
        public SwordDAL(SamuraiContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSword == null)
                    throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
                _context.Swords.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Sword>> GetAll()
        {
            var swords = await _context.Swords.OrderBy(q => q.Weight).ToListAsync();
            return swords;
        }
        public async Task<IEnumerable<Sword>> GetAllSword(PaggingParam obj)
        {
            var swords = await _context.Swords.Include(s=>s.SType)
                .Include(s => s.SType)
                .OrderBy(q => q.Id)
                .Skip((obj.PageNumber - 1) * obj.PageSize)
                .Take(obj.PageSize)
                .ToListAsync();
            return swords;
        }
        public async Task<IEnumerable<Sword>> InsertSwordWType(Sword obj)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        public  Task<Sword> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Sword>> GetByName(string name)
        {
            var swords = await _context.Swords.Where(c => c.Name.Contains(name))
                .OrderBy(s => s.Name).ToListAsync();
            return swords;
        }
        public async Task<Sword> Insert(Sword obj)
        {
            try
            {
                _context.Swords.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> Update(Sword obj)
        {
            try
            {
                var updateSword= await _context.Swords.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSword == null)
                    throw new Exception($"Data Sword dengan id {obj.Id} tidak ditemukan");

                updateSword.Name = obj.Name;
                updateSword.Weight = obj.Weight;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Sword>> AddElementToExistingSword(Sword obj)
        {
            try
            {
                var updateSword = await _context.Swords.Include(e => e.Elements).FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSword == null)
                    throw new Exception($"Data Sword dengan id {obj.Id} tidak ditemukan");

                //var aviElements = obj.Elements.ElementId;
                updateSword.Elements = obj.Elements;
                await _context.SaveChangesAsync();
                throw new ArgumentNullException(nameof(obj));
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }
        public async Task<IEnumerable<Sword>> DelElementToExistingSword(Sword obj)
        {
            try
            {
                var updateSword = await _context.Swords.Include(e => e.Elements).FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSword == null)
                    throw new Exception($"Data Sword dengan id {obj.Id} tidak ditemukan");
                updateSword.Elements.Clear();
                await _context.SaveChangesAsync();
                throw new ArgumentNullException(nameof(obj));
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }


        }
    }
}
