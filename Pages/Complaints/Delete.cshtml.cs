using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sem_project.Data;
using Sem_project.Models;

namespace Sem_project.Pages.Complaints
{
    public class DeleteModel : PageModel
    {
        private readonly ComplaintRepository _repository;

        public DeleteModel(ComplaintRepository repository)
        {
            _repository = repository;
        }

        public Complaint? Complaint { get; set; }

        public IActionResult OnGet(int id)
        {
            Complaint = _repository.GetById(id);
            if (Complaint == null)
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
