using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Sem_project.Data;
using Sem_project.Models;

namespace Sem_project.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentRepository _repository;

        public IndexModel(StudentRepository repository)
        {
            _repository = repository;
        }

        public List<Student> StudentsList { get; set; } = new();

        public void OnGet()
        {
            StudentsList = _repository.GetAll();
        }
    }
}