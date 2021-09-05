using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandController(ICommanderRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commands = _repository.GetAllCommands();
            if (commands != null)
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
            return NoContent();
        }

        //GET api/command/{id}
        [Authorize]
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var result = _repository.GetCommandById(id);
            if (result != null) return Ok(_mapper.Map<CommandReadDto>(result));
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmd)
        {
            var commandModel = _mapper.Map<Command>(cmd);
            if (commandModel != null)
            {
                _repository.CreateCommand(commandModel);
                if (_repository.SaveChanges())
                {
                    var readDto = _mapper.Map<CommandReadDto>(commandModel);
                    return CreatedAtRoute(nameof(GetCommandById), new { Id = readDto.Id }, readDto);
                }
            }
            return NotFound();
        }

        //PUT api/command/{id}
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            //_repository.UpdateCommand(_mapper.Map(commandUpdateDto, commandModelFromRepo));
            _repository.SaveChanges();
            return NoContent();
        }

        //PATCH api/command/{id}
        [Authorize]
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //DELETE api/command/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
