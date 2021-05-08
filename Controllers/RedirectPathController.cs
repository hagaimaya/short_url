using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using short_url.BusinessLogic;
using short_url.Models;
using System.Net;
using short_url.Consts.Messages;

namespace short_url.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedirectPathController : ControllerBase
    {
        private readonly RedirectPathBL _businesslogic;

        public RedirectPathController(RedirectPathContext context)
        {
            _businesslogic = new RedirectPathBL(context);
        }

        // GET: api/RedirectPath
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RedirectPath>>> GetRedirectPaths()
        {
            return _businesslogic.GetRedirectPaths();
        }

        // GET: api/RedirectPath/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RedirectPath>> GetRedirectPath(long id)
        {
            RedirectPath redirectPath = _businesslogic.GetRedirectPaths(id);

            if (redirectPath == null)
            {
                return NotFound(Messages.redirectNotFound);
            }

            return redirectPath;
        }

        // PUT: api/RedirectPath/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRedirectPath(long id, RedirectPath redirectPath)
        {
            try
            {
                bool modifySuccess = await _businesslogic.ModifyRedirectPath(id, redirectPath);
                if (!modifySuccess)
                {
                    return NotFound(Messages.redirectNotFound);
                }
                else
                {
                    return NoContent();
                }
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST: api/RedirectPath
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RedirectPath>> PostRedirectPath(RedirectPath redirectPath)
        {
            redirectPath = await _businesslogic.AddRedirectPath(redirectPath);

            return CreatedAtAction(nameof(GetRedirectPath), new { id = redirectPath.Id }, redirectPath);
        }

        // DELETE: api/RedirectPath/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRedirectPath(long id)
        {
            bool deletedSuccessfully = await _businesslogic.DeleteRedirectPath(id);
            if (!deletedSuccessfully)
            {
                return NotFound(Messages.redirectNotFound);
            }

            return NoContent();
        }
    }
}
