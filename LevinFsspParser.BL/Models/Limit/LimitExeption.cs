using System;

namespace LevinFsspParser.BL.Models
{
    public class LimitExeption : Exception
    {
        public LimitExeption(string message) : base(message)
        {

        }
    }
}
