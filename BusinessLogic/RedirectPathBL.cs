using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using short_url.Models;

namespace short_url.BusinessLogic
{
    public class RedirectPathBL
    {
        private readonly RedirectPathContext _context;

        public RedirectPathBL(RedirectPathContext context)
        {
            _context = context;
        }

        public List<RedirectPath> GetRedirectPaths()
        {
            return _context.RedirectPaths.ToList<RedirectPath>();
        }
        public RedirectPath GetRedirectPaths(string path)
        {
            return _context.RedirectPaths.FirstOrDefault<RedirectPath>(rp => rp.path.Equals(path));
        }
        public RedirectPath GetRedirectPaths(long id)
        {
            return _context.RedirectPaths.FirstOrDefault<RedirectPath>(rp => rp.Id == id);
        }

        public async bool ModifyRedirectPath(long id,RedirectPath redirectPath)
        {
            if (id != redirectPath.Id)
            {
                return false;
            }

            _context.Entry(redirectPath).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RedirectPathExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

         private bool RedirectPathExists(long id)
        {
            return _context.RedirectPaths.Any(e => e.Id == id);
        }
    }
}