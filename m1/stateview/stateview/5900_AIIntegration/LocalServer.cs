using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stateview._5900_AIIntegration
{
    public class LocalServer
    {
        private HttpListener _listener;
        private bool _isRunning = false;
        private Thread _serverThread;

        public event Action<string> OnRequestReceived;

        public int Port { get; private set; } = 0;
        
        public void Start(int startPort = 5000)
        {
            if (_isRunning) return;

            for (int p = startPort; p < startPort + 10; p++)
            {
                try 
                {
                    _listener = new HttpListener();
                    _listener.Prefixes.Add($"http://localhost:{p}/");
                    _listener.Start();
                    _isRunning = true;
                    Port = p;

                    _serverThread = new Thread(RunServer);
                    _serverThread.IsBackground = true;
                    _serverThread.Start();
                
                    System.Diagnostics.Debug.WriteLine($"LocalServer started on port {p}");
                    return; // Success
                }
                catch(Exception ex)
                {
                   System.Diagnostics.Debug.WriteLine($"Failed to bind port {p}: {ex.Message}");
                   _listener?.Close();
                   _listener = null;
                   // Try next port
                }
            }
            System.Diagnostics.Debug.WriteLine($"LocalServer failed to start on any port in range {startPort}-{startPort+9}");
        }

        public void Stop()
        {
            _isRunning = false;
            _listener?.Stop();
            _listener?.Close();
        }

        private void RunServer()
        {
            while (_isRunning)
            {
                try
                {
                    var context = _listener.GetContext();
                    Task.Run(() => ProcessRequest(context));
                }
                catch (HttpListenerException)
                {
                    // Listener stopped
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Server error: {ex.Message}");
                }
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                var request = context.Request;
                var response = context.Response;

                string responseString = "{ \"status\": \"ok\" }";

                if (request.Url.AbsolutePath.StartsWith("/api/"))
                {
                    // Basic routing
                    string command = request.Url.AbsolutePath.Substring("/api/".Length);
                    string data = "";
                    if (request.HasEntityBody)
                    {
                        using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
                        {
                            data = reader.ReadToEnd();
                        }
                    }
                    
                    // Dispatch to StateBridge
                    responseString = StateBridge.ProcessCommand(command, data);
                    
                    System.Diagnostics.Debug.WriteLine($"Received request: {request.Url.AbsolutePath} data={data}");
                }

                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "application/json";
                var output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Request processing error: {ex.Message}");
            }
        }
    }
}
