using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerWebAPI.DAL.Entities;
using TaskTrackerWebAPI.DAL.Repositories.Interfaces;
using TaskTrackerWebAPI.View_Models;

namespace TaskTrackerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        readonly IProjectRepository _projectRepository;
        readonly ITaskRepository _taskRepository;
        readonly IMapper _mapper;
        public TaskController(ITaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<TaskVM>> GetAllTasks()
        {
            if (!_taskRepository.GetAll().Any())
                return NotFound();

            return Ok(_taskRepository.GetAll().Select(x => _mapper.Map<TaskVM>(x)).ToList());
        }

        [HttpGet("{projectId}")]
        public ActionResult<List<TaskVM>> GetAllTasksInProject(int projectId)
        {
            if (_projectRepository.GetById(projectId) == null)
                return NotFound();

            if (_projectRepository.GetById(projectId).Tasks.Count == 0)
                return Ok(new List<TaskVM>());

            // there is an easier way of this using _projectRepository
            return Ok(_taskRepository.GetAllTasksInProject(projectId)
                                  .Select(x => _mapper.Map<TaskVM>(x))
                                  .ToList());
        }

        [HttpPost("{projectId}")]
        public ActionResult AddTaskToProject(int projectId, [FromBody] TaskVM taskVM)
        {
            if (_projectRepository.GetById(projectId) == null)
                return NotFound();

            if (taskVM == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            var tempTaskEntity = _mapper.Map<TaskEntity>(taskVM);

            tempTaskEntity.Project.Id = projectId;

            _taskRepository.Add(tempTaskEntity);
            _taskRepository.Save();

            return Ok();
        }

        [HttpDelete("{taskId}")]
        public ActionResult DeleteTask(int taskId)
        {
            if (!_taskRepository.DeleteById(taskId))
                return NotFound();
            _taskRepository.Save();
            return Ok();
        }

        [HttpDelete("{projectId}/{taskId}")]
        public ActionResult DeleteTaskFromProject(int projectId, [FromRoute] int taskId)
        {
            if (_projectRepository.GetById(projectId) == null)
                return NotFound();

            if (_projectRepository.GetById(projectId).Tasks.Any(x => x.Id == taskId))
                return NotFound();

            if (!_taskRepository.DeleteById(taskId))
                return Problem();

            _taskRepository.Save();
            return Ok();
        }
    }
}
