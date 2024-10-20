using LYC_API.Model;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LYC_API.Service
{
    public class UserService
    {
        private readonly string _filePath;
        private static UserService _instance;
        private static readonly object _lock = new object();
        private List<User> _users;

        private UserService(string filePath)
        {
            _filePath = filePath;
            _users = LoadUsersFromFile();
        }

        public static UserService GetInstance(string filePath)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserService(filePath);
                    }
                }
            }
            return _instance;
        }

        private List<User> LoadUsersFromFile()
        {
            if (File.Exists(_filePath))
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<User>>(jsonData) ?? new List<User>();
            }
            return new List<User>();
        }

        private void SaveUsersToFile()
        {
            var jsonData = JsonSerializer.Serialize(_users);
            File.WriteAllText(_filePath, jsonData);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void InsertUser(User newUser)
        {
            newUser.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(newUser);
            SaveUsersToFile();
        }

        public void UpdateUser(User updatedUser)
        {
            var existingUser = GetUserById(updatedUser.Id);
            if (existingUser != null)
            {
                existingUser.UserName = updatedUser.UserName;
                existingUser.NickName = updatedUser.NickName;
                existingUser.CreatedOn = updatedUser.CreatedOn;
                SaveUsersToFile();
            }
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _users.Remove(user);
                SaveUsersToFile();
            }
        }
    }
}
