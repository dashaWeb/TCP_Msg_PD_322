using System.Net;
using System.Net.Sockets;

internal class Program
{
    private static void Main(string[] args)
    {
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);


        TcpListener listener = new TcpListener(ipPoint);

		try
		{
			listener.Start(10);

			Console.WriteLine("Server started!! Wait connection");

			TcpClient client = listener.AcceptTcpClient();

			while (client.Connected)
			{
				NetworkStream ns = client.GetStream();
				StreamReader sr = new StreamReader(ns);
				string response = sr.ReadLine();
				Console.WriteLine($"{client.Client.RemoteEndPoint} - {response} at {DateTime.Now.ToShortTimeString()}");


				StreamWriter writer = new StreamWriter(ns);
				string message = "Message was send!!";
				writer.WriteLine(message);
				writer.Flush();
				/*sr.Close();
				ns.Close();*/
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		finally
		{
			listener.Stop();
		}
    }
}