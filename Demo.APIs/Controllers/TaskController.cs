using AutoMapper;
using Azure;
using Demo.APIs.DTOs;
using Demo.APIs.Helper;
using Demo.BLL.Interfaces;
using Demo.BLL.Specifications.Task;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Demo.APIs.Controllers
{
    public class TaskController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected ApiResponse _response;

        public TaskController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<Pageination<TaskDto>>> GetAllTasks([FromQuery] TaskSpecificationParams taskSpecParams)
        {
            if (taskSpecParams == null)
            {
                taskSpecParams = new TaskSpecificationParams();
            }

            var spec = new TaskFilterSpecification(taskSpec: taskSpecParams);
            var totalItems = await _unitOfWork.Repository<TaskEntity>().GetCountAsync(spec);
            var tasks = await _unitOfWork.Repository<TaskEntity>().GetAllWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<TaskEntity>, IReadOnlyList<TaskDto>>(tasks);
            _response.Result = new Pageination<TaskDto>(taskSpecParams.PageIndex, taskSpecParams.PageSize, totalItems, data);
            _response.StatusCode = (HttpStatusCode)200;
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            if (id == 0)
            {
                _response.IsSuccess =false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("this Task not found!");
                return BadRequest(_response);
            }
            var spec = new TaskFilterSpecification(id);
            var task = await _unitOfWork.Repository<TaskEntity>().GetEntityWithSpec(spec);
            if (task is null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = (HttpStatusCode)200;
                _response.ErrorMessages.Add("this Task not found!");
                return BadRequest(_response);
            }
            var taskDto = _mapper.Map<TaskEntity, TaskDto>(task);
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = taskDto;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The task data is missing.");
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            if (string.IsNullOrEmpty(taskDto.Title))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The Title field is required.");
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            if (taskDto.DueDate == DateTime.MinValue)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("A valid DueDate is required.");
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            try
            {
                var mappedTask = _mapper.Map<TaskDto, TaskEntity>(taskDto);
                await _unitOfWork.Repository<TaskEntity>().Create(mappedTask);
                await _unitOfWork.Complete();

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"An error occurred: {ex.Message}");
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<TaskDto>> Update([FromQuery] int id, TaskDto taskDTO)
        {
            if (taskDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("There is no task data!");
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingTask = await _unitOfWork.Repository<TaskEntity>().GetByIdAsync(id);

            if (existingTask == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Task not found!");
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            try
            {
                _mapper.Map(taskDTO, existingTask);
                _unitOfWork.Repository<TaskEntity>().Update(existingTask);
                await _unitOfWork.Complete();
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<TaskEntity, TaskDto>(existingTask);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"An error occurred: {ex.Message}");
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("this Task not found!");
                return BadRequest(_response);
            }
            var task = await _unitOfWork.Repository<TaskEntity>().GetByIdAsync(id);
            try
            {
                _unitOfWork.Repository<TaskEntity>().Delete(task);
                await _unitOfWork.Complete();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add($"{ex.Message}");
                return BadRequest(_response);
            }
        }
    }
}
