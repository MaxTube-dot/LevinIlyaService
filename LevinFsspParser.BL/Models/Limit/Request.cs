using System;

namespace LevinFsspParser.BL.Models
{
    internal class Request
    {
        public enum TypeRequest
        {
            Group, Single
        }; 
        public DateTime Date { get; set; } =  DateTime.Now;

        public TypeRequest Type { get; set; }
    }


}
