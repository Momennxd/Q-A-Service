using API_Layer.Security;
using Core_Layer.DTOs.People;
using DataAccess_Layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Layer.Controllers.People
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        [HttpPost("Signin")]
        public async Task<ActionResult> Signin(UsersDTOs.AddUserDTO addUserDTO)
        {
            if (addUserDTO == null)
                return BadRequest();

            try
            {
                using (var context = await clsService.contextFactory!.CreateDbContextAsync())
                {
                    var Person = PeopleDTOs.ConvertFromDTOtoEntity(addUserDTO.Person);
                    var User = UsersDTOs.ConvertFromDTOtoEntity(addUserDTO);
                    if (Person == null || User == null)
                        return BadRequest();
                    
                    User.Person = Person;
                    User.PersonID = Person.PersonID;
                    await context.Users.AddAsync(User);

                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UsersDTOs.LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest();

            try
            {
                using (var context = await clsService.contextFactory!.CreateDbContextAsync())
                {
                    var User = await context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(user => user.Username == loginDTO.Username && user.Password == loginDTO.Password);
                    if (User == null) return BadRequest();
                    clsToken.CreateToken(User.UserID);
                    return Ok(clsToken.CreateToken(User.UserID));
                }
            }
            catch (Exception ex) { return BadRequest(ex); }
        }
    }
}
