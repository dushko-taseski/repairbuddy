using App.BusinessLogicContracts;
using App.Web.Dtos.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
    [Authorize]
    public class TicketItemController : BaseController
    {
        private readonly ITicketItemService _ticketItemService;

        public TicketItemController(ITicketItemService ticketItemService)
        {
            _ticketItemService = ticketItemService;
        }

        [Route("ticket/ticketItems")]
        [HttpGet]
        public IActionResult GetTicketItemsForTicket(int ticketId)
        {
            var result = _ticketItemService.GetTicketItemsForTicket(ticketId, LoggedInUserId);
            return Ok(result.Payload);
        }

        [Route("ticket/newTicketItem")]
        [HttpPost]
        public IActionResult AddNewTicketItem([FromBody] NewTicketItemDto newTicketItemDto)
        {
            var result = _ticketItemService.AddTicketItem(
                newTicketItemDto.Description,
                newTicketItemDto.Price,
                newTicketItemDto.TicketId
                );
            if (!result.IsFaulted)
            {
                return Ok(result.Payload);
            }

            return BadRequest();
        }

        [Route("ticket/deleteTicketItem")]
        [HttpPost]
        public IActionResult DeleteTicketItem(int ticketItemId)
        {
            var result = _ticketItemService.DeleteTicketItem(ticketItemId);
            if (!result.IsFaulted)
            {
                return Ok();
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }
    }
}
