using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sem_project.Data;
using Sem_project.Models;

namespace Sem_project.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly StudentRepository _repository;

        public DeleteModel(StudentRepository repository)
        {
            _repository = repository;
        }

        public Student? Student { get; set; }

        public IActionResult OnGet(int id)
        {
            Student = _repository.GetById(id);
            if (Student == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _repository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
