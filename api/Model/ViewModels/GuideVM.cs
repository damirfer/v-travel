using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model.ViewModels
{
   public class GuideVM
    {

        public class Single
        {
            public int GuideId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Bio { get; set; }
            public string PhotoUrl { get; set; }
            public List<int> GuideLanguage { get; set; }
        }

        public class List
        {
            public int GuideId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Bio { get; set; }
            public string PhotoUrl { get; set; }


        }

    }



}
