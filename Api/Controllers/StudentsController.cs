using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> _repository;
        private readonly IMapper _mapper;
        public StudentsController(IRepository<Student> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _repository.GetAllAsync();
            

            return Ok(_mapper.Map<List<StudentDto>>(students));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var students = await _repository.GetAsync(id);
            if (students == null) return NotFound("There is no student with this id.");
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            bool result = await _repository.AddAsync(student);
            if (!result) return BadRequest("Be clever");
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StudentDto studentDto)
        {
            Student existStudent = await _repository.GetAsync(id);
            if (existStudent == null) return NotFound("There is no student with this id");
            existStudent.Name = studentDto.Name;
            existStudent.Surname = studentDto.Surname;
            bool result = _repository.Update(existStudent);
            if (!result) return BadRequest("Sorry, I can't handle this");
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var student = await _repository.GetAsync(id);
            var result = await _repository.DeleteAsync(student);
            if (!result) return BadRequest("Something went wrong :(");
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
