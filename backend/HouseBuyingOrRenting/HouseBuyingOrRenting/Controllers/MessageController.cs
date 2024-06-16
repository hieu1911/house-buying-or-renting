using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessageController : BaseController<Message, Message, MessageCreateDto, Message>
    {
        private readonly IMessageService _messageService;

        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService) : base(messageService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetByUser(Guid senderId, Guid receiverId)
        {
            var result = await _messageService.GetByUser(senderId, receiverId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("contact/{id}")]
        public async Task<IActionResult> GetContactsById(Guid id)
        {
            var contactIds = await _messageService.GetContactIds(id);
            var result = await _userService.GetByIdsAsync(contactIds);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
