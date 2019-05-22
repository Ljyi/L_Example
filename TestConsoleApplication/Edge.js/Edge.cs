using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeJs;
namespace TestConsoleApplication.Edge.js
{
    public static class Edged
    {
        public static async void Start()
        {
            var createWebSocketServer = EdgeJs.Edge.Func(@"
            var WebSocketServer = require('ws').Server;
 
            return function (port, cb) {
                var wss = new WebSocketServer({ port: port });
                wss.on('connection', function (ws) {
                    ws.on('message', function (message) {
                        ws.send(message.toUpperCase());
                    });
                    ws.send('Hello!');
                });
                cb();
            };
        ");
            await createWebSocketServer(8080);
        }
    }
}
