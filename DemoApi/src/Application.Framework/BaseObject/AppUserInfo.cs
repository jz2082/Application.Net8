using System.Collections.Generic;

namespace Application.Framework
{
    public record AppUserInfo : BaseRecord
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPrincipalName { get; set; }
        public string UserType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public List<AppKeyValue> ClaimList { get; set; }
        public List<string> GroupList { get; set; }
        public List<string> RoleList { get; set; }
        public bool UserEnabled { get; set; }

        public AppUserInfo()
        {
            ClaimList = new List<AppKeyValue>();
            GroupList = new List<string>();
            RoleList = new List<string>();
        }
    }
}
