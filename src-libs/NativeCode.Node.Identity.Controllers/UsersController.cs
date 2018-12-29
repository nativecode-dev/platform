namespace NativeCode.Node.Identity.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SeedModels;

    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        public UsersController(IdentityDataContext context, IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        protected IdentityDataContext Context { get; }

        protected IMapper Mapper { get; }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var users = await this.Context.Users.ToListAsync();

            return this.Ok(this.Mapper.Map<IEnumerable<UserInfo>>(users));
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await this.Context.Users.SingleOrDefaultAsync(u => u.Email == email);

            return this.Ok(this.Mapper.Map<UserInfo>(user));
        }
    }
}
