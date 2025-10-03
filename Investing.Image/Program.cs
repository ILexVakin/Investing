using Investing.Image;

public class Program
{
    static async Task Main()
    {
        //IconInstruments iconInstruments = new IconInstruments();
        //await iconInstruments.GetIsinInstruments();
        ReadingIcon readingIcon = new ReadingIcon();
        var t = readingIcon.GetAllIconsCampany();
    }
}