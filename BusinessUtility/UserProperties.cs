using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class UserProperties
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Mobile { get; set; }
        public bool? IsActive { get; set; }
        public string UserType { get; set; }
    }
}
