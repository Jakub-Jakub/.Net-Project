using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LogicLayer
{
    public class MessageManager : IMessageManager
    {
        private IMessageAccessor _messageAccessor;

        public MessageManager()
        {
            _messageAccessor = new MessageAccessor();
        }
        public List<MessageVM> GetChatroomMessages(int chatroomID)
        {
            List<MessageVM> messages = new List<MessageVM>();
            try
            {
                messages = _messageAccessor.SelectMessagesByChatroomID(chatroomID);
                foreach (var message in messages)
                {                   
                    if (message.UserImage != null)
                    {
                        ImageSource image;
                        image = ImageHelper.ConvertByteArrayToImageSource(message.UserImage);
                        message.UserImageSource = image;
                    }                                       
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return messages;
        }

        public bool AddChatroomMessage(int chatroomId, int userId, string message)
        {
            bool result = false;

            try
            {
                result = (0 != _messageAccessor.InsertChatroomMessage(chatroomId, userId, message));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
    }
}
