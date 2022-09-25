using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultConnection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyVaultController : ControllerBase
    {
        private SecretClient GetSecretClient()
        {
            var keyVaultUri = "https://projectname1kvne.vault.azure.net/";

            return new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecretApiKey(string userId)
        {
            var secretClient = GetSecretClient();
            var secretName = $"api-key-{userId}";
            var secret = Guid.NewGuid().ToString();

            await secretClient.SetSecretAsync(secretName, secret);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSecretApiKey(string userId)
        {
            var secretClient = GetSecretClient();
            var secretName = $"api-key-{userId}";

            var secretResponse = await secretClient.GetSecretAsync(secretName);
            return Ok(secretResponse.Value.Value);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSecretApiKey(string userId)
        {
            var secretClient = GetSecretClient();
            var secretName = $"api-key-{userId}";

            var deleteOperation = await secretClient.StartDeleteSecretAsync(secretName);

            await deleteOperation.WaitForCompletionAsync();

            return NoContent();
        }
    }
}