using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class GuideLanguage
    {
        public int GuideId { get; set; }
        public virtual Guide Guide { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
