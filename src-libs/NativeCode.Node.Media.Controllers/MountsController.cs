namespace NativeCode.Node.Media.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using NativeCode.Core.Data.Exceptions;
    using NativeCode.Node.Media.Data.Services.Storage;
    using NativeCode.Node.Media.Models.Data.Storage;
    using NativeCode.Node.Media.Models.Views.Mounts;

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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteMountRequest), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(DeleteMountRequest request)
        {
            try
            {
                await this.MountService.DeleteMount(request.Id)
                    .ConfigureAwait(true);

                return this.Ok();
            }
            catch (EntityException)
            {
                return this.NotFound(request);
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(IEnumerable<MountInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var mounts = await this.MountService.GetMounts()
                                 .ConfigureAwait(false);

                return this.Ok(this.Mapper.Map<IEnumerable<MountInfo>>(mounts));
            }
            catch (EntityException)
            {
                return this.NotFound();
            }
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(MountInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var mount = await this.MountService.GetMount(id)
                                .ConfigureAwait(false);

                return this.Ok(this.Mapper.Map<MountInfo>(mount));
            }
            catch (EntityException)
            {
                return this.NotFound();
            }
        }

        [HttpGet("local")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(IEnumerable<MountInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLocal()
        {
            try
            {
                var mounts = await this.MountService.GetLocalMounts()
                                 .ConfigureAwait(false);

                return this.Ok(this.Mapper.Map<IEnumerable<MountInfo>>(mounts));
            }
            catch (EntityException)
            {
                return this.NotFound();
            }
        }

        [HttpGet("nfs")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(IEnumerable<MountInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNfs()
        {
            try
            {
                var mounts = await this.MountService.GetNfsMounts()
                                 .ConfigureAwait(false);

                return this.Ok(this.Mapper.Map<IEnumerable<MountInfo>>(mounts));
            }
            catch (EntityException)
            {
                return this.NotFound();
            }
        }

        [HttpGet("smb")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(IEnumerable<MountInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSmb()
        {
            try
            {
                var mounts = await this.MountService.GetSmbMounts()
                                 .ConfigureAwait(false);

                return this.Ok(this.Mapper.Map<IEnumerable<MountInfo>>(mounts));
            }
            catch (EntityException)
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(MountInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateMountRequest), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreateMountRequest request)
        {
            try
            {
                var mount = await this.MountService.CreateMount(request.Name, request.Type)
                                .ConfigureAwait(true);

                return this.Ok(this.Mapper.Map<MountInfo>(mount));
            }
            catch (EntityException)
            {
                return this.NotFound(request);
            }
        }
    }
}
