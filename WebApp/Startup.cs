using LogicLayer;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading;

[assembly: OwinStartupAttribute(typeof(WebApp.Startup))]
namespace WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Thread InstanceCaller = new Thread(new ThreadStart(ServerClass.StaticMethod));
            InstanceCaller.Start();
            ConfigureAuth(app);         
        }
    }
    public class ServerClass
    {
        public static void StaticMethod()
        {
            IMessageManager messageManager = new MessageManager();
            int count = 1;
            while (true)
            {
                Thread.Sleep(5000);
                System.Diagnostics.Debug.WriteLine("Testing infinite thread");
                messageManager.AddChatroomMessage(1000000, 1000002, "Hey this is an interval message that is being spammed by the server. Message count this startup = " + count);
                count++;

            }
        }
    }
}
