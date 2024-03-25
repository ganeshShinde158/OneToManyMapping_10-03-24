using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneToManyMapping_10_03_24.Models;
using OneToManyMapping_10_03_24.Services;

namespace OneToManyMapping_10_03_24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentServiceRepo _studentService;

        public StudentController(StudentServiceRepo studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("AddStudentWithQualifications")]
        public IActionResult AddStudentWithQualifications([FromBody] StudentModels student)
        {
            try
            {
                var response = _studentService.InsertStudentWithQualifications(student);

                // Check the operation success flag in the response
                if (response.OperationSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel
                {
                    ResponseMsg = $"An error occurred: {ex.Message}",
                    OperationSuccess = false,
                    InsertedStudentID = 0
                });
            }
        }
        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _studentService.GetAllStudents();

                // Check if there are any students
                if (students.Count > 0)
                {
                    return Ok(students);
                }
                else
                {
                    return NotFound(new ResponseModel
                    {
                        ResponseMsg = "No student records found.",
                        OperationSuccess = false,
                        InsertedStudentID = 0
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel
                {
                    ResponseMsg = $"An error occurred: {ex.Message}",
                    OperationSuccess = false,
                    InsertedStudentID = 0
                });
            }
        }
    }
}