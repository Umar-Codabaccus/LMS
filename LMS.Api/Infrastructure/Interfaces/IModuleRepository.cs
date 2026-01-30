using LMS.Api.Domain.Entities;

namespace LMS.Api.Infrastructure.Interfaces
{
    public interface IModuleRepository
    {
        public Module Add(Module module);
        public bool Delete(Module module);
        public bool Update(Module module);
        public Module Get(Guid id);
        public List<Module> GetAll(Guid courseId);
        public int GetOrder(Guid courseId);
        public bool UpdateBulk(IEnumerable<Module> modules);
    }
}
