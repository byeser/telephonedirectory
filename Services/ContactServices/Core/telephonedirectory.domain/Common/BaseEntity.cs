﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telephonedirectory.domain.Common
{
    abstract public class BaseEntity
    {
        [Key]
        public Guid UUID { get; set; }
    }
}
