﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.DB
{
    public class Singleton
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
