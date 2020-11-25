namespace PearAdmin.AbpTemplate.MultiTenancy.Editions.Dto
{
    public class EditionFeatureDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public string ParentName { get; set; }

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