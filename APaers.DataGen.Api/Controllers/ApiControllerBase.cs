using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace APaers.DataGen.Api.Controllers
{
    public class ApiControllerBase : ApiController
    {
        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();
            if (result.Succeeded) return null;
            if (result.Errors != null)
            {
                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);
            }
            if (ModelState.IsValid)
                return BadRequest();
            return BadRequest(ModelState);
        }
    }
}