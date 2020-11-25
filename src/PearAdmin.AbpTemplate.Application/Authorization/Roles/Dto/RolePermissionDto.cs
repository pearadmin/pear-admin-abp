namespace PearAdmin.AbpTemplate.Authorization.Roles.Dto
{
    public class RolePermissionDto
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public string Description { get; set; }

        private bool _isAssigned;
        public bool IsAssigned
        {
            get
            {
                return _isAssigned;
            }
            set
            {
                _isAssigned = value;
                if (_isAssigned)
                {
                    CheckArr = "1";
                }
            }
        }

        public string CheckArr { get; set; } = "0";
    }
}