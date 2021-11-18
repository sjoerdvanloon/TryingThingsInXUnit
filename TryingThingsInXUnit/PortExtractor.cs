using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class PortExtractor
    {

        public int[] GetPorts(string input)
        {
            var ports = new List<int>();
            if (!string.IsNullOrWhiteSpace(input))
            {
                var rangeParts = input.Split(',').Select(x => x.Trim());
                foreach (var rangePart in rangeParts)
                {
                    var unparsedNumbers = rangePart.Split("-").Select(x => x.Trim()).ToArray();
                    if (unparsedNumbers.Count() > 2)
                    {
                        throw new ArgumentException($"Range part {rangePart} contained more then 2 ({unparsedNumbers.Count()}) unparsed numbers parts");
                    }

                    if (!int.TryParse(unparsedNumbers[0], out var portOne))
                    {
                        throw new ArgumentException($"{unparsedNumbers[0]} is not an valid int");
                    }

                    if (unparsedNumbers.Count() == 1)
                    {
                        ports.Add(portOne);
                    }
                    else
                    {
                        // Its a range
                        if (!int.TryParse(unparsedNumbers[1], out var portTwo))
                        {
                            throw new ArgumentException($"{unparsedNumbers[1]} is not an valid int");
                        }

                        if (portTwo <= portOne)
                        {
                            throw new ArgumentException($"Port two {portTwo} should be larger then port one {portOne}");
                        }

                        for (int port = portOne; port <= portTwo; port++)
                        {
                            ports.Add(port);
                        }
                    }

                }
            }

            return ports.ToArray();
        }
    }
}
