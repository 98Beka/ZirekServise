namespace ZirekTeamAdmin.Models {
    public class BaseAudit {
        public long Id { get; set; }
        public string EntityGuid { get; set; }
        public long EntityId { get; set; }
        public string EntityName { get; set; }
        public string JsonData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte ActionType { get; set; }

    }
}
