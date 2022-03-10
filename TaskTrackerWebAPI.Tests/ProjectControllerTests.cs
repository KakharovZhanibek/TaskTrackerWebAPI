using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TaskTrackerWebAPI.Controllers;
using TaskTrackerWebAPI.DAL.Entities;
using TaskTrackerWebAPI.DAL.Repositories.Interfaces;
using TaskTrackerWebAPI.View_Models;

namespace TaskTrackerWebAPI.Tests
{
    public class ProjectControllerTests
    {

        private readonly ProjectController _projectController;
        private readonly Mock<IProjectRepository> _projectRepoMock = new Mock<IProjectRepository>();

        private readonly IMapper _mapper = MappingConfigurator.ConfigureMapper();

        public ProjectControllerTests()
        {
            _projectController = new ProjectController(_projectRepoMock.Object, _mapper);
        }

        [Test]
        public void GetById_ShouldReturnProject_WhenProjectExist()
        {
            //Arrange
            var projectId = 1;
            var projectDto = new ProjectEntity
            {
                Id = projectId,
                Name = "TestProject",
                StartDate = null,
                CompletionDate = null,
                Priority = 4,
                Status = DAL.Entities.ProjectStatus.NotStarted,
                CreatedDate = DateTime.Now,
                Tasks = null
            };

            _projectRepoMock.Setup(x => x.GetById(projectId)).Returns(projectDto);

            //Act
            var actionResult = _projectController.Get(projectId);

            //Assert

            var result = actionResult.Result as OkObjectResult;
            var returnProject = result.Value as ProjectVM;
            var mappedProjectDto = _mapper.Map<ProjectVM>(projectDto);

            Assert.AreEqual(mappedProjectDto.Id, returnProject.Id);
            Assert.AreEqual(mappedProjectDto.Name, returnProject.Name);
            Assert.AreEqual(mappedProjectDto.StartDate, returnProject.StartDate);
            Assert.AreEqual(mappedProjectDto.CompletionDate, returnProject.CompletionDate);
            Assert.AreEqual(mappedProjectDto.Priority, returnProject.Priority);
            Assert.AreEqual(mappedProjectDto.Status, returnProject.Status);
            Assert.AreEqual(mappedProjectDto.Tasks, returnProject.Tasks);
        }

        [Test]
        public void GetById_ShouldReturnNothing_WhenProjectDoesNotExist()
        {
            //Arrange

            _projectRepoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => null);

            //Act
            var actionResult = _projectController.Get(new Random().Next(0, 100));

            //Assert

            Assert.Null(actionResult.Value);
        }

        [Test]
        public void GetAllProjects()
        {
            List<ProjectEntity> projectsList = new List<ProjectEntity>
            {
                new ProjectEntity()
                {
                    Id = 1,
                    CreatedDate=DateTime.Now,
                    Name = "TestProject1",
                    StartDate = null,
                    CompletionDate = null,
                    Priority = 4,
                    Status = DAL.Entities.ProjectStatus.NotStarted,
                    Tasks = null
                },
                new ProjectEntity()
                {
                    Id = 2,
                    CreatedDate=DateTime.Now,
                    Name = "TestProject2",
                    StartDate = null,
                    CompletionDate = null,
                    Priority = 5,
                    Status = DAL.Entities.ProjectStatus.NotStarted,
                    Tasks = null
                },
            };

            _projectRepoMock.Setup(x => x.GetAll()).Returns(projectsList);


            var actionResult = _projectController.Get();


            var returnedList = (actionResult.Result as OkObjectResult).Value as List<ProjectVM>;

            Assert.AreEqual(returnedList.Count,projectsList.Count);
        }
    }
}