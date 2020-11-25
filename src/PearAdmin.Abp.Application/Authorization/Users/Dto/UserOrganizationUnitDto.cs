namespace PearAdmin.Abp.Authorization.Users.Dto
{
    /// <summary>
    /// 用户组织机构Dto
    /// </summary>
    public class UserOrganizationUnitDto
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public string OrganizationUnitCode { get; set; }

        public string DisplayName { get; set; }

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
