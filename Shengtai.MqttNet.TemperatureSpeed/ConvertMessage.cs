using MQTTnet;
using MQTTnet.Protocol;
using System;
using System.Text;

namespace Shengtai.MqttNet.TemperatureSpeed
{
    public class ConvertMessage : IConvertMessage
    {
        public MqttApplicationMessage Convert(MqttApplicationMessage message)
        {
            MqttApplicationMessage applicationMessage = null;

            if (message != null && message.Topic == "fan/temperature")
            {
                var value = Encoding.UTF8.GetString(message.Payload);
                float temperature = System.Convert.ToSingle(value);
                if (temperature < 20)
                    temperature = 20;
                else if (temperature > 35)
                    temperature = 35;

                // (temperature - 20) / 16 = (speed - 36) / 25
                var speed = (temperature - 20) / 16 * 25 + 36;
                applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic("fan/speed")
                    .WithPayload(Encoding.UTF8.GetBytes(speed.ToString()))
                    .WithExactlyOnceQoS()
                    .WithRetainFlag(false)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                    .Build();
            }

            return applicationMessage;
        }
    }
}
