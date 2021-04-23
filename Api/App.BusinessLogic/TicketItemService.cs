using App.BusinessLogicContracts;
using App.DataAccessContracts;
using App.Entities;
using App.Entities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BusinessLogic
{
    public class TicketItemService : ITicketItemService
    {
        private readonly ITicketItemRepository _ticketItemRepository;

        public TicketItemService(ITicketItemRepository ticketItemRepository)
        {
            _ticketItemRepository = ticketItemRepository;
        }

        public MethodResult<TicketItem> AddTicketItem(string description, double price, int ticketId)
        {
            var newTicketItem = new TicketItem
            {
                Description = description,
                Price = price,
                TicketId = ticketId
            };

            _ticketItemRepository.Add(newTicketItem);

            return new MethodResult<TicketItem>(newTicketItem);

        }

        public VoidResult DeleteTicketItem(int id)
        {
            var deleteTicketItem = _ticketItemRepository.GetById(id);
            if (deleteTicketItem != null)
            {
                _ticketItemRepository.Delete(id);
                return VoidResult.Ok();
            }
            else
            {
                return VoidResult.CreateFaultedResult("The ticketItem can't be found.");
            }
            throw new NotImplementedException();
        }

        public MethodResult<TicketItem> GetById(int ticketItemId)
        {
            var ticketItem = _ticketItemRepository.GetById(ticketItemId);
            if (ticketItem != null)
            {
                return new MethodResult<TicketItem>(ticketItem);
            }

            return MethodResult<TicketItem>.CreateFaultedResult("The ticketItem with this Id can't be found.");
        }

        public MethodResult<List<TicketItem>> GetTicketItemsForTicket(int ticketId, int userId)
        {
            var ticketItems = _ticketItemRepository.GetAllForTicket(ticketId, userId);
            return new MethodResult<List<TicketItem>>(ticketItems);
        }
    }
}
