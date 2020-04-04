using IFT585_TP3.Server.Model;
using IFT585_TP3.Server.Repositories;
using IFT585_TP3.Server.Repositories.MessageRepositories;
using IFT585_TP3.Server.RESTFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.Controllers
{
    public class MessageController
    {
        public MessageInMemoryRepository MessageRepo { get; set; }
        public GroupInMemoryRepository GroupRepo { get; set; }

        public void RegisterRoutes(RESTFramework.Server server)
        {
            server.Use(Method.GET, "/api/message/:group_name", GetMessages);
            server.Use(Method.POST, "/api/message/:group_name", AddMessage);
        }

        #region Handler

        private async Task GetMessages(Request req, Response res)
        {
            var groupName = req.Params.Get("group_name");
            var group = GroupRepo.Retrieve(groupName);

            if (!group.MemberUsernames.Contains(req.Context.AuthenticatedUser.Username))
            {
                await res.Unauthorized($"User is not a member of the requested group.");
                return;
            }

            var timestamp = req.Query.Get("from");

            var fromDate = timestamp == null ? new DateTime() : DateTime.Parse(timestamp, null, DateTimeStyles.RoundtripKind);
            var messages = MessageRepo.RetrieveAll().ToList().FindAll(_msg => _msg.GroupName == groupName && _msg.Timestamp >= fromDate);

            var response = new Common.Reponses.MessageListReponse()
            {
                Messages = messages.Select(_msg => new Common.Reponses.Message()
                {
                    Content = _msg.Content,
                    GroupName = _msg.GroupName,
                    SenderUsername = _msg.SenderUsername,
                    Timestamp = _msg.Timestamp
                })
            };
            await res.Json(response);
        }

        private async Task AddMessage(Request req, Response res)
        {
            var group = GroupRepo.Retrieve(req.Params.Get("group_name"));
            if (!group.MemberUsernames.Contains(req.Context.AuthenticatedUser.Username))
            {
                await res.Unauthorized($"User is not a member of the requested group.");
                return;
            }

            var message = await req.GetBody<Common.Reponses.Message>();

            MessageRepo.Create(new Message()
            {
                Content = message.Content,
                GroupName = group.GroupName,
                Timestamp = DateTime.Now,
                SenderUsername = req.Context.AuthenticatedUser.Username
            });

            res.Close();
        }

        #endregion Handler
    }
}
