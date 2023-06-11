namespace BibleQuiz.Core
{
    public static class Verses
    {
        public static(string BookName, string Chapter, string Verse, string Errors) ExtractBibleVerse(string verse)
        {
            string book = default;

            string chapter = default;

            string verseNum = default;

            string errorMessage = default;

            try
            {
                var stringArray = verse.Split("(");

                var verseArray = stringArray[1].Split(" ");

                string[] locationArray = new string[] { };            

                if (verseArray.Count() == 2)
                {
                    if (verseArray[1].Contains(":"))
                    {
                        locationArray = verseArray[1].Split(":");

                        chapter = locationArray[0];

                        verseNum = locationArray[1];

                        book = verseArray[0];
                    }
                }

                if (verseArray.Count() == 3)
                {
                    if (int.TryParse(verseArray[0], out _))
                    {
                        book = verseArray[0] + verseArray[1];

                        var splitChapterAndVerse = verseArray[2].Split(":");

                        chapter = splitChapterAndVerse[0];

                        verseNum = splitChapterAndVerse[1].Replace(")", "");
                    }
                    else
                    {
                        verseNum = verseArray[2].Replace(")", "");

                        chapter = verseArray[1].Trim(':');

                        book = verseArray[0];
                    }

                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;

                return (book, chapter, verseNum, errorMessage);
            }

            return (book, chapter, verseNum, errorMessage);

            //return new string[] { book, chapter, verseNum };
        }
    }
}
