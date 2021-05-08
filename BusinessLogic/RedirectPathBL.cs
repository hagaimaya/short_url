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

        public async Task<bool> ModifyRedirectPath(long id, RedirectPath redirectPath)
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

        public async Task<RedirectPath> AddRedirectPath(RedirectPath redirectPath)
        {
            if (GetRedirectPaths(redirectPath.path) != null || GetRedirectPaths(redirectPath.Id) != null)
            {
                return null;
            }
            else
            {
                _context.RedirectPaths.Add(redirectPath);
                await _context.SaveChangesAsync();
            }

            return redirectPath;
        }

        public async Task<bool> DeleteRedirectPath(long id)
        {
            var redirectPath = GetRedirectPaths(id);
            if (redirectPath == null)
            {
                return false;
            }
            _context.RedirectPaths.Remove(redirectPath);
            await _context.SaveChangesAsync();
            return true;
        }
        private bool RedirectPathExists(long id)
        {
            return _context.RedirectPaths.Any(e => e.Id == id);
        }
    }
}