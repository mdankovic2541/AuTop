using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Model.Common;

namespace AuTOP.Model
{
    public class User : IdDateBaseModel, IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
