# Simple test tool for MQTT environment
Test your IOT devices directly

client 端就是你的 IOT device，必須在你的 MQTT 通訊中，填寫 ClientId(本系統預設值 shengtai)、Username(本系統預設值 user)、Password(本系統預設值 pass)；
我在 server 端額外設計內建預設之 client，配合依照 Shengtai.MqttNet.IConvertMessage 設計之演算法，可以動態載入規則，
譬如：讓電風扇感測室溫，依照人體舒適的溫度來變化電風扇馬達的轉速，而這演算法是由 server 來控制，
換句話說，device 向 server 訂閱"速度"，前述內建預設之 client 依照前述之演算法將溫度換算成速度，server 將速度推撥給有訂閱的 device，之後流程就不再冗述。

*MQTT伺服器的防火牆設定*
https://swf.com.tw/?p=1005

*程式原始範例*
https://www.itread01.com/content/1550365044.html

*文件*
https://github.com/chkr1011/MQTTnet/wiki/Server
