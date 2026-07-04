using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sem_project.Data;
using Sem_project.Models;

namespace Sem_project.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly StudentRepository _repository;

        public CreateModel(StudentRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Student Student { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Create(Student);
            return RedirectToPage("./Index");
        }
    }
}
