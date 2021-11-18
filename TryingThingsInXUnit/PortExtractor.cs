using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryingThingsInXUnit
{
    internal class PortExtractor
    {

        public PortExtractorConfiguration PortExtractorConfiguration { get; } = new PortExtractorConfiguration();

        /// <summary>
        /// Gets ports from a string which supports comma separated ports and port ranges
        /// </summary>
        /// <param name="input">string with parts</param>
        /// <returns>All possible ports</returns>
        /// <exception cref="ArgumentException">When something with the input is wrong</exception>
        /// <example>1 -> 1</example>
        /// <example>1,2 -> 1,2</example>
        /// <example>1-3 -> 1,2,3</example>
        /// <example>1-3,6 -> 1,2,3,6</example>
        public int[] GetPorts(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            var validationResult = PortExtractorConfiguration.Validate();
            if (!validationResult.Valid)
            {
                throw new Exception($"{nameof(PortExtractorConfiguration)} is invalid because {validationResult.Reason}");
            }

            var regexPattern = "[^\\" + PortExtractorConfiguration.PartSeparator + "|\\d" + (PortExtractorConfiguration.EnableMasking ? "|\\" + PortExtractorConfiguration.MaskIndicator : "") + (PortExtractorConfiguration.EnableRanges ? "|\\" + PortExtractorConfiguration.RangeIndicator : "") + "]";
            if (System.Text.RegularExpressions.Regex.IsMatch(input, regexPattern))
            {
                throw new ArgumentException($"{nameof(input)} contained unsupported characters");
            }

            var ports = new List<int>();
                var rangeParts = input.Split(PortExtractorConfiguration.PartSeparator).Select(x => x.Trim());
                foreach (var rangePart in rangeParts)
                {
                    var unparsedNumbers = rangePart.Split(PortExtractorConfiguration.RangeIndicator).Where(x=> !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();

                    switch (unparsedNumbers.Count())
                    {
                        case 0:
                            throw new ArgumentException($"Range part '{rangePart}' contained no number parts");
                        case 1:
                            var firstUnparsedNumber = unparsedNumbers[0];
                        if (firstUnparsedNumber.Contains(PortExtractorConfiguration.MaskIndicator))
                        {
                            throw new NotSupportedException("Masks not yet supported");
                        
                            /*
                             * * = 1 until 9
                             * 10* -> 100 until 109
                             * 1*1 -> 101,111,121,131,141,151,161,171,181,191
                             * 1** -> 100, 101,102,103,104,105,106,107,108,109,110,111 until 199
                             * *1* -> 10, 11 until 19, 110 until 119, 210 until 219 until 910 untill 919
                             * *** -> 1 untill 999
                             */
                            }
                            else
                            {
                                ports.Add(int.Parse(firstUnparsedNumber));
                            }
                            break;
                        case 2:
                            var portOne = int.Parse(unparsedNumbers[0]);
                            var portTwo = int.Parse(unparsedNumbers[1]);
                            if (portTwo <= portOne)
                                throw new ArgumentException($"Port two {portTwo} should be larger then port one {portOne}");

                            for (int port = portOne; port <= portTwo; port++)
                            {
                                ports.Add(port);
                            }

                            break;

                        default:
                            throw new ArgumentException($"Range part {rangePart} contained more then 2 ({unparsedNumbers.Count()}) unparsed numbers parts");
                    }
            }

            return ports.ToArray();
        }
    }
}
