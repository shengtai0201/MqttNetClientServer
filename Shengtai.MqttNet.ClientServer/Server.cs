using LiteDB;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Shengtai.MqttNet.ClientServer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shengtai.MqttNet.ClientServer
{
    public partial class frmServer : Form, IMqttServerStartedHandler, IMqttServerStoppedHandler, IMqttServerClientConnectedHandler,
        IMqttServerClientDisconnectedHandler, IMqttServerClientSubscribedTopicHandler, IMqttServerClientUnsubscribedTopicHandler,
        IMqttApplicationMessageReceivedHandler, IMqttClientConnectedHandler, IMqttClientDisconnectedHandler
    {
        private readonly Action<string> updateListBoxAction;
        private int clientNumbers;

        public frmServer()
        {
            InitializeComponent();

            this.updateListBoxAction = new Action<string>(s =>
            {
                lbxMessage.Items.Add(s);
                if (lbxMessage.Items.Count > 32)
                    lbxMessage.Items.RemoveAt(0);

                var visibleItems = lbxMessage.ClientRectangle.Height / lbxMessage.ItemHeight;
                lbxMessage.TopIndex = lbxMessage.Items.Count - visibleItems + 1;
            });

            this.clientNumbers = 0;
        }

        private async void frmServer_Load(object sender, EventArgs e)
        {
            var menu = this.menuStrip1.Items[0] as ToolStripMenuItem;
            var newClient = menu.DropDownItems[0] as ToolStripMenuItem;
            newClient.Click += (sender, e) =>
            {
                var form = new frmClient(++this.clientNumbers);
                form.Focus();
                form.Show();
            };
            var dynamicConverter = menu.DropDownItems[1] as ToolStripMenuItem;
            dynamicConverter.Click += (sender, e) =>
            {
                var form = frmConverter.Instance;
                form.Focus();
                form.Show();
            };

            this.sslMessage.Text = "總連線數：0";

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
        }

        private IMqttServer mqttServer = null;
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (this.mqttServer != null)
                return;

            this.mqttServer = new MqttFactory().CreateMqttServer();
            this.mqttServer.StartedHandler = this;
            this.mqttServer.StoppedHandler = this;
            this.mqttServer.ClientConnectedHandler = this;
            this.mqttServer.ClientDisconnectedHandler = this;
            this.mqttServer.ClientSubscribedTopicHandler = this;
            this.mqttServer.ClientUnsubscribedTopicHandler = this;
            this.mqttServer.ApplicationMessageReceivedHandler = this;

            var optionsBuilder = new MqttServerOptionsBuilder().WithConnectionBacklog(1000).WithDefaultEndpointPort(Convert.ToInt32(tbxPort.Text));

            if (!string.IsNullOrEmpty(tbxServer.Text))
                optionsBuilder.WithDefaultEndpointBoundIPAddress(IPAddress.Parse(tbxServer.Text));

            optionsBuilder.WithConnectionValidator(context =>
            {
                if (string.IsNullOrEmpty(context.ClientId))
                {
                    context.ReasonCode = MqttConnectReasonCode.ClientIdentifierNotValid;
                    return;
                }

                if (context.Username != Settings.Default.Username || context.Password != Settings.Default.Password)
                {
                    context.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                    return;
                }

                context.ReasonCode = MqttConnectReasonCode.Success;
            });

            optionsBuilder.WithApplicationMessageInterceptor(context =>
            {
                // 控管 client 發佈
                if (string.IsNullOrEmpty(context.ClientId) || !context.ClientId.StartsWith(Settings.Default.ClientId))
                {
                    context.AcceptPublish = false;
                    return;
                }
            });

            optionsBuilder.WithSubscriptionInterceptor(context =>
            {
                // 控管 client 訂閱
                if (!context.ClientId.StartsWith(Settings.Default.ClientId))
                {
                    context.AcceptSubscription = false;
                    return;
                }
            });

            await this.mqttServer.StartAsync(optionsBuilder.Build());

            tbxServer.Enabled = false;
            tbxPort.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            if (this.mqttServer != null)
            {
                var statuse = await this.mqttServer.GetClientStatusAsync();
                foreach (var status in statuse)
                    await status.DisconnectAsync();

                await this.mqttServer.StopAsync();
                this.mqttServer = null;
            }

            tbxServer.Enabled = true;
            tbxPort.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void lbxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c' || e.KeyChar == 'C')
                lbxMessage.Items.Clear();
        }

        // 內建之 client 端不訂閱任何東西
        private IMqttClient mqttClient = null;
        public async Task HandleServerStartedAsync(EventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, "Mqtt Server Start...");

            if (this.mqttClient != null)
            {
                await this.mqttClient.DisconnectAsync();
                this.mqttClient = null;
            }

            this.mqttClient = new MqttFactory().CreateMqttClient();
            this.mqttClient.ConnectedHandler = this;
            this.mqttClient.DisconnectedHandler = this;

            var optionsBuilder = new MqttClientOptionsBuilder()
               .WithClientId($"{Settings.Default.ClientId}_Inner")
               .WithTcpServer(tbxServer.Text, Convert.ToInt32(tbxPort.Text))
               .WithCredentials(Settings.Default.Username, Settings.Default.Password)
               .WithCleanSession()
               .WithKeepAlivePeriod(TimeSpan.FromSeconds(100.5));
            await this.mqttClient.ConnectAsync(optionsBuilder.Build());

            var s = await this.mqttServer.GetClientStatusAsync();
            this.sslMessage.Text = $"總連接數：{s.Count}";
        }

        public async Task HandleServerStoppedAsync(EventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, "Mqtt Server Stop...");

            if (this.mqttClient != null && this.mqttClient.IsConnected)
            {
                await this.mqttClient.DisconnectAsync();
                this.mqttClient.Dispose();
                this.mqttClient = null;
            }
        }

        public async Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, $">Client Connected:ClientId:{eventArgs.ClientId},ProtocalVersion:");

            var s = await this.mqttServer.GetClientStatusAsync();
            this.sslMessage.Text = $"總連接數：{s.Count}";
        }

        public async Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, $"<Client DisConnected:ClientId:{eventArgs.ClientId}");

            var s = await this.mqttServer.GetClientStatusAsync();
            this.sslMessage.Text = $"總連接數：{s.Count}";
        }

        public Task HandleClientSubscribedTopicAsync(MqttServerClientSubscribedTopicEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction,
               $"@ClientSubscribedTopic ClientId:{eventArgs.ClientId} Topic:{eventArgs.TopicFilter.Topic} QualityOfServiceLevel:{eventArgs.TopicFilter.QualityOfServiceLevel}");

            return Task.FromResult(0);
        }

        public Task HandleClientUnsubscribedTopicAsync(MqttServerClientUnsubscribedTopicEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, $"%ClientUnsubscribedTopic ClientId:{eventArgs.ClientId} Topic:{eventArgs.TopicFilter.Length}");

            return Task.FromResult(0);
        }

        private async Task<ICollection<IConvertMessage>> CreateInstancesAsync()
        {
            var result = new List<IConvertMessage>();

            using (var db = new LiteDatabase(Settings.Default.ConnectionString))
            {
                var storage = db.GetStorage<string>();

                var streams = storage.FindAll().Select(x => x.OpenRead()).ToList();
                foreach (var stream in streams)
                {
                    var memory = new MemoryStream();
                    await stream.CopyToAsync(memory);
                    var rawAssembly = memory.ToArray();

                    Assembly assembly = Assembly.Load(rawAssembly);
                    foreach (var type in assembly.GetExportedTypes())
                        if (assembly.CreateInstance(type.FullName) is IConvertMessage instance)
                            result.Add(instance);
                }
            }

            return result;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction,
                $"ClientId:{eventArgs.ClientId} Topic:{eventArgs.ApplicationMessage.Topic} Payload:{Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload)} QualityOfServiceLevel:{eventArgs.ApplicationMessage.QualityOfServiceLevel}");

            var instances = await CreateInstancesAsync();
            foreach (var instance in instances)
            {
                var message = instance.Convert(eventArgs.ApplicationMessage);
                if (message != null)
                    await this.mqttClient.PublishAsync(message);
            }
        }

        public Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, $"Inner Client is Connected:  IsSessionPresent:{eventArgs.AuthenticateResult.IsSessionPresent}");

            return Task.FromResult(0);
        }

        public Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            lbxMessage.BeginInvoke(this.updateListBoxAction, $"Inner Client is DisConnected ClientWasConnected:{eventArgs.ClientWasConnected}");

            return Task.FromResult(0);
        }
    }
}
