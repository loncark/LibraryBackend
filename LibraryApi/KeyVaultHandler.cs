using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace LibraryApi
{
    public class KeyVaultHandler
    {
        public string GetSecret(string secretName)
        {
            var client = new SecretClient(vaultUri: new Uri("https://algebravault.vault.azure.net/"), credential: new VisualStudioCredential());
            var secret = client.GetSecret(secretName);

            return secret.Value.Value;
        }
    }
}
