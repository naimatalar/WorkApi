﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Work.Core
{
    public  class Company:BaseEntity
    {
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
