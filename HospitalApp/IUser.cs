using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public enum UserRole
    { 
        Client = 0,
        Doctor = 1,
        Reseption = 2,
    }

    public interface IUser
    {
        [Key]
        public int id { get; set; }
        public UserRole role { get; set; }
        public string name { get; set; }


    }


}
