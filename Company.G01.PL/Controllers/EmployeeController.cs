using AutoMapper;
using Company.G01.BLL.Inerfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Company.G01.PL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(/*IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository*/IUniteOfWork uniteOfWork,IMapper mapper)
        {
            
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _uniteOfWork = uniteOfWork;
            _mapper=mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _uniteOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees= await _uniteOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var deparment=await _uniteOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Departments"]=deparment;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult>  Create(CreateEmployeeDtos model)
        {
            if (ModelState.IsValid)
            {
                if(model.image is not null)
                {
                   model.ImageName= DocumentSetting.UploadFile(model.image,"images");
                }
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    DepartmentId = model.DepartmentId
                 
                //};
                var employee=_mapper.Map<Employee>(model);
               await _uniteOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _uniteOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id/*, string ViewName = "Details"*/)
        {
            if (id is null) return BadRequest("Invaild Id");
            var employee = await _uniteOfWork.EmployeeRepository.GetAsync(id);
            if (employee is null) return NotFound(new
            {
                StatusCode = 404,
                Message = $"Employeewith Id{id} is not found"
            }
            );
            var dto=_mapper.Map<CreateEmployeeDtos>(employee);
            return View(/*ViewName,*/ dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id,string ViewName ="Edit")
        {
            var deparment = await _uniteOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Departments"] = deparment;

            if (id is null) return BadRequest("Invaild Id");
            var employee = await _uniteOfWork.EmployeeRepository.GetAsync(id);
            if (employee is null) return NotFound(new
            {
                StatusCode = 404,
                Message = $"Departmentwith Id{id} is not found"
            }
            );

            //var employeeDto = new CreateEmployeeDtos()
            //{
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    CreateAt = employee.CreateAt,
            //    HiringDate = employee.HiringDate,
            //    Email = employee.Email,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    DepartmentId = employee.DepartmentId
            //};
            var employeeDto=_mapper.Map<CreateEmployeeDtos>(employee);
            //return View(employeeDto);
            return View(ViewName, employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDtos model,String ViewName="Edit")
        {
            if (ModelState.IsValid)
            {
                if(model.ImageName is not null&&model.image is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "images");
                }
                if (model.image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.image, "images");
                }

                var employee = _mapper.Map<Employee>(model);

                //if (id != model.Id) return BadRequest();
                _uniteOfWork.EmployeeRepository.Update(employee);
                var count = await _uniteOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(ViewName,model);

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
            return await Edit(id, "Delete");
        }
        [HttpPost]
          [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDtos model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);

                //if (id != model.Id) return BadRequest();
                _uniteOfWork.EmployeeRepository.Delete(employee);
                var count = await _uniteOfWork.CompleteAsync();

                if (count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile(model.ImageName, "images");
                    }

                    return RedirectToAction("Index");
                }
            }
            return View(model);

        }


    }
}
