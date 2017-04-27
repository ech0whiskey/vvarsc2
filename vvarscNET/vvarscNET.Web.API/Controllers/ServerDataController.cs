using vvarscNET.Core.Service.Interfaces;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System;
using vvarscNET.Web.API.Models;
using System.Collections.Generic;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller for Handling Authentication of Members using AdminConsole System
    /// </summary>
    public class ServerDataController : ApiController
    {

        /// <summary>
        /// Constructor for Authentication Controller
        /// </summary>
        public ServerDataController()
        {
        }

        /// <summary>
        /// Method to Get ServerData (for testing)
        /// </summary>
        /// <returns>Member</returns>
        [HttpGet]
        [Route("api/serverdata")]
        [ResponseType(typeof(List<ServerDataModel>))]
        public IHttpActionResult Get()
        {

            var returnData = new List<ServerDataModel>
            {
                new ServerDataModel
                {
                    Id = 1,
                    InitialDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddHours(12),
                    OrderNumber = 1,
                    IsDirty = false,
                    IP = "192.168.110.103",
                    Type = 1,
                    RecordIdentifier = 1
                }
            };

            return Ok(returnData);
        }


    }
}