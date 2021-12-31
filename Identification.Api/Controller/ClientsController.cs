using Identification.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identification.Models;
using Microsoft.AspNetCore.Authorization;

namespace Identification.Api.Controller
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        public ClientsController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await clientRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreving data from the database");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetFromDb(int id)
        {
            try
            {
                var result = await clientRepository.GetFromDb(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Client>> AddClient(Client client)
        {
            try
            {
                if (client == null)
                    return BadRequest();

                var createdclient = await clientRepository.AddClient(client);

                return CreatedAtAction(nameof(GetFromDb), new { id = createdclient.Id }, createdclient);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new client record");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Client>> UpdateClient([FromBody] Client client)
        {
            try
            {
                //if (id != client.Id)
                //    return BadRequest("Client ID mismatch");

               // var employeeToUpdate = await clientRepository.GetFromDb(id);

                //if (employeeToUpdate == null)
                //    return NotFound($"Client with Id = {id} not found");

                return await clientRepository.UpdateClient(client);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            try
            {
                var employeeToDelete = await clientRepository.GetFromDb(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Client with Id = {id} not found");
                }

                return await clientRepository.DeleteClient(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
