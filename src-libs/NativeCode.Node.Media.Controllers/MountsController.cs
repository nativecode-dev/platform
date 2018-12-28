namespace NativeCode.Node.Media.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Services.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [ApiController]
    [Route("[controller]")]
    public class MountsController : ControllerBase
    {
        public MountsController(IMapper mapper, IMountService mountService)
        {
            this.Mapper = mapper;
            this.MountService = mountService;
        }

        protected IMapper Mapper { get; }

        protected IMountService MountService { get; }

        [HttpGet]
        public async Task<IEnumerable<MountInfo>> Get()
        {
            var mounts = await this.MountService.GetMounts()
                .ConfigureAwait(false);
            return this.Mapper.Map<IEnumerable<MountInfo>>(mounts);
        }

        [HttpGet("local")]
        public async Task<IEnumerable<MountInfo>> GetLocal()
        {
            var mounts = await this.MountService.GetLocalMounts()
                .ConfigureAwait(false);
            return this.Mapper.Map<IEnumerable<MountInfo>>(mounts);
        }

        [HttpGet("nfs")]
        public async Task<IEnumerable<MountInfo>> GetNfs()
        {
            var mounts = await this.MountService.GetNfsMounts()
                .ConfigureAwait(false);
            return this.Mapper.Map<IEnumerable<MountInfo>>(mounts);
        }

        [HttpGet("smb")]
        public async Task<IEnumerable<MountInfo>> GetSmb()
        {
            var mounts = await this.MountService.GetSmbMounts()
                .ConfigureAwait(false);
            return this.Mapper.Map<IEnumerable<MountInfo>>(mounts);
        }

        [HttpGet("{id}")]
        public async Task<MountInfo> Get(Guid id)
        {
            var mount = await this.MountService.GetMount(id)
                .ConfigureAwait(false);
            return this.Mapper.Map<MountInfo>(mount);
        }
    }
}
