using api.Core.Model;
using api.Core.Utilities;
using api.Models;
using AutoMapper;

namespace api.Controllers
{
    public abstract class BaseController<H> : api.Core.BaseController<H> where H : BaseModel
    {

        /// The currently logged in user taken from the session.
        /// Make it accessible to all subclasses of BaseController
        public User LoggedInUser => (User)HttpContext.Items["User"];
        public BaseController(IMapper mapper, ILoggerManager logger) : base (mapper, logger)
        {
            
        }
    }
}