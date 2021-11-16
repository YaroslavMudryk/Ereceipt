using Ereceipt.Application.Services.Interfaces;
using System.Text.Json;

namespace Ereceipt.Application.Services.Implementations
{
    public class InMemoryTokenManager : ITokenManager, IDisposable
    {
        private readonly List<string> _tokens;
        public InMemoryTokenManager()
        {
            var tokens = loadTokens();
            if (tokens == null)
                _tokens = new List<string>(5);
            else
                _tokens = new List<string>(tokens);
        }

        public bool AddNewToken(string token)
        {
            if (_tokens.Contains(token))
                return false;
            _tokens.Add(token);
            return true;
        }

        public bool AddNewTokens(IEnumerable<string> tokens)
        {
            int replicationCount = 0;
            foreach (var token in tokens)
            {
                if (_tokens.Contains(token))
                    replicationCount++;
            }
            if (replicationCount > 0)
                return false;
            _tokens.AddRange(tokens);
            return true;
        }

        public bool IsAvailableToken(string token)
        {
            return _tokens.Contains(token);
        }

        public void RemoveAllTokens()
        {
            _tokens.Clear();
        }

        public bool RemoveToken(string token)
        {
            if (_tokens.Contains(token))
            {
                _tokens.Remove(token);
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            uploadTokens();
        }

        private string[] loadTokens()
        {
            if (File.Exists(_pathToFile))
            {
                var content = File.ReadAllText(_pathToFile);
                var tokensFromFile = JsonSerializer.Deserialize<string[]>(content);
                if (tokensFromFile != null && tokensFromFile.Length > 0)
                    return tokensFromFile;
                return null;
            }
            else
                return null;
        }

        private void uploadTokens()
        {
            if(File.Exists(_pathToFile))
                File.Delete(_pathToFile);
            var json = JsonSerializer.Serialize(_tokens);
            File.WriteAllText(_pathToFile, json);
        }

        private const string _pathToFile = "tokens.json";
    }
}