using Password_Storage.Api.Models.DTOs;
using Password_Storage.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Password_Storage.Api.Context;

using Password_Storage.Api.Services.IServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Password_Storage.Api.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordDbContext _context;

        public PasswordService(PasswordDbContext context)
        {
            _context = context;
        }

        public async Task AddPasswordAsync(PasswordDto passwordDto)
        {
            var password = new PasswordModel
            {
                account_name = passwordDto.AccountName,
                username = passwordDto.Username,
                password_hash = EncryptPassword(passwordDto.Password),
                created_at = DateTime.UtcNow
            };

            await _context.passwordstorage.AddAsync(password);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PasswordDto>> GetPasswordsAsync()
        {
            try
            {
                var passwords = await _context.passwordstorage.ToListAsync();
                return passwords.Select(p => new PasswordDto
                {
                    AccountName = p.account_name,
                    Username = p.username,
                    Password = DecryptPassword(p.password_hash) // Decrypt the password here
                }).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception (or handle it as needed)
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                throw new InvalidOperationException("Şifreleri alırken bir hata oluştu.", ex);
            }
        }

        public async Task<PasswordDto> GetPasswordByIdAsync(int id)
        {
            try
            {
                var password = await _context.passwordstorage.FindAsync(id);
                if (password != null)
                {
                    return new PasswordDto
                    {
                        AccountName = password.account_name,
                        Username = password.username,
                        Password = DecryptPassword(password.password_hash) // Decrypt the password here
                    };
                }

                throw new KeyNotFoundException("Şifre bulunamadı."); // Veya uygun bir şekilde ele alın
            }
            catch (Exception ex)
            {
                // Log the exception (or handle it as needed)
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                throw new InvalidOperationException("Şifreyi alırken bir hata oluştu.", ex);
            }
        }



        public async Task DeletePasswordAsync(int id)
        {
            var password = await _context.passwordstorage.FindAsync(id);
            if (password != null)
            {
                _context.passwordstorage.Remove(password);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePasswordAsync(int id, PasswordDto passwordDto)
        {
            var existingPassword = await _context.passwordstorage.FindAsync(id);
            if (existingPassword != null)
            {
                existingPassword.account_name = passwordDto.AccountName;
                existingPassword.username = passwordDto.Username;
                existingPassword.password_hash = EncryptPassword(passwordDto.Password);

                _context.passwordstorage.Update(existingPassword);
                await _context.SaveChangesAsync();
            }
        }

        public string EncryptPassword(string plainTextPassword)
        {
            using (Aes aes = Aes.Create())
            {
                string key = ReadEncryptionKey();
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                
                if (keyBytes.Length != 16) 
                {
                    throw new CryptographicException("Anahtarın boyutu 16 byte olmalıdır (128 bit).");
                }

                aes.Key = keyBytes;
                aes.GenerateIV(); 

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        
                        ms.Write(aes.IV, 0, aes.IV.Length);

                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (var sw = new StreamWriter(cs))
                            {
                                sw.Write(plainTextPassword);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public string DecryptPassword(string encryptedPassword)
        {
            using (Aes aes = Aes.Create())
            {
                string key = ReadEncryptionKey();
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                
                if (keyBytes.Length != 16) // 128 bit = 16 byte
                {
                    throw new CryptographicException("Anahtarın boyutu 16 byte olmalıdır (128 bit).");
                }

                byte[] fullCipher = Convert.FromBase64String(encryptedPassword);

                // IV'yi ayır
                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);

                aes.Key = keyBytes;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (var sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }





        private string ReadEncryptionKey()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "passwordconfig.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Anahtar dosyası bulunamadı.", filePath);
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);

                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    if (doc.RootElement.TryGetProperty("EncryptionKey", out JsonElement encryptionKeyElement))
                    {
                        if (encryptionKeyElement.ValueKind == JsonValueKind.String)
                        {
                            string encryptionKey = encryptionKeyElement.GetString();

                            if (string.IsNullOrWhiteSpace(encryptionKey))
                            {
                                throw new InvalidOperationException("Anahtar dosyasındaki anahtar boş.");
                            }

                            
                            if (Encoding.UTF8.GetByteCount(encryptionKey) != 16)
                            {
                                throw new CryptographicException("Anahtarın boyutu 16 byte olmalıdır (128 bit).");
                            }

                            return encryptionKey;
                        }
                        else
                        {
                            throw new InvalidOperationException("Anahtar dosyasında EncryptionKey alanı geçerli bir string değil.");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Anahtar dosyasında EncryptionKey alanı bulunamadı.");
                    }
                }
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Anahtar dosyası okuma hatası.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Bilinmeyen bir hata oluştu.", ex);
            }
        }





    }

}
