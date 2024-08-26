using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Common.Validation
{
    public class ValidationConstants
    {
        public static class Post
        {
            public const int MinTitleLenght = 10;
            public const int MaxTitleLenght = 50;
            public const int MinContentLength = 30;
            public const int MaxContentLength = 1500;
        } 
    }
}
