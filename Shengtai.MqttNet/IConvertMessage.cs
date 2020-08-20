using MQTTnet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.MqttNet
{
    public interface IConvertMessage
    {
        MqttApplicationMessage Convert(MqttApplicationMessage message);
    }
}
