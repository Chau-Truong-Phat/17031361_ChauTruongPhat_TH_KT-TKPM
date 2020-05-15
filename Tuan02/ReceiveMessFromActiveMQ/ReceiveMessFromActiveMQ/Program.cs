using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveMessFromActiveMQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            IConnection con = factory.CreateConnection();
            con.Start();

            ISession sesstion = con.CreateSession(AcknowledgementMode.AutoAcknowledge);
            ActiveMQQueue des = new ActiveMQQueue("Hello");
            IMessageConsumer consumer = sesstion.CreateConsumer(des);
            consumer.Listener += Consumer_Listener;
            Console.ReadKey();
        }

        private static void Consumer_Listener(IMessage message)
        {
            if (message is ActiveMQTextMessage)
            {
                ActiveMQTextMessage msg = message as ActiveMQTextMessage;
                Console.WriteLine(msg.Text);
            }
        }
    }
}
