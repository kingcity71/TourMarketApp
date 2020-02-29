using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourMarket.DAL;
using TourMarket.Entities;
using TourMarket.Interfaces;

namespace TourMarket.BLL
{
    public class UserService : IUserService
    {
        private readonly Context context;

        public UserService(Context context)
        {
            this.context = context;
        }
        public User GetUser(string email)
        {
            if (email == null) return null;
            return context.Users.Where(x => x.Email.ToLower() == email.ToLower()).Include(x => x.Role).FirstOrDefault();
        }

        public string LogIn(User user)
        {
            var userDB = GetUser(user?.Email);
            if (userDB == null) return "Не найден";
            if (userDB.Password != user.Password) return "Неверный пароль";
            return null;
        }

        public string Register(User user)
        {
            var validation = Validation(user);
            if (validation != null) return validation;

            user.Id = Guid.NewGuid();
            user.Role = context.Roles.FirstOrDefault(x => x.Description.ToLower() == "user");
            context.Users.Add(user);
            context.SaveChanges();
            return null;
        }
        private string Validation(User user)
        {
            if (user == null) return "Введите данные";
            if (string.IsNullOrEmpty(user.Name)) return "Введите имя";
            if (string.IsNullOrEmpty(user.Surname)) return "Введите фамилию";
            if (string.IsNullOrEmpty(user.Email)) return "Введите e-mail";
            if (string.IsNullOrEmpty(user.Password)) return "Введите пароль";

            var userDB = GetUser(user?.Email);
            if (userDB != null) return "Пользователь с таким e-mail уже есть";
            return null;
        }
    }
}
