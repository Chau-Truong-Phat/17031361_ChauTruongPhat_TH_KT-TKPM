using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.Util;
using BussinessObject;
using Apache.NMS.ActiveMQ.Commands;
using System.Threading;

namespace SendMessToActiveMQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            //IObjectMessage objMessage;
            //OperatorRequestObject operatorRequestObject = new OperatorRequestObject();
            //operatorRequestObject.shortcode 

            IConnectionFactory factory = new NMSConnectionFactory("tcp://localhost:61616");
            IConnection connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            ActiveMQQueue queueDestination = new ActiveMQQueue("Hello");
            IMessageProducer messageProducer = session.CreateProducer(queueDestination);

            //Console.ReadKey();
            while (true)
            {
                IMessage send = new ActiveMQTextMessage(Console.ReadLine());
                messageProducer.Send(send);
                Thread.Sleep(100);
            }
            session.Close();
            connection.Stop();
        }
    }
}
