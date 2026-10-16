using AutoMapper;
using Company.G01.BLL;
using Company.G01.BLL.Inerfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Company.G01.PL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.G01.PL.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {
        //public readonly IDepartmentRepository _departmentRepository;
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUniteOfWork uniteOfWork, IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Department> departments;
            if (string.IsNullOrEmpty(SearchInput))
            {
                departments = await _uniteOfWork.DepartmentRepository.GetAllAsync();
            }
            else
            {
               departments = await _uniteOfWork.DepartmentRepository.GetByNameAsync(SearchInput);
            }
            return View(departments);

            //var department =  _departmentRepository.GetAll();
            // return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreateDepartmentDtos model)
        {
            if (ModelState.IsValid)
            {
                if (model.image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.image, "images");
                }

                //var department = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt
                //};
                var department = _mapper.Map<Department>(model);

                await _uniteOfWork.DepartmentRepository.AddAsync(department);
                var count = await _uniteOfWork.CompleteAsync();


                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id,string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invaild Id");
            var department= await _uniteOfWork.DepartmentRepository.GetAsync(id);
            if (department is null) return NotFound(new
            {
                StatusCode = 404,
                Message = $"Departmentwith Id{id} is not found"
            }
            );
            var dto = _mapper.Map<CreateDepartmentDtos>(department);

            return View(ViewName,dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("Invaild Id");
            var department = await _uniteOfWork.DepartmentRepository.GetAsync(id);
            if (department is null) return NotFound(new
            {
                StatusCode = 404,
                Message = $"Departmentwith Id{id} is not found"
            });
            //var departmentDto = new CreateDepartmentDtos()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = department.CreateAt
            //};
            var dto = _mapper.Map<CreateDepartmentDtos>(department);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,CreateDepartmentDtos departmentDtos)
        {
            if (ModelState.IsValid)
            {
                //if (id != departmentDtos.Id) return BadRequest();
                //var departments = new Department()
                //{
                //    //Id = departmentDtos.Id,
                //    Code = departmentDtos.Code,
                //    Name = departmentDtos.Name,
                //    CreateAt = departmentDtos.CreateAt
                //};
                if (departmentDtos.ImageName is not null && departmentDtos.image is not null)
                {
                    DocumentSetting.DeleteFile(departmentDtos.ImageName, "images");
                }
                if (departmentDtos.image is not null)
                {
                    departmentDtos.ImageName = DocumentSetting.UploadFile(departmentDtos.image, "images");
                }

                var department = _mapper.Map<Department>(departmentDtos);

                 _uniteOfWork.DepartmentRepository.Update(department);
                var count = await _uniteOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(departmentDtos);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest("Invaild Id");
            //var department = _departmentRepository.Get(id);
            //if (department is null) return NotFound(new
            //{
            //    StatusCode = 404,
            //    Message = $"Departmentwith Id{id} is not found"
            //}
            //);
            //return View(department);
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateDepartmentDtos departmentDtos)
        {
            if (ModelState.IsValid)
            {
                //if (id != department.Id) return BadRequest();
                var department = _mapper.Map<Department>(departmentDtos);

                 _uniteOfWork.DepartmentRepository.Delete(department);
                var count = await _uniteOfWork.CompleteAsync();

                if (count > 0)
                {
                    if (departmentDtos.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile(departmentDtos.ImageName, "images");
                    }

                    return RedirectToAction("Index");
                }
            }
            return View(departmentDtos);

        }



    }
}
