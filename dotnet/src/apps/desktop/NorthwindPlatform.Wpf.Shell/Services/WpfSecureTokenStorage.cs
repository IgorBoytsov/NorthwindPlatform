using Shared.Client.Security.Abstractions;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace NorthwindPlatform.Wpf.Shell.Services
{
    internal sealed class WpfSecureTokenStorage : ISecureTokenStorage
    {
        internal sealed record TokenData(string AccessToken, string RefreshToken);

        private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NorthwindPlatform", "token.data");
        private static readonly byte[] s_entropy = Encoding.Unicode.GetBytes("JE2D6mGrmySirkDoky91pFcMKqvt22d8");

        public Task ClearTokensAsync()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            return Task.CompletedTask;
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            var tokenData = await ReadAndDecryptTokensAsync();
            return tokenData?.AccessToken;
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            var tokenData = await ReadAndDecryptTokensAsync();
            return tokenData?.RefreshToken;
        }


        public async Task StoreTokensAsync(string accessToken, string refreshToken)
        {
            var tokenData = new TokenData(accessToken, refreshToken);

            string jsonString = JsonSerializer.Serialize(tokenData);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(jsonString);
            byte[] encryptedBytes = ProtectedData.Protect(tokenBytes, s_entropy, DataProtectionScope.CurrentUser);

            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

            await File.WriteAllBytesAsync(_filePath, encryptedBytes);
        }

        private async Task<TokenData?> ReadAndDecryptTokensAsync()
        {
            if (!File.Exists(_filePath))
                return null;

            try
            {
                byte[] encryptedBytes = await File.ReadAllBytesAsync(_filePath);

                byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, s_entropy, DataProtectionScope.CurrentUser);

                string jsonString = Encoding.UTF8.GetString(decryptedBytes);

                var tokenData = JsonSerializer.Deserialize<TokenData>(jsonString);

                return tokenData;
            }
            catch (Exception) 
            {
                return null;
            }
        }
    }
}