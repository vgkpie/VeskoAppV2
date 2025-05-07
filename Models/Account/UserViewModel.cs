using System.Collections.Generic;

namespace VeskoAppV2.Models.Account
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
