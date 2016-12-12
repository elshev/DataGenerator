using System.Collections.Generic;

namespace APaers.DataGen
{
    public static class Facker
    {
        public static string Email()
        {
            return Faker.Internet.Email();
        }

        public static IEnumerable<string> LoremParagraphs(int paragraphCount)
        {
            return Faker.Lorem.Paragraphs(paragraphCount);
        }
    }
}