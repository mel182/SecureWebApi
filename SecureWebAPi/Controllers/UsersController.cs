﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureWebAPi.Database;
using SecureWebAPi.Database.Handler.DbStorageManager;
using SecureWebAPi.Database.Model;
using SecureWebAPi.Database.Services;

namespace SecureWebAPi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseRepository _context;

        public UsersController(DatabaseRepository context)
        {
            _context = context;
        }

        // GET: api/Users
        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthenticatedUser>>> GetAuthenticatedUsers()
        {
            return await _context.AuthenticatedUsers.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthenticatedUser>> GetAuthenticatedUser(long id)
        {
            var authenticatedUser = await _context.AuthenticatedUsers.FindAsync(id);

            if (authenticatedUser == null)
            {
                return NotFound();
            }

            return authenticatedUser;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthenticatedUser(long id, AuthenticatedUser authenticatedUser)
        {
            if (id != authenticatedUser.ID)
            {
                return BadRequest();
            }

            _context.Entry(authenticatedUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthenticatedUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<AuthenticatedUser>> PostAuthenticatedUser(AuthenticatedUser authenticatedUser)
        {
            var newUser = DatabaseService.Get.AddUserAsync(authenticatedUser).Result;

            if (newUser != null)
            {
                return Ok(new
                {
                    id = newUser.ID,
                    firstName = newUser.FirstName,
                    lastName = newUser.LastName,
                    email = newUser.Email,
                    userName = newUser.UserName,
                    role = newUser.Role,
                    creationDate = newUser.CreationDate,
                    updated = newUser.Updated
                });
            }
            
            Response.StatusCode = 500;
            return Content("Unable to store data in database");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthenticatedUser>> DeleteAuthenticatedUser(long id)
        {
            var authenticatedUser = await _context.AuthenticatedUsers.FindAsync(id);
            if (authenticatedUser == null)
            {
                return NotFound();
            }

            _context.AuthenticatedUsers.Remove(authenticatedUser);
            await _context.SaveChangesAsync();

            return authenticatedUser;
        }

        private bool AuthenticatedUserExists(long id)
        {
            return _context.AuthenticatedUsers.Any(e => e.ID == id);
        }
    }
}
