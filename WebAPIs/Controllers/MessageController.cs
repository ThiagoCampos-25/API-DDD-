using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IMessage _IMenssage;
        private readonly IServiceMessage _IServiceMessage;

        public MessageController(IMapper IMapper, IMessage IMenssage, IServiceMessage IServiceMessage)
        {
            _IMapper = IMapper;
            _IMenssage = IMenssage;
            _IServiceMessage = IServiceMessage;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(MessageViewModel message)
        {
            message.UserId = await RetornarIdUsuarioLogado();

            var messageMap = _IMapper.Map<Message>(message);
            //await _IMenssage.Add(messageMap);
            await _IServiceMessage.Adicionar(messageMap);

            return messageMap.Notitycoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Update")]
        public async Task<List<Notifies>> Update(MessageViewModel message) 
        {
            var messageMap = _IMapper.Map<Message>(message);
            //await _IMenssage.Update(messageMap);
            await _IServiceMessage.Atualizar(messageMap);

            return messageMap.Notitycoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Delete")]
        public async Task<List<Notifies>> Delete(MessageViewModel message)
        {

            var messageMap = _IMapper.Map<Message>(message);
            await _IMenssage.Delete(messageMap);

            return messageMap.Notitycoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/GetEntityById")]
        public async Task<MessageViewModel> GetEntityById(Message message)
        {

            message = await _IMenssage.GetEntityById(message.Id);
            var messageMap = _IMapper.Map<MessageViewModel>(message);
            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/List")]
        public async Task<List<MessageViewModel>> ListaMessageAtivas()
        { 

            var mensagens = await _IServiceMessage.ListaMessageAtivas();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(mensagens);
            return messageMap;
        }



        private async Task<string> RetornarIdUsuarioLogado()
        {

            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }

            return string.Empty;
        }
    }
}
