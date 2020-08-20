using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using Shengtai.MqttNet.ClientServer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shengtai.MqttNet.ClientServer
{
    public partial class frmClient : Form, IMqttClientConnectedHandler, IMqttClientDisconnectedHandler, IMqttApplicationMessageReceivedHandler
    {
        private readonly int clientNumber;
        private readonly Action<string> updateListBoxAction;

        public frmClient(int clientNumber)
        {
            this.clientNumber = clientNumber;
            InitializeComponent();

            MqttNetGlobalLogger.LogMessagePublished += (sender, e) =>
            {
                var builder = new StringBuilder();
                builder.Append($"{e.LogMessage.Timestamp} ");
                builder.Append($"{e.LogMessage.Level} ");
                builder.Append($"{e.LogMessage.Source} ");
                builder.Append($"{e.LogMessage.ThreadId} ");
                builder.Append($"{e.LogMessage.Message} ");
                builder.Append($"{e.LogMessage.Exception} ");
                builder.Append($"{e.LogMessage.LogId}");
                Console.WriteLine(builder.ToString());
            };

            this.updateListBoxAction = new Action<string>(s =>
            {
                lbxMessage.Items.Add(s);
                if (lbxMessage.Items.Count > 32)
                    lbxMessage.Items.RemoveAt(0);
            });
        }

        private async void frmClient_Load(object sender, EventArgs e)
        {
            this.Text = $"使用者端 #{this.clientNumber}";

            var ips = await Dns.GetHostAddressesAsync(Dns.GetHostName());
            foreach (var ip in ips)
            {
                switch (ip.AddressFamily)
                {
                    case AddressFamily.InterNetwork:
                        tbxServer.Text = ip.ToString();
                        break;
                    case AddressFamily.InterNetworkV6:
                        break;
                }
            }

            cbxSubMqttQuality.SelectedIndex = 0;
            cbxPubMqttQuality.SelectedIndex = 0;
        }

        private IMqttClient mqttClient = null;
        private async Task ClientDisconnectAsync()
        {
            if (this.mqttClient != null && this.mqttClient.IsConnected)
            {
                await this.mqttClient.DisconnectAsync();
                this.mqttClient.Dispose();
                this.mqttClient = null;

                tbxServer.Enabled = true;
                tbxPort.Enabled = true;
                btnConnect.Enabled = true;
                btnDisConnect.Enabled = false;
                tbxSubscribe.Enabled = false;
                cbxSubMqttQuality.Enabled = false;
                btnSubscribe.Enabled = false;
                tbxTopic.Enabled = false;
                cbxPubMqttQuality.Enabled = false;
                tbxPayload.Enabled = false;
                btnSend.Enabled = false;
            }
        }

        private async void frmClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            await this.ClientDisconnectAsync();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.mqttClient != null)
            {
                await this.mqttClient.DisconnectAsync();
                this.mqttClient = null;
            }
            
            this.mqttClient = new MqttFactory().CreateMqttClient();
            this.mqttClient.ConnectedHandler = this;
            this.mqttClient.DisconnectedHandler = this;
            this.mqttClient.ApplicationMessageReceivedHandler = this;

            var optionsBuilder = new MqttClientOptionsBuilder()
               .WithClientId($"{Settings.Default.ClientId}_{this.clientNumber}")
               .WithTcpServer(tbxServer.Text, Convert.ToInt32(tbxPort.Text))
               .WithCredentials(Settings.Default.Username, Settings.Default.Password)
               .WithCleanSession()
               .WithKeepAlivePeriod(TimeSpan.FromSeconds(100.5));
            var result = await this.mqttClient.ConnectAsync(optionsBuilder.Build());
            if (result.ResultCode == MqttClientConnectResultCode.Success)
            {
                //await this.mqttClient.SubscribeAsync(new MqttTopicFilter
                //{
                //    Topic = "fan/speed",
                //    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce
                //});
            }

            tbxServer.Enabled = false;
            tbxPort.Enabled = false;
            btnConnect.Enabled = false;
            btnDisConnect.Enabled = true;
            tbxSubscribe.Enabled = true;
            cbxSubMqttQuality.Enabled = true;
            btnSubscribe.Enabled = true;
            tbxTopic.Enabled = true;
            cbxPubMqttQuality.Enabled = true;
            tbxPayload.Enabled = true;
            btnSend.Enabled = true;
        }

        private async void btnDisConnect_Click(object sender, EventArgs e)
        {
            await this.ClientDisconnectAsync();
        }

        private async void btnSubscribe_Click(object sender, EventArgs e)
        {
            await this.mqttClient.SubscribeAsync(new MqttTopicFilter
            {
                Topic = tbxSubscribe.Text,
                QualityOfServiceLevel = (MqttQualityOfServiceLevel)cbxSubMqttQuality.SelectedIndex
            });
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(tbxTopic.Text)
            .WithPayload(Encoding.UTF8.GetBytes(tbxPayload.Text))
            .WithExactlyOnceQoS()
            .WithRetainFlag(false)
            .WithQualityOfServiceLevel((MqttQualityOfServiceLevel)cbxPubMqttQuality.SelectedIndex)
            .Build();

            await this.mqttClient.PublishAsync(applicationMessage);
        }

        public Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, $"Client is Connected:  IsSessionPresent:{eventArgs.AuthenticateResult.IsSessionPresent}");

            return Task.FromResult(0);
        }

        public Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, $"Client is DisConnected ClientWasConnected:{eventArgs.ClientWasConnected}");

            return Task.FromResult(0);
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction,
                 $"ClientID:{eventArgs.ClientId} | TOPIC:{eventArgs.ApplicationMessage.Topic} | Payload:{Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload)} | QoS:{eventArgs.ApplicationMessage.QualityOfServiceLevel} | Retain:{eventArgs.ApplicationMessage.Retain}");

            return Task.FromResult(0);
        }
    }
}
