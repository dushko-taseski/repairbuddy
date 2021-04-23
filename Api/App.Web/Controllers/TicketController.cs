using App.BusinessLogicContracts;
using App.Web.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Route("tickets")]
        [HttpGet]
        public IActionResult GetAllTickets()
        {
            var resullt = _ticketService.GetAllTicketsForUser(LoggedInUserId);
            return Ok(resullt.Payload);
        }

        [Route("ticket/new")]
        [HttpPost]
        public IActionResult AddNewTicket([FromBody] NewTicketDto newTicketDto)
        {
            var result = _ticketService.AddTicket(
                newTicketDto.Description,
                newTicketDto.AssetId
                );

            if (!result.IsFaulted)
            {
                return Ok(result.Payload);
            }

            return BadRequest();
        }

        [Route("ticket/closeTicket")]
        [HttpPost]
        public IActionResult CloseTicket(int ticketId)
        {
            var result = _ticketService.UpdateTicket(ticketId);
            if (!result.IsFaulted)
            {
                return Ok(result.Payload);
            }

            return BadRequest();
        }

        [Route("ticket/delete")]
        [HttpPost]
        public IActionResult DeleteTicket(int ticketId)
        {
            var result = _ticketService.DeleteTicket(ticketId);
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
