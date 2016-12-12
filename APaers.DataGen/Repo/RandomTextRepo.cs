using System.Collections.Generic;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Repo
{
    internal class RandomTextRepo : Repo<RandomText>
    {
        public RandomTextRepo(IDataProvider<RandomText> dataProvider) : base(dataProvider)
        {
        }

        public override RandomText GetRandom()
        {
            IEnumerable<string> paragraphs = Facker.LoremParagraphs(16);
            string text = string.Concat(paragraphs);
            return new RandomText {Name = "Lorem ipsum", Text = text};
        }
    }
}