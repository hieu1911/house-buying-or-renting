using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class MessageService : BaseService<Message, Message, MessageCreateDto, Message>, IMessageService
    {
        private readonly IMapper _mapper;

        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository, IMapper mapper) : base(messageRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public async Task<List<Message>> GetByUser(Guid senderId, Guid receiverId)
        {
            var result = await _messageRepository.GetByUser(senderId, receiverId);
            return result;
        }

        public async Task<List<Guid>> GetContactIds(Guid id)
        {
            var result = await _messageRepository.GetContactIds(id);
            return result;
        }

        public async override Task<Message> MapEntityCreateDtoToEntity(MessageCreateDto entityCreateDto)
        {
            var message = _mapper.Map<Message>(entityCreateDto);
            return message;
        }

        public async override Task<Message> MapEntityToEntityDto(Message entity)
        {
            return entity;
        }

        public override Task<Message> MapEntityUpdateDtoToEntity(Guid id, Message entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
