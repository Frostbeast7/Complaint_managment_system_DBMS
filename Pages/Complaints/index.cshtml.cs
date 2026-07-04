using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Sem_project.Data;
using Sem_project.Models;

namespace Sem_project.Pages.Complaints
{
    public class IndexModel : PageModel
    {
        private readonly ComplaintRepository _repository;

        public IndexModel(ComplaintRepository repository)
        {
            _repository = repository;
        }

        public List<Complaint> ComplaintsList { get; set; } = new();

        public void OnGet()
        {
            ComplaintsList = _repository.GetAll();
        }

    }
}