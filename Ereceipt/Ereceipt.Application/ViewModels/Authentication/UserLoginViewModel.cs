﻿using Ereceipt.Domain.Models;

namespace Ereceipt.Application.ViewModels.Authentication
{
    public class UserLoginViewModel
    {
        public User User { get; set; }
        public UserLogin LoginData { get; set; }
        public SessionViewModel Session { get; set; }
        public Role Role { get; set; }
        public AuthViewModel Token { get; set; }
    }
}
