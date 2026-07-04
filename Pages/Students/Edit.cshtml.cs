using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sem_project.Data;
using Sem_project.Models;

namespace Sem_project.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly StudentRepository _repository;

        public EditModel(StudentRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Student Student { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var student = _repository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            Student = student;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Update(Student);
            return RedirectToPage("./Index");
        }
    }
}
