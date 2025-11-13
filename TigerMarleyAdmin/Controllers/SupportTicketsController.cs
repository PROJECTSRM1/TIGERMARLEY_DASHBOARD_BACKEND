using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;
using TigerMarleyAdmin.DTOs;

namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportTicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupportTicketsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all support tickets
        [HttpGet("list")]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _context.GetSupportTicketsFromFunctionAsync(-1);

            var ticketDtos = tickets.Select(t => new SupportTicketDto
            {
                Id = t.Id,
                CustomerName = t.CustomerName,
                ProductName = t.ProductName,
                //Quantity = t.Quantity,
                SubjectName = t.SubjectName,
                Description = t.Description,
                Total = t.Total,
                CreatedDate = t.CreatedDate,
                //IsActive = t.IsActive
            }).ToList();

            return Ok(ticketDtos);
        }

        // ✅ Get single ticket by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var tickets = await _context.GetSupportTicketsFromFunctionAsync(id);
            var ticket = tickets.FirstOrDefault();

            if (ticket == null)
                return NotFound(new { message = "Support ticket not found" });

            var ticketDto = new SupportTicketDto
            {
                Id = ticket.Id,
                CustomerName = ticket.CustomerName,
                ProductName = ticket.ProductName,
                //Quantity = ticket.Quantity,
                SubjectName = ticket.SubjectName,
                Description = ticket.Description,
                Total = ticket.Total,
                CreatedDate = ticket.CreatedDate,
                //IsActive = ticket.IsActive
            };

            return Ok(ticketDto);
        }
    }
}
