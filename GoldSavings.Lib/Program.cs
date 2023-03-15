using System.Xml.Linq;
using GoldSavings.Model;

namespace GoldSavings;

public class Tasks
{
    public static void Main()
    {
        Task2();
        Task3();
        Task4();
        Task8();
        Task9();
        Task12();
        Task13();
    }

    public static List<GoldPrice> ReadPricesByDate(DateTime startDate, DateTime endDate)
    {
        GoldClient goldClient = new GoldClient();
        List<GoldPrice> goldPrices = goldClient
            .GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();
        return goldPrices;
    }


    public static void Task2()
    {
        List<GoldPrice> goldPrices = ReadPricesByDate(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));
        IEnumerable<GoldPrice> listDescending =
            from list in goldPrices
            orderby list.Price descending
            select list;

        IEnumerable<GoldPrice> listAscending =
            from list in goldPrices
            orderby list.Price ascending
            select list;

        List<GoldPrice> topHighest = listDescending.Take(3).ToList();
        Console.WriteLine("Highest prices of the gold last year:");
        Console.WriteLine("1: " + topHighest[0].Price);
        Console.WriteLine("2: " + topHighest[1].Price);
        Console.WriteLine("3: " + topHighest[2].Price);


        List<GoldPrice> topLowest = listAscending.Take(3).ToList();
        Console.WriteLine("Lowest prices of the gold last year:");
        Console.WriteLine("1: " + topLowest[0].Price);
        Console.WriteLine("2: " + topLowest[1].Price);
        Console.WriteLine("3: " + topLowest[2].Price);
    }

    public static void Task3()
    {
        List<GoldPrice> goldPrices = ReadPricesByDate(new DateTime(2020, 01, 01), new DateTime(2020, 01, 31));

        IEnumerable<GoldPrice> checkWhereEarned =
            from listJanuary in goldPrices
            where listJanuary.Price / goldPrices[0].Price >= 1.05
            select listJanuary;

        List<GoldPrice> dayEarned5 = checkWhereEarned.ToList();
        foreach (var date in dayEarned5)
        {
            Console.WriteLine("Date: " + date.Date);
        }
    }

    public static void Task4()
    {
        List<GoldPrice> goldPrices2019 = ReadPricesByDate(new DateTime(2019, 01, 01), new DateTime(2019, 12, 31));
        List<GoldPrice> goldPrices2020 = ReadPricesByDate(new DateTime(2020, 01, 01), new DateTime(2020, 12, 31));
        List<GoldPrice> goldPrices2021 = ReadPricesByDate(new DateTime(2021, 01, 01), new DateTime(2021, 12, 31));
        List<GoldPrice> goldPrices2022 = ReadPricesByDate(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));

        goldPrices2019.AddRange(goldPrices2020);
        goldPrices2019.AddRange(goldPrices2021);
        goldPrices2019.AddRange(goldPrices2022);

        IEnumerable<GoldPrice> prices =
            from listPrices in goldPrices2019
            orderby listPrices.Price descending
            select listPrices;

        List<GoldPrice> listPricesSorted = prices.Skip(10).Take(3).ToList();

        Console.WriteLine("Date 1: " + listPricesSorted[0].Date + ", Price: " + listPricesSorted[0].Price);
        Console.WriteLine("Date 2: " + listPricesSorted[1].Date + ", Price: " + listPricesSorted[1].Price);
        Console.WriteLine("Date 3: " + listPricesSorted[2].Date + ", Price: " + listPricesSorted[2].Price);
    }

    public static void Task8()
    {
        List<GoldPrice> goldPrices2020 = ReadPricesByDate(new DateTime(2020, 01, 01), new DateTime(2020, 12, 31));
        List<GoldPrice> goldPrices2021 = ReadPricesByDate(new DateTime(2021, 01, 01), new DateTime(2021, 12, 31));
        List<GoldPrice> goldPrices2022 = ReadPricesByDate(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));

        IEnumerable<double> prices2020 = from listPrices in goldPrices2020 select listPrices.Price;
        double average2020 = prices2020.ToList().Average();

        IEnumerable<double> prices2021 = from listPrices in goldPrices2021 select listPrices.Price;
        double average2021 = prices2021.ToList().Average();

        IEnumerable<double> prices2022 = from listPrices in goldPrices2022 select listPrices.Price;
        double average2022 = prices2022.ToList().Average();

        Console.WriteLine("Average 2019: " + average2020);
        Console.WriteLine("Average 2020: " + average2021);
        Console.WriteLine("Average 2021: " + average2022);
    }

    public static void Task9()
    {
        List<GoldPrice> goldPrices2019 = ReadPricesByDate(new DateTime(2019, 01, 01), new DateTime(2019, 12, 31));
        List<GoldPrice> goldPrices2020 = ReadPricesByDate(new DateTime(2020, 01, 01), new DateTime(2020, 12, 31));
        List<GoldPrice> goldPrices2021 = ReadPricesByDate(new DateTime(2021, 01, 01), new DateTime(2021, 12, 31));
        List<GoldPrice> goldPrices2022 = ReadPricesByDate(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));

        goldPrices2019.AddRange(goldPrices2020);
        goldPrices2019.AddRange(goldPrices2021);
        goldPrices2019.AddRange(goldPrices2022);

        IEnumerable<GoldPrice> ascendingList =
            from listPrices in goldPrices2019
            orderby listPrices.Price ascending
            select listPrices;

        List<GoldPrice> lowestDay = ascendingList.Take(1).ToList();
        Console.WriteLine("Best day to buy: " + lowestDay[0].Date);

        IEnumerable<GoldPrice> descendingList =
            from listPrices in goldPrices2019
            orderby listPrices.Price descending
            select listPrices;

        List<GoldPrice> highestDay = descendingList.Take(1).ToList();
        Console.WriteLine("Best day to sell: " + highestDay[0].Date);
        Console.WriteLine("The return of investment: " + (highestDay[0].Price - lowestDay[0].Price));
    }

    public static void Task12()
    {
        List<GoldPrice> goldPrices2019 = ReadPricesByDate(new DateTime(2019, 01, 01), new DateTime(2019, 12, 31));
        List<GoldPrice> goldPrices2020 = ReadPricesByDate(new DateTime(2020, 01, 01), new DateTime(2020, 12, 31));
        List<GoldPrice> goldPrices2021 = ReadPricesByDate(new DateTime(2021, 01, 01), new DateTime(2021, 12, 31));
        List<GoldPrice> goldPrices2022 = ReadPricesByDate(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));

        goldPrices2019.AddRange(goldPrices2020);
        goldPrices2019.AddRange(goldPrices2021);
        goldPrices2019.AddRange(goldPrices2022);

        XDocument file = new XDocument(new XElement("PricesList",
            from prices in goldPrices2019
            select new XElement("GoldPrice",
                new XElement("Date", prices.Date),
                new XElement("Price", prices.Price)
            )));

        file.Declaration = new XDeclaration("1.0", "utf-8", "true");
        file.Save("/Users/monikazielinska/RiderProjects/GoldSavings/GoldSavings.Lib/Prices.xml");
        Console.WriteLine("XML file created.");
    }

    public static void Task13()
    {
        XDocument file =
            XDocument.Load("/Users/monikazielinska/RiderProjects/GoldSavings/GoldSavings.Lib/Prices.xml");
        foreach (XElement price in file.Root.Elements())
        {
            Console.WriteLine("Date: " + price.Element("Date").Value + " Price: " + price.Element("Price").Value);
        }
    }
}