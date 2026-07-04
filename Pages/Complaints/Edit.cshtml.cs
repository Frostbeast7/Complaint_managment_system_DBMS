using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sem_project.Data;
using Sem_project.Models;

namespace Sem_project.Pages.Complaints
{
    public class EditModel : PageModel
    {
        private readonly ComplaintRepository _repository;

        public EditModel(ComplaintRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Complaint Complaint { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var complaint = _repository.GetById(id);
            if (complaint == null)
            {
                return NotFound();
            }

            Complaint = complaint;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Update(Complaint);
            return RedirectToPage("./Index");
        }
    }
}
