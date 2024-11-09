using Password_Storage.Api.Models;
using Password_Storage.Api.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Password_Storage.Api.Services.IServices
{
    public interface IPasswordService
    {
        Task<List<PasswordDto>> GetPasswordsAsync();
        Task<PasswordDto> GetPasswordByIdAsync(int id);
        Task AddPasswordAsync(PasswordDto password);
        Task UpdatePasswordAsync(int id,PasswordDto password);
        Task DeletePasswordAsync(int id);

        Task DeletePasswordByAccNameAsync(string accountname);

        public string DecryptPassword(string encryptedPassword);

        public string EncryptPassword(string plainTextPassword);
    }
}
