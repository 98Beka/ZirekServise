using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZirekService.Data;
using ZirekService.Models;

namespace ZirekService.Services {
    public class AuditService {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuditService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public enum ActionType {
            Create = 0,
            Update = 1,
            Delete = 2
        }
        public void AuditCreate(BaseEntity entity, ActionType actionType, string? entityName = null) {
            string createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var jsonString = JsonConvert.SerializeObject(entity);

            long Id = entity.Id;
            if (string.IsNullOrEmpty(entityName))
                entityName = entity.GetType().Name;

            BaseAudit baseAudit = new BaseAudit {
                EntityId = Id,
                EntityGuid = "",
                EntityName = entityName,
                JsonData = jsonString,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                ActionType = (byte)actionType
            };
            _context.BaseAudits.Add(baseAudit);
            _context.SaveChanges();
        }


    }
}
