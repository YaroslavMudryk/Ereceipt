﻿using Microsoft.AspNetCore.Mvc;

namespace Ereceipt.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {

    }
}