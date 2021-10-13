using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using api.Core.Utilities;
using api.Core.Model;

namespace api.Core
{
    public abstract class BaseController<H> : ControllerBase where H : BaseModel
    {
        protected IMapper _mapper;

        protected readonly ILoggerManager _logger;

        public BaseController(IMapper mapper, ILoggerManager logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public abstract bool Validate(H model);
    }
}