using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ombi.Core.Models.UI;
using Ombi.Core.Processor;
using Ombi.Helpers;
using Ombi.Schedule.Jobs.Ombi;

namespace Ombi.Controllers
{
    [ApiV1]
    [Produces("application/json")]
    [AllowAnonymous]
    public class UpdateController : Controller
    {
        public UpdateController(ICacheService cache, IChangeLogProcessor processor, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
            _processor = processor;
        }

        private readonly ICacheService _cache;
        private readonly IChangeLogProcessor _processor;
        private readonly IMapper _mapper;

        [HttpGet("{branch}")]
        public async Task<UpdateModel> UpdateAvailable(string branch)
        {
            return await _cache.GetOrAdd(branch, async () => await _processor.Process(branch));
        }

        [HttpGet]
        public async Task<UpdateViewModel> UpdateAvailable()
        {
            var versionInfo = OmbiAutomaticUpdater.GetVersion();
            var model = await _cache.GetOrAdd(versionInfo[1], async () => await _processor.Process(versionInfo[1]));
            var mapped = _mapper.Map<UpdateViewModel>(model);

            var currentVersionNumber = versionInfo[0];
            mapped.InstalledVersion = currentVersionNumber;

            return mapped;
        }
    }
}