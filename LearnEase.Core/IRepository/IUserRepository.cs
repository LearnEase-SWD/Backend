﻿using LearnEase_Api.Entity;

namespace LearnEase_Api.LearnEase.Infrastructure.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> FindById(string id);
        Task<User> FindByName(string name);
        Task<User> FindByEmail(string email);
        Task<User> createNewUser(User user);
        Task<User> updateUser(User user, string id);
        Task<User> deleteUser(string id);
    }
}
