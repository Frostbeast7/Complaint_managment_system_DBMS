using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sem_project.Data;
using Sem_project.Models;

namespace Sem_project.Pages.Complaints
{
    public class CreateModel : PageModel
    {
        private readonly ComplaintRepository _repository;

        public CreateModel(ComplaintRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Complaint Complaint { get; set; } = new();

        public void OnGet()
        {
            Complaint.Date = DateTime.Today;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Create(Complaint);
            return RedirectToPage("./Index");
        }
    }
}
