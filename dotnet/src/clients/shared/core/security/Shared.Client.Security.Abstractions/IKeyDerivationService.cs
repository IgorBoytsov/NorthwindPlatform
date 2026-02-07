namespace Shared.Client.Security.Abstractions
{
    public interface IKeyDerivationService
    {
        (byte[] Kek, string AuthHash) DeriveKeysFromPassword(string password, byte[] salt);
    }
}