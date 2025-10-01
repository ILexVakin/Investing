using Investing_image;

public class Program
{
    static async Task Main()
    {
        IconInstruments iconInstruments = new IconInstruments();
        await iconInstruments.GetIsinInstruments();
    }
}