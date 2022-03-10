using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerWebAPI.DAL.Context;
using TaskTrackerWebAPI.DAL.Entities;
using TaskTrackerWebAPI.DAL.Repositories.Implementation;
using TaskTrackerWebAPI.DAL.Repositories.Interfaces;
using TaskTrackerWebAPI.View_Models;

namespace TaskTrackerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectRepository _projectRepository;
        IMapper _mapper;
        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjectVM>> Get()
        {
            if (!_projectRepository.GetAll().Any())
                return NotFound();

            return Ok(_projectRepository.GetAll().Select(x => _mapper.Map<ProjectVM>(x)).ToList());
        }

        [HttpGet("{projectId}")]
        public ActionResult<ProjectVM> Get(int projectId)
        {
            var temp = _projectRepository.GetById(projectId);
            ProjectVM project = _mapper.Map<ProjectVM>(temp);

            if (project == null)
            {
                return NotFound(project);
            }

            return Ok(project); ;
        }

        [HttpPost]
        public ActionResult<ProjectVM> Post([FromBody] ProjectVM project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            _projectRepository.Add(_mapper.Map<ProjectEntity>(project));
            _projectRepository.Save();

            return Ok();
        }

        [HttpPut("{projectId}")]
        public ActionResult<ProjectVM> Put(int projectId, [FromBody] ProjectVM project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (!_projectRepository.GetAll().Any(x => x.Id == projectId))
            {
                return NotFound();
            }

            var tempProject = _mapper.Map<ProjectEntity>(project);

            var updatingEntity = _projectRepository.GetById(projectId);

            updatingEntity.Name = tempProject.Name;
            updatingEntity.StartDate = tempProject.StartDate;
            updatingEntity.CompletionDate = tempProject.CompletionDate;
            updatingEntity.Status = tempProject.Status;
            updatingEntity.Priority = tempProject.Priority;
            updatingEntity.Tasks = tempProject.Tasks;

            _projectRepository.Update(updatingEntity);
            _projectRepository.Save();

            return Ok(project);
        }

        [HttpDelete("{projectId}")]
        public ActionResult<ProjectVM> Delete(int projectId)
        {
            if (!_projectRepository.DeleteById(projectId))
            {
                return NotFound();
            }

            _projectRepository.Save();

            return Ok();
        }
    }
}
