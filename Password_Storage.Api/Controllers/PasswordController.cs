using Microsoft.AspNetCore.Mvc;
using Password_Storage.Api.Models;
using Password_Storage.Api.Models.DTOs;
using Password_Storage.Api.Services;
using Password_Storage.Api.Services.IServices;

namespace Password_Storage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasswordDto>>> GetPasswords()
        {
            var passwords = await _passwordService.GetPasswordsAsync();
            return Ok(passwords);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<PasswordDto>> GetPasswordById(int id)
        {
            var password = await _passwordService.GetPasswordByIdAsync(id);
            if (password == null)
            {
                return NotFound();
            }
            return Ok(password);
        }

        
        [HttpPost]
        public async Task<ActionResult> AddPassword(PasswordDto passwordDto)
        {
            await _passwordService.AddPasswordAsync(passwordDto);
            return Ok();
        }

        // PUT: api/password
        [HttpPut]
        public async Task<ActionResult> UpdatePassword(int id,PasswordDto passwordDto)
        {
            try
            {
                await _passwordService.UpdatePasswordAsync(id,passwordDto);
                return NoContent(); // Return 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePassword(int id)
        {
            try
            {
                await _passwordService.DeletePasswordAsync(id);
                return NoContent(); // Return 204 No Content on successful delete
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete/{accountName}")]
        public async Task<IActionResult> DeletePasswordByAccName(string accountName)
        {
            try
            {
                // Servis üzerinden şifreyi account name'e göre siliyoruz
                await _passwordService.DeletePasswordByAccNameAsync(accountName);
                return Ok("Password entry deleted successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



        [HttpPost("encrypt")]
        public ActionResult<string> EncryptPasswords([FromBody] string plainTextPassword)
        {
            var encryptedPassword = _passwordService.EncryptPassword(plainTextPassword);
            return Ok(encryptedPassword);
        }

        [HttpPost("decrypt")]
        public ActionResult<string> DecryptPasswords([FromBody] string encryptedPassword)
        {
            var decryptedPassword = _passwordService.DecryptPassword(encryptedPassword);
            return Ok(decryptedPassword);
        }
    }
}
