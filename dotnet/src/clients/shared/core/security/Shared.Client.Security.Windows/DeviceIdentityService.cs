using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Shared.Client.Security.Abstractions;

namespace Shared.Client.Security.Windows
{
    public class DeviceIdentityService : IDeviceIdentityService
    {
        private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NorthwindPlatform", "device.dat");

        public async Task<DeviceIdentity> GetOrCreateAsync()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    var encryptedData = await File.ReadAllBytesAsync(_filePath);
                    var decrypted = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
                    var json = Encoding.UTF8.GetString(decrypted);
                    return JsonSerializer.Deserialize<DeviceIdentity>(json) ??
                        CreateAndSaveNew();
                }
                catch
                {
                    return CreateAndSaveNew();
                }
            }

            return CreateAndSaveNew();
        }

        private DeviceIdentity CreateAndSaveNew()
        {
            var fingerprint = GenerateRawFingerprint();
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(fingerprint));

            var identity = new DeviceIdentity
            {
                DeviceId = Guid.NewGuid().ToString(),
                FingerprintHash = hash
            };

            Save(identity);

            return identity;
        }

        private void Save(DeviceIdentity identity)
        {
            var json = JsonSerializer.Serialize(identity);
            var bytes = Encoding.UTF8.GetBytes(json);
            var encrypted = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(_filePath, encrypted);
        }

        private static string GenerateRawFingerprint()
        {
            var machineGuid = GetMachineGuid(); 
            var osVersion = Environment.OSVersion.ToString();
            var processorCount = Environment.ProcessorCount.ToString();
            var totalMemory = GetTotalMemoryInGb().ToString();

            return $"{machineGuid}-{osVersion}-{processorCount}-{totalMemory}";
        }

        private static string GetMachineGuid()
        {
            try
            {
                using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");

                return key?.GetValue("MachineGuid")?.ToString() ?? Guid.NewGuid().ToString();
            }
            catch
            {
                return Guid.NewGuid().ToString();
            }
        }

        private static long GetTotalMemoryInGb()
        {
            try
            {
                using var mc = new System.Management.ManagementClass("Win32_ComputerSystem");
                using var moc = mc.GetInstances();

                foreach (var mo in moc)
                {
                    var totalMemory = Convert.ToUInt64(mo["TotalPhysicalMemory"]);
                    return (long)(totalMemory / (1024L * 1024L * 1024L));
                }
            }
            catch
            {
                
            }

            return 0;
        }
    }
}