using LYC_API.Model;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LYC_API.Service
{
    public class ChallengeService
    {
        private readonly string _filePath;
        private static ChallengeService _instance;
        private static readonly object _lock = new object();
        private List<Challenge> _challenges;

        private ChallengeService(string filePath)
        {
            _filePath = filePath;
            _challenges = LoadChallengesFromFile();
        }

        public static ChallengeService GetInstance(string filePath)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ChallengeService(filePath);
                    }
                }
            }
            return _instance;
        }

        private List<Challenge> LoadChallengesFromFile()
        {
            if (File.Exists(_filePath))
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Challenge>>(jsonData) ?? new List<Challenge>();
            }
            return new List<Challenge>();
        }

        private void SaveChallengesToFile()
        {
            var jsonData = JsonSerializer.Serialize(_challenges);
            File.WriteAllText(_filePath, jsonData);
        }

        public List<Challenge> GetAllChallenges()
        {
            return _challenges;
        }

        public Challenge GetChallengeById(int id)
        {
            return _challenges.FirstOrDefault(u => u.Id == id);
        }

        public void InsertChallenge(Challenge newChallenge)
        {
            newChallenge.Id = _challenges.Any() ? _challenges.Max(u => u.Id) + 1 : 1;
            _challenges.Add(newChallenge);
            SaveChallengesToFile();
        }

        public void UpdateChallenge(Challenge updatedChallenge)
        {
            var existingChallenge = GetChallengeById(updatedChallenge.Id);
            if (existingChallenge != null)
            {
                existingChallenge.Title = updatedChallenge.Title;
                existingChallenge.UserId = updatedChallenge.UserId;
                existingChallenge.Description = updatedChallenge.Description;
                existingChallenge.DailyTargetInMins = updatedChallenge.DailyTargetInMins;
                existingChallenge.StartDate = updatedChallenge.StartDate;
                existingChallenge.EndDate = updatedChallenge.EndDate;
                existingChallenge.Color = updatedChallenge.Color;
                existingChallenge.CreatedOn = updatedChallenge.CreatedOn;
                SaveChallengesToFile();
            }
        }

        public void DeleteChallenge(int id)
        {
            var challenge = GetChallengeById(id);
            if (challenge != null)
            {
                _challenges.Remove(challenge);
                SaveChallengesToFile();
            }
        }
    }
}
