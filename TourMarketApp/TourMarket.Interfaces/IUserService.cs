using System;
using System.Collections.Generic;
using System.Text;
using TourMarket.Entities;

namespace TourMarket.Interfaces
{
    public interface IUserService
    {
        string LogIn(User user);
        string Register(User user);
        User GetUser(string email);
    }
}
