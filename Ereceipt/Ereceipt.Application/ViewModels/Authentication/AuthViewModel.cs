using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ereceipt.Application.ViewModels.Authentication
{
    public class AuthViewModel
    {
        public TokenViewModel Token { get; set; }
        public UserAuthViewModel User { get; set; }
    }
}
