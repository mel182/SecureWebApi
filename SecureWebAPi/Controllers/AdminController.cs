﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureWebAPi.Api.Response.Model;
using SecureWebAPi.Controllers.SecureBaseClass;
using SecureWebAPi.Database.Handler;
using SecureWebAPi.Database.Model;

namespace SecureWebAPi.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : BaseSecureController
    {
        //[Route("all")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<RegisteredUser>>> GetAdmins()
        //{
        //    //if (IsAdmin())
        //    //{
        //    //    return await _context.Post.ToListAsync();
        //    //}

        //    return await AdminService.GetAll();
            
        //    //Response.StatusCode = 401;
        //    //return Content("You are not authorized".ToErrorMessage());
        //}

        //[Route("{username}")]
        //[HttpGet]
        //public async Task<ActionResult<>> GetAdmin(string userName)
        //{

        //}

        //[Route("update")]
        //[HttpPut]
        //public async Task<ActionResult<RegisteredUser>> UpdateAdmin(string userName)
        //{

        //}

        //[Route("add")]
        //[HttpPost]
        //public async Task<ActionResult<RegisteredUser>> UpdateAdmin(string userName)
        //{

        //}

        //[Route("delete/{id}")]
        //[HttpPost]
        //public async Task<ActionResult<RegisteredUser>> DeleteAdmin(long id)
        //{

        //}
    }
}