using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ChatUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context, UserManager<ChatUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task BroadcastFromClient(string message)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(Context.User);

                var m = new Message()
                {
                    MessageBody = message,
                    MessageDtTm = DateTime.Now,
                    FromUser = currentUser
                };

                _context.Messages.Add(m);
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync(
                    "Broadcast", "yarrağım"
                    //new
                    //{
                    //    messageBody = m.MessageBody,
                    //    fromUser = m.FromUser,
                    //    messageDtTm = m.MessageDtTm.ToString(
                    //            "HH:mm", CultureInfo.InvariantCulture
                    //        )
                    //}//todo fix
                    );
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("HubError", new { error = ex.Message });
            }
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync(
                "UserConnected",
                new
                {
                    connectionId = Context.ConnectionId,
                    connectionDtTm = DateTime.Now,
                    messageDtTm = DateTime.Now.ToString(
                                "hh:mm tt MMM dd", CultureInfo.InvariantCulture
                            )
                }
                );
            
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("UserDisconnected", $"User disconnected, ConnectionId {Context.ConnectionId}");
        }

    }
}
