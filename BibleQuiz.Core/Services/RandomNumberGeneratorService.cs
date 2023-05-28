namespace BibleQuiz.Core
{
    public class RandomNumberGeneratorService
    {
        //private static RandomNumberGeneratorService _instance;

        //private static readonly object _lockObject = new object();

        //private int _randomNumber;

        //private Timer _timer;

        //public int RandomNumber => _randomNumber;

        //private RandomNumberGeneratorService() 
        //{
        //    _randomNumber = GenerateRandomNumber();
        //    _timer = new Timer(GenerateNewRandomNumber, null, TimeSpan.FromHours(24), TimeSpan.FromHours(24));
        
        //}

        //public static RandomNumberGeneratorService Instance
        //{
        //    get
        //    {
        //        if(_instance == null)
        //        {
        //            lock (_lockObject)
        //            {
        //                if(_instance == null)
        //                {
        //                    _instance = new RandomNumberGeneratorService();
        //                }
        //            }
        //        }

        //        return _instance;
        //    }
        //}

        //private void GenerateNewRandomNumber(object state)
        //{
        //    lock(_lockObject)
        //    {
        //        _randomNumber = GenerateRandomNumber();
        //    }
        //}

        //private int GenerateRandomNumber()
        //{
        //    return new Random().Next(3);
        //}
    }
}
