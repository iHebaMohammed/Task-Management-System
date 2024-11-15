using Demo.APIs.DTOs;
using Demo.APIs.Helper;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Demo.MVC.HttpClientService;
using Demo.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Humanizer;
using System.Drawing.Printing;

namespace Demo.MVC.Controllers
{
    public class TasksController : Controller
    {
        private readonly CrudHttpService _service;

        public TasksController(CrudHttpService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var apiUrl = $"Task?PageIndex={pageIndex}&PageSize={pageSize}";
            var tasks = await _service.GetAll<GetAllResponseViewModel>(apiUrl); 
            return View(tasks.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskDto dto)
        {
            var apiUrl = "Task";
            var response = await _service.PostAsync<ResponseViewModel>(apiUrl , dto);
            if (response == null)
                return View(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var apiUrl = $"Task/{id}";
            var response = await _service.GetById<ResponseViewModel>(apiUrl , id);
            var task = response.Result;
            ViewBag.TaskStatusList = Enum.GetValues(typeof(TaskStatusEnum))
                                 .Cast<TaskStatusEnum>()
                                 .Select(status => new SelectListItem
                                 {
                                     Value = status.ToString(),
                                     Text = status.ToString()
                                 })
                                 .ToList();
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id , TaskDto taskDto)
        {
            ViewBag.TaskStatusList = Enum.GetValues(typeof(TaskStatusEnum))
                                 .Cast<TaskStatusEnum>()
                                 .Select(status => new SelectListItem
                                 {
                                     Value = status.ToString(),
                                     Text = status.ToString()
                                 })
                                 .ToList();
            var apiUrl = $"Task?id={id}";
            var response = await _service.PutAsync<ResponseViewModel>(apiUrl, taskDto);
            if (response == null)
                return View(taskDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var apiUrl = $"Task/{id}";
            var response = await _service.GetById<ResponseViewModel>(apiUrl, id);
            var task = response.Result;
            ViewBag.TaskStatusList = Enum.GetValues(typeof(TaskStatusEnum))
                                 .Cast<TaskStatusEnum>()
                                 .Select(status => new SelectListItem
                                 {
                                     Value = status.ToString(),
                                     Text = status.ToString()
                                 })
                                 .ToList();
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id , TaskDto dto)
        {
            if(id != dto.Id)
                return BadRequest();
            
            var apiUrl = $"Task/{id}";

            var response = await _service.DeleteAsync(apiUrl);

            return RedirectToAction(nameof(Index));
        }
    }
}
