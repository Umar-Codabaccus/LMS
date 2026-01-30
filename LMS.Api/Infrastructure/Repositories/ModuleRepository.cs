using LMS.Api.Domain.Entities;
using LMS.Api.Infrastructure.Context;
using LMS.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using LMS.Api.Infrastructure.ValueObjects;

namespace LMS.Api.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Module> _db;

        public ModuleRepository(AppDbContext context)
        {
            _context = context;
            _db = context.Set<Module>();
        }

        public Module Add(Module module)
        {
            try
            {
                _db.Add(module);
                _context.SaveChanges();
                return module;
            } 
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Delete(Module module)
        {
            try
            {
                _db.Remove(module);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting module: {e}");
                return false;
            }
        }

        public Module Get(Guid moduleId)
        {
            var module = _db.Find(moduleId);

            if (module is null)
            {
                Console.WriteLine("Module not found");
            }

            return module;
        }

        public List<Module> GetAll(Guid courseId)
        {
            var modules = _db.Where(module => module.CourseId == courseId).ToList();
            return modules;
        }

        public bool Update(Module module)
        {
            try
            {
                _db.Update(module);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating module: {e}");
                return false;
            }
        }

        public int GetOrder(Guid courseId)
        {
            var modules = _db.Where(module => module.CourseId == courseId).ToList();

            int max = 0;

            foreach(var module in modules)
            {
                if (module.ModuleOrder >= max)
                {
                    max = module.ModuleOrder;
                }
            }

            return max;
        }

        public bool UpdateBulk(IEnumerable<Module> modules)
        {
            foreach(var module in modules)
            {
                _db.Update(module);
            }

            int result = _context.SaveChanges();

            return result > 0;
        }
    }
}
